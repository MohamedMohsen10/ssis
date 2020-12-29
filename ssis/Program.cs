using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json;
using ssis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver;

using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using System.Collections;

namespace ssis
{
    class Program
    {
        public static void Main(string[] args)
        {
             //IList<WorkItem> workItems = GetAllWorkItems();
            System.Text.StringBuilder stringbuilder = new System.Text.StringBuilder(String.Empty);

            DataFromSDK dataFromSDK = new DataFromSDK();

            IList<WorkItem> workItems = dataFromSDK.GetAllWorkItems();
            #region BuildingStringforCSV
            /*
            foreach (WorkItem item in workItems)
            {

                stringbuilder.Append(item.Fields["System.Id"].ToString().Replace(",", "-") ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.Title"].ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.State"].ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.CommentCount"].ToString().Replace(",", "-") ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append(((IdentityRef)item.Fields["System.CreatedBy"]).Id.ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(((IdentityRef)item.Fields["System.ChangedBy"]).Id.ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "System.Links.LinkType")?.FirstOrDefault().Value
                    ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Effort")?.FirstOrDefault().Value
                    ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.StoryPoints")?.FirstOrDefault().Value
                    ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.RemainingWork")?.FirstOrDefault().Value
                   ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.CompletedWork")?.FirstOrDefault().Value
                                  ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.OriginalEstimate")?.FirstOrDefault().Value
                                   ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Common.Severity")?.FirstOrDefault().Value
                                  ?.ToString()?.Replace(",", "-")) ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Common.Priority")?.FirstOrDefault().Value
                                  ?.ToString()?.Replace(",", "-")) ?? "0");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.WorkItemType"].ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.TeamProject"].ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.IterationPath"].ToString().Replace(",", "-") ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.Id"].ToString().Replace(",", "-") ?? "0"); //addsomething-------
                stringbuilder.Append(",");
                stringbuilder.Append((item?.Fields.Where(wi => wi.Key == "Microsoft.VSTS.Common.ValueArea")?.FirstOrDefault().Value
                                  ?.ToString()?.Replace(",", "-")) ?? "NULL");
                stringbuilder.Append(",");
                stringbuilder.Append(item.Fields["System.IterationId"].ToString().Replace(",", "-") ?? "0");
                stringbuilder.Append(",");

                stringbuilder.Append(((IdentityRef)(item.Fields.Where(wi => wi.Key == "System.AssignedTo")
                    .FirstOrDefault().Value))?.DisplayName?.ToString().Replace(",", "-") ?? "NULL");
                //stringbuilder.Append(",");
                stringbuilder.Append("|\n");
            }*/
            #endregion
            #region CommentedRegion






            //List<WorkItemModel> workItemModels = new List<WorkItemModel>();
            //WorkItemModel workItemModel = new WorkItemModel();
            //foreach (var item in workItems) {
            //    workItemModel.SourceId = long.Parse(item.Fields["System.Id"].ToString());

            //    workItemModel.State = item.Fields["System.State"].ToString().Replace(",", "-") ?? "NULL";

            //    workItemModel.Title = item.Fields["System.Title"].ToString().Replace(",", "-") ?? "NULL";

            //    workItemModel.CompletedWork = decimal.Parse(item?.Fields
            //        .Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.CompletedWork")?.FirstOrDefault().Value.ToString());

            //    workItemModel.OriginalEstimate = decimal.Parse(item?.Fields
            //        .Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.OriginalEstimate")?.FirstOrDefault().Value.ToString());

            //    workItemModel.RemainingWork = decimal.Parse(item?.Fields
            //        .Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.RemainingWork")?.FirstOrDefault().Value.ToString());

            //    workItemModel.Effort =decimal.Parse(item?.Fields.Where(wi => wi.Key == "Effort")?
            //       .FirstOrDefault().Value.ToString());

            //    workItemModel.StoryPoints = int.Parse(item.Fields
            //        .Where(wi => wi.Key == "Microsoft.VSTS.Scheduling.StoryPoints")?.FirstOrDefault().Value.ToString());

            //workItemModel.Severity = item?.Fields.Where(wi => wi.Key == "Severity")?
            //   .FirstOrDefault().Value.ToString();

            //workItemModel.Tags = workItemModel.Severity = item?.Fields.Where(wi => wi.Key == "Tags")?
            //   .FirstOrDefault().Value.ToString();

            //workItemModel.Priority = 


            //workItemModels.Add(workItemModel);           
            //}

            //using (StreamWriter file = File.CreateText(@"C:\temp\WorkItems.JSON"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    //serialize object directly into file stream
            //    //serializer.Serialize(file, JsonConvert.SerializeObject(workItems.Select(p => p.Fields)));
            //    serializer.Serialize(file, Newtonsoft.Json.JsonConvert.SerializeObject(workItems.Select(p => p.Fields)));
            //}

            //System.IO.File.WriteAllText(@"C:\temp\WorkItems.csv", stringbuilder.ToString());
            //Console.WriteLine("Done");

            #endregion

            var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("Crops_MongoDB");
            var BsonWorkItems = db.GetCollection<BsonDocument>("WorkItems");
            Dictionary<string,object> tempDic = new Dictionary<string, object>();
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
                bsonDoc.AddRange(tempDic);
                BsonWorkItems.InsertOne(bsonDoc);
                tempDic.Clear();
                bsonDoc.Clear();
            }   
            Console.WriteLine("Done");
        }
    }
}
