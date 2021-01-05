using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.Core.WebApi.Types;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CropsV4
{
    public class Iterations
    {
        public List<TeamSettingsIteration> GetAllIterations(string collectionUri, string projectName,string token)
        {
            var iterations = GetProjectIterations(collectionUri, projectName, token).Result;
            return iterations;
        }
        public async Task<List<TeamSettingsIteration>> GetProjectIterations(string serverUrl, string projectName, string token)
        {
            var uri = new Uri(serverUrl);
            var creds = new VssClientCredentials(new WindowsCredential(true), new VssFederatedCredential(true), CredentialPromptType.PromptIfNeeded);

            var azureDevopsConnection = new VssConnection(uri, creds);
            await azureDevopsConnection.ConnectAsync();

            WorkHttpClient azureDevOpsWorkHttpClient = azureDevopsConnection.GetClient<WorkHttpClient>();
            TeamContext teamContext = new TeamContext(projectName);

            List<TeamSettingsIteration> results = await azureDevOpsWorkHttpClient.GetTeamIterationsAsync(teamContext);
            return results;
         
        }
    }
}
