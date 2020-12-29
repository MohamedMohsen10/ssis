using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4
{
    interface ALMDataFromAzureSDK
    {
        public IList<WorkItem> GetAllWorkItems(string collectionUri, string projectName, string token);
        public Task<IList<WorkItem>> GetAllWorkItemsAsyc(string collectionUri, string project, string token);
        public IList<WorkItem> GetAllWorkItemsHistory(string collectionUri, string projectName, string token);
        public Task<IList<WorkItem>> GetAllWorkItemsHistoryAsyc(string collectionUri, string project, string token);
    }
}
