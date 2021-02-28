using CropsV4.Alm.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using System.Linq;

namespace CropsV4.Alm.Operations
{
    public class DataOperations : IDataOperations
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _db;

        public DataOperations()
        {
            _mongoClient = new MongoClient("mongodb://127.0.0.1:27017");
            _db = _mongoClient.GetDatabase("Crops_MongoDB");
        }

        public void InsertWorkItems(IList<WorkItem> workItems)
        {
            var BsonWorkItems = _db.GetCollection<BsonDocument>("WorkItems");
            Dictionary<string, object> tempDic = new Dictionary<string, object>();
            var bsonDoc = new BsonDocument { };
            foreach (var item in workItems)
            {
                foreach (var i in item.Fields)
                {
                    if (i.Value.GetType() != typeof(IdentityRef))
                        tempDic.Add(i.Key.ToString().Replace("System", "").Replace(".", ""), i.Value);
                    else
                    {
                        tempDic.Add(i.Key.ToString().Replace("System.", "").Replace(".", "") + "Id", ((IdentityRef)i.Value).Id);
                        tempDic.Add(i.Key.ToString().Replace("System.", "").Replace(".", "") + "DisplayName", ((IdentityRef)i.Value).DisplayName);
                    }
                }
                //tempDic.Add("ParentSourceId",item.Relations.Select(e=>e.Attributes.))
                bsonDoc.AddRange(tempDic);
                BsonWorkItems.InsertOne(bsonDoc);
                tempDic.Clear();
                bsonDoc.Clear();
            }
            Console.WriteLine("Done");
        }

        public void InsertTeams(List<WebApiTeam> teams)
        {
            var BsonTeam = _db.GetCollection<BsonDocument>("Teams");
            Dictionary<string, object> tempDic = new Dictionary<string, object>();
            var bsonDoc = new BsonDocument { };
            foreach (var item in teams)
            {

                if (item.Name.GetType() != typeof(IdentityRef))
                {
                    tempDic.Add("TeamName", item.Name);
                    tempDic.Add("ProjectId", item.ProjectId);
                    tempDic.Add("ProjectName", item.ProjectName);
                    tempDic.Add("Description", item.Description);
                    tempDic.Add("URL", item.Url);

                }
                else
                {
                    tempDic.Add("TeamId", (item.Id));
                }
                bsonDoc.AddRange(tempDic);
                BsonTeam.InsertOne(bsonDoc);
                tempDic.Clear();
                bsonDoc.Clear();
            }
        }

        public void InsertIterations(List<TeamSettingsIteration> iterations)
        {
            var BsonIteration = _db.GetCollection<BsonDocument>("Iterations");
            Dictionary<string, object> tempDic = new Dictionary<string, object>();
            var bsonDoc = new BsonDocument { };
            foreach (var item in iterations)
            {
                if (item.Name.GetType() != typeof(IdentityRef))
                {
                    tempDic.Add("IterationName", item.Name);
                    tempDic.Add("IterationPath", item.Path);
                    tempDic.Add("Links", item.Links);
                }
                else
                {
                    tempDic.Add("IterationId", item.Id);
                }
                bsonDoc.AddRange(tempDic);
                BsonIteration.InsertOne(bsonDoc);
                tempDic.Clear();
                bsonDoc.Clear();
            }
        }
    }
}
