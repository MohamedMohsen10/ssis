using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
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
    }
}
