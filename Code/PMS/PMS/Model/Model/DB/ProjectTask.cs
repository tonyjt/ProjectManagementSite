using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class ProjectTask
    {
        public ProjectTask()
        {
            this.TaskParticipators = new List<TaskParticipator>();
        }

        public System.Guid TaskId { get; set; }
        public System.Guid ProjectId { get; set; }
        public Nullable<System.Guid> RequirementId { get; set; }
        public string Content { get; set; }
        public System.Guid Creator { get; set; }
        public byte Status { get; set; }
        public string History { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public virtual Project Project { get; set; }
        public virtual Requirement Requirement { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TaskParticipator> TaskParticipators { get; set; }
    }
}
