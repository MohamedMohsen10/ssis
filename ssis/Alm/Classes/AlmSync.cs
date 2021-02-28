using CropsV4.Alm.Interfaces;
using CropsV4.Alm.Operations;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4.Alm.Classes
{
    public class AlmSync
    {
        private readonly IAlm _almSource;
        private readonly AlmProject _almProject;
        private readonly IDataOperations dataOperations;
        public string ProjectId { get; set; }

        public AlmSync(AlmProject almProject)
        {
            _almSource = new AzureAlm();  // can be azure or jira 
            _almProject = almProject;
            dataOperations = new DataOperations();
        }

        public async Task SyncAsync()
        {
            //var projects = _almSource.GetProjects(_almProject.CollectionUri, _almProject.ProjectName, _almProject.Token);
            //dataOperations.InsertProjects(projects);

            var workitems = await _almSource.GetAllWorkItems(_almProject.CollectionUri, _almProject.ProjectName, _almProject.Token);
            dataOperations.InsertWorkItems(workitems);

            var relation = await _almSource.RunWorkItemLinksQueryAndGetRelations(new Guid(_almProject.SourceId),_almProject.CollectionUri,_almProject.Token);

            var teams = _almSource.GetTeams(_almProject.CollectionUri, _almProject.ProjectName, _almProject.Token);
            dataOperations.InsertTeams(teams);

            var iterations = _almSource.GetAllIterations(_almProject.CollectionUri, _almProject.ProjectName, _almProject.Token, teams);
            dataOperations.InsertIterations(iterations);

        }

        



    }
}
