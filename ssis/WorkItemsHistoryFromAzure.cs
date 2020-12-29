using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4
{
    public class WorkItemsHistoryFromAzure : ALMDataFromAzureSDK
    {
        public IList<WorkItem> GetAllWorkItems(string collectionUri, string projectName, string token)
        {
            throw new NotImplementedException();
        }

        public Task<IList<WorkItem>> GetAllWorkItemsAsyc(string collectionUri, string project, string token)
        {
            throw new NotImplementedException();
        }

        public  IList<WorkItem> GetAllWorkItemsHistory(string collectionUri, string projectName, string token)
        {
            var workItems = GetAllWorkItemsHistoryAsyc(collectionUri, projectName, token).Result;
            return workItems;
        }

        public async Task<IList<WorkItem>> GetAllWorkItemsHistoryAsyc(string collectionUri, string project, string token)
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
                        //workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, fields, result.AsOf).ConfigureAwait(false));
                        workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.All));
                        count = 0;
                        ids.Clear();
                    }

                }
                return workItems;
            }
        }
    }
}
