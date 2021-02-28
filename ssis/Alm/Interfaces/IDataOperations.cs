using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;


namespace CropsV4.Alm.Interfaces
{
    public interface IDataOperations
    {
        //public void InsertProjects(IList<TeamProjectReference> workItems);
        public void InsertWorkItems(IList<WorkItem> workItems);
        public void InsertTeams(List<WebApiTeam> teams);
        public void InsertIterations(List<TeamSettingsIteration> iterations);
    }
}
