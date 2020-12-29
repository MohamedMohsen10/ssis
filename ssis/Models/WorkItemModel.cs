using System;
using System.Collections.Generic;
using System.Text;

namespace ssis.Models
{

    public class WorkItemModel
    {


        public virtual string Name { get; set; }
        public virtual long SourceId { get; set; }
        public virtual int? ParentSourceId { get; set; }
        public virtual int? IterationSk { get; set; }
        public virtual int? AreaSk { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ClosedDate { get; set; }
        public virtual string State { get; set; } = "NULL";
        public virtual Guid? ProjectId { get; set; }
        public virtual string TeamProject { get; set; } = "NULL";
        public virtual string Type { get; set; }

        public virtual string Title { get; set; }
        public virtual DateTime ChangedDate { get; set; }
        public virtual DateTime StateChangeDate { get; set; }
        public virtual string AssignedTo { get; set; } = "Not Assigned";
        public virtual string CreatedBy { get; set; } = "Not Assigned";
        public virtual string ResolvedBy { get; set; } = "Not Assigned";
        public virtual string ChangedBy { get; set; } = "Not Assigned";
        public virtual string AssigneeSourceId { get; set; }
        public virtual string CreatedBySourceId { get; set; }
        public virtual string ResolvedBySourceId { get; set; }
        public virtual string ChangedBySourceId { get; set; }
        public virtual int StoryPoints { get; set; } = 0;
        public virtual decimal CompletedWork { get; set; } = 0;
        public virtual decimal OriginalEstimate { get; set; } = 0;
        public virtual decimal RemainingWork { get; set; } = 0;
        public virtual string Activity { get; set; } = "NULL";
        public virtual long? Rev { get; set; }
        public virtual string Tags { get; set; }
        public virtual string Severity { get; set; }
        public virtual decimal Effort { get; set; } = 0;
        public virtual long? Priority { get; set; } = 0;
        public virtual string ValueArea { get; set; }
        public virtual string IterationLevel7 { get; set; }
        public virtual string IterationLevel6 { get; set; }
        public virtual string IterationLevel5 { get; set; }
        public virtual string IterationLevel4 { get; set; }
        public virtual string IterationLevel3 { get; set; }
        public virtual string IterationLevel2 { get; set; }
        public virtual string IterationLevel1 { get; set; }
        public virtual string AreaLevel7 { get; set; }
        public virtual string AreaLevel6 { get; set; }
        public virtual string AreaLevel5 { get; set; }
        public virtual string AreaLevel4 { get; set; }
        public virtual string AreaLevel3 { get; set; }
        public virtual string AreaLevel2 { get; set; }
        public virtual string AreaLevel1 { get; set; }

        public virtual string Reason { get; set; }
    }
}
