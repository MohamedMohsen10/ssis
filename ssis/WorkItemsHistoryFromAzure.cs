using Aspose.Cells.Revisions;
using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4
{
    public class WorkItemsHistoryFromAzure 
    {
      

        public  List<WorkItem> GetAllWorkItemsHistory(string collectionUri, string project, string token)
         {
            var workitemhistory = GetAllWorkItemsHistoryAsyc(collectionUri, project, token).Result;
            return workitemhistory;
         }

       
        public async Task<List<WorkItem>> GetAllWorkItemsHistoryAsyc(string collectionUri, string project, string token)
         {
             var credentials = new VssBasicCredential(string.Empty, token);
             List<int> ids = new List<int>();
             // create a wiql object and build our query
             var wiql = new Wiql()
             {
                 // NOTE: Even if other columns are specified, only the ID & URL will be available in the WorkItemReference
                 Query = "Select [Id] " +
                         "From WorkItem  " +
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
                        // workItems.AddRange(await httpClient.GetWorkItemsHistory(projectId, workItemsIds.Select(i => (long)i).ToList()));
                        workItems.AddRange(await httpClient.GetWorkItemsAsync(ids, expand: WorkItemExpand.All));
                        string strOldValue, strNewValue;
                       
                        //workItems.AddRange(await GetWor(ids,expand:WorkItemExpand.All));
                        count = 0;
                         ids.Clear();
                     }

                 }
                 return workItems;
             }
        }
       

    }
}
