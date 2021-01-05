using DocumentFormat.OpenXml.InkML;
using Microsoft.CodeAnalysis;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4
{
    public class Teams
    {
        public List<WebApiTeam> GetTeams(string collectionUri, string projectName, string token)
        {
            var Teams = ListAllTeams(collectionUri, projectName, token);
            return Teams;
        }

        
        public  List<WebApiTeam> ListAllTeams(string collectionUri, string projectName, string token)
        {
           
            var credentials = new VssBasicCredential(string.Empty, token);
          
            TeamHttpClient teamClient = new TeamHttpClient(new Uri(collectionUri), credentials);
            // Get the teams for the project
            List<WebApiTeam> result =  teamClient.GetTeamsAsync(projectName).Result;

            var teams = result.Select(e => new WebApiTeam
            {
                Id = e.Id,
                Name = e.Name,
               
            }).ToList();
           return teams;
          
        }

    }
}
