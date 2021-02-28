using CropsV4.Alm.Classes;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4
{
    public class WorkItemsHistoryFromAzure : IAlm
    {
        public List<TeamSettingsIteration> GetAllIterations(string collectionUri, string projectName, string token, List<WebApiTeam> teams)
        {
            throw new NotImplementedException();
        }

        public IList<WorkItem> GetAllWorkItems(string collectionUri, string projectName, string token)
        {
            throw new NotImplementedException();
        }

        public Task<IList<WorkItem>> GetAllWorkItemsAsyc(string collectionUri, string project, string token)
        {
            throw new NotImplementedException();
        }

        public List<TeamProjectReference> GetProjects(string collectionUri, string project, string token)
        {
            throw new NotImplementedException();
        }

        public List<WebApiTeam> GetTeams(string collectionUri, string projectName, string token)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AzureRelationLink>> RunWorkItemLinksQueryAndGetRelations(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AzureRelationLink>> RunWorkItemLinksQueryAndGetRelations(Guid projectId, string collectionUri, string token)
        {
            throw new NotImplementedException();
        }

        Task<IList<WorkItem>> IAlm.GetAllWorkItems(string collectionUri, string projectName, string token)
        {
            throw new NotImplementedException();
        }
    }
}
