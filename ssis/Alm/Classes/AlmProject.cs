using System;
using System.Collections.Generic;
using System.Text;

namespace CropsV4.Alm
{
    public class AlmProject
    {
        public string CollectionUri { get; set; }
        public string ProjectName { get; set; }
        public string Token { get; set; }
        public string SourceId { get; set; }

        public AlmProject(string CollectionUri, string ProjectName, string Token, string SourceId)
        {
            this.CollectionUri = CollectionUri;
            this.ProjectName = ProjectName;
            this.Token = Token;
            this.SourceId = SourceId;
        }
    }
}
