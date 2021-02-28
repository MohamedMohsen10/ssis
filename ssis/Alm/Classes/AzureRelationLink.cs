using System;
using System.Collections.Generic;
using System.Text;

namespace CropsV4.Alm.Classes
{
    public class AzureRelationLink
    {
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        public string RelationType { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
