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
    public interface IAlm
    {
        //public List<TeamProjectReference> GetProjects(string collectionUri, string project, string token);
        public Task<IList<WorkItem>> GetAllWorkItems(string collectionUri, string projectName, string token);
        public Task<IList<WorkItem>> GetAllWorkItemsAsyc(string collectionUri, string project, string token);
        public List<WebApiTeam> GetTeams(string collectionUri, string projectName, string token);
        public List<TeamSettingsIteration> GetAllIterations(string collectionUri, string projectName, string token, List<WebApiTeam> teams);
        public Task<IEnumerable<AzureRelationLink>> RunWorkItemLinksQueryAndGetRelations(Guid projectId, string collectionUri, string token);
    }
}
