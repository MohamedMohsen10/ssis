using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.DependencyInjection;
using CropsV4.Alm.Classes;
using CropsV4.Alm;
using System.Data.SqlClient;
using CropsV4.DataBaseConnection;

namespace CropsV4
{
    class Program
    {
        public static void Main(string[] args)
        {
            DbConnection dbConnection = new DbConnection();
            var projects  = dbConnection.GetProjects();

            //AlmProject almProject = new AlmProject("https://projects.integrant.com/TFS/BICollection", "TFS Reports", "vxgwn6lrc6stdf52aem5stdx7hffmc4mt4epubpqcricso3ccmba");

            foreach (var project in projects)
            {
                AlmProject almProject = new AlmProject(project.CollectionUri, project.ProjectName, project.Token, project.SourceId);
                AlmSync almSync = new AlmSync(almProject);
                almSync.ProjectId = almProject.SourceId;
                almSync.SyncAsync();
            }


        }
    }
}
