using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ssis
{
    public class DataFromSDK
    {


        public IList<WorkItem> GetAllWorkItems()
        {


            const String collectionUri = "https://projects.integrant.com/TFS/BICollection";
            const String projectName = "TFS Reports";
            const string token = "vxgwn6lrc6stdf52aem5stdx7hffmc4mt4epubpqcricso3ccmba";


            //const String collectionUri = "https://dev.azure.com/IPTS-Perspecta";
            //const String projectName = "IPTS";
            //const string token = "gbjnuvypldsi4e2rvrk3a2xvahvgccfbzhh4tdjw2usdg6awufma";


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

                //// build a list of the fields we want to see
                //var fields = new[] { "System.Id", "System.Title", "System.State" ,"System.CommentCount",
                //    "System.CreatedDate","System.CreatedBy","System.ChangedBy","System.Links.LinkType",
                //     "Effort","Microsoft.VSTS.Scheduling.StoryPoints","Microsoft.VSTS.Scheduling.OriginalEstimate",
                //     "Microsoft.VSTS.Scheduling.RemainingWork","Microsoft.VSTS.Scheduling.CompletedWork"


                //};
                foreach (var workItem in result.WorkItems)
                {

                    count++;
                    ids.Add(workItem.Id);
                    if (count >= 200)
                    {
                        //workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false));
                        workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.All));
                        count = 0;
                        ids.Clear();
                    }   

                }

                //ids.Add(66);
                //workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false));

                return workItems;
                // get work items for the ids found in query

                //    return await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false);
            }
        }

    }
}
