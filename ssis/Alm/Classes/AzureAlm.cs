using CropsV4.Alm.Classes;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CropsV4
{
    public class AzureAlm : IAlm
    {
        public WorkItemTrackingHttpClient WorkItemTrackingClient;

        public async Task<IList<WorkItem>> GetAllWorkItems(string collectionUri, string projectName, string token)
        {
            var workItems = GetAllWorkItemsAsyc(collectionUri, projectName, token).Result;
            return workItems;
        }
        public async Task<IList<WorkItem>> GetAllWorkItemsAsyc(string collectionUri, string project, string token)
        {

            var credentials = new VssBasicCredential(string.Empty, token);
            List<int> ids = new List<int>();
            // create a wiql object and build our query
            var wiql = new Wiql()
            {
                // NOTE: Even if other columns are specified, only the ID & URL will be available in the WorkItemReference
                Query = "Select [Id] " +
                        "From WorkItems " +
                        "Where [System.TeamProject] = '" + project + "'",
            };

            // create instance of work item tracking http client
            using (WorkItemTrackingHttpClient httpClient = new WorkItemTrackingHttpClient(new Uri(collectionUri), credentials))
            {
                // execute the query to get the list of work items in the results
                var result = await httpClient.QueryByWiqlAsync(wiql).ConfigureAwait(false);
                int count = 0;
                List<WorkItem> workItems = new List<WorkItem>();
                foreach (var workItem in result.WorkItems)
                {

                    count++;
                    ids.Add(workItem.Id);
                    if (count >= 200)
                    {
                        workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.All));
                        count = 0;
                        ids.Clear();
                    }
                    else if (count < 200 && workItem == result.WorkItems.Last())
                    {
                        workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.All));
                    }
                }
                return workItems;
            }
        }
        public List<WebApiTeam> GetTeams(string collectionUri, string projectName, string token)
        {
            var Teams = ListAllTeams(collectionUri, projectName, token);
            return Teams;
        }
        public List<WebApiTeam> ListAllTeams(string collectionUri, string projectName, string token)
        {

            var credentials = new VssBasicCredential(string.Empty, token);

            TeamHttpClient teamClient = new TeamHttpClient(new Uri(collectionUri), credentials);
            // Get the teams for the project
            List<WebApiTeam> result = teamClient.GetTeamsAsync(projectName).Result;

            var teams = result.Select(e => new WebApiTeam
            {
                Id = e.Id,
                Name = e.Name,

            }).ToList();
            return teams;

        }

        public List<TeamSettingsIteration> GetAllIterations(string collectionUri, string projectName, string token, List<WebApiTeam> teams)
        {
            var iterations = GetProjectIterations(collectionUri, projectName, token, teams).Result;
            return iterations;
        }

        public async Task<List<TeamSettingsIteration>> GetProjectIterations(string serverUrl, string projectName, string token, List<WebApiTeam> teams)
        {
            var uri = new Uri(serverUrl);
            var credentials = new VssBasicCredential(string.Empty, token);

            var azureDevopsConnection = new VssConnection(uri, credentials);
            await azureDevopsConnection.ConnectAsync();

            WorkHttpClient azureDevOpsWorkHttpClient = azureDevopsConnection.GetClient<WorkHttpClient>();

            var teamsIterations = new List<TeamSettingsIteration>();
            foreach (var item in teams)
            {
                TeamContext teamContext = new TeamContext(projectName, item.Name);
                List<TeamSettingsIteration> results = await azureDevOpsWorkHttpClient.GetTeamIterationsAsync(teamContext);
                teamsIterations.AddRange(results);
            }

            return teamsIterations;
        }




        public async Task<IEnumerable<AzureRelationLink>> RunWorkItemLinksQueryAndGetRelations(Guid projectId, string collectionUri, string token)
        {
            bool isLastBatch = false;
            string continouationToken = null;
            List<WorkItemRelation> relations = new List<WorkItemRelation>();
            var links = new List<AzureRelationLink>();
            var credentials = new VssBasicCredential(string.Empty, token);
            WorkItemTrackingClient = new WorkItemTrackingHttpClient(new Uri(collectionUri),credentials);
                do
                {
                    try
                    {
                        var result = await WorkItemTrackingClient.GetReportingLinksByLinkTypeAsync(projectId, null, null, continouationToken);
                        isLastBatch = result.IsLastBatch;
                        continouationToken = result.ContinuationToken;
                        relations.AddRange(result.Values);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);  
                    }
                }
                while (!isLastBatch);
            

            foreach (var relation in relations)
            {
                if ((Boolean)relation.Attributes["isActive"] && relation.Attributes["linkType"].ToString() == "System.LinkTypes.Hierarchy-Forward")
                {
                    int.TryParse(relation.Attributes["sourceId"].ToString(), out int sourceId);
                    int.TryParse(relation.Attributes["targetId"].ToString(), out int targetId);
                    DateTime? currentRelationChangedDate = (DateTime?)relation.Attributes["changedDate"];
                    var indexOfLink = links.FindIndex(l => l.TargetId == targetId);

                    if (indexOfLink >= 0 && links[indexOfLink].ChangedDate < currentRelationChangedDate)
                        links[indexOfLink] = new AzureRelationLink() { SourceId = sourceId, TargetId = targetId, ChangedDate = currentRelationChangedDate };
                    else
                        links.Add(new AzureRelationLink() { SourceId = sourceId, TargetId = targetId, ChangedDate = currentRelationChangedDate });
                }
            }
            return links;
        }




    }
}
