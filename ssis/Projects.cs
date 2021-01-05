using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CropsV4
{
    public class Projects
    {


        public List<TeamProjectReference> ListAllProjectsAndTeams(string collectionUri, string project, string token)
        {
            var credentials = new VssBasicCredential(string.Empty, token);
            ProjectHttpClient httpClient = new ProjectHttpClient(new Uri(collectionUri), credentials);
        
      
            IEnumerable<TeamProjectReference> projects = httpClient.GetProjects().Result;
            var list = projects.ToList();
          
          
                
            if (projects.Count() == 0)
            {
                Console.WriteLine("No projects found.");
            }

            return list;
         
        }
 }
}