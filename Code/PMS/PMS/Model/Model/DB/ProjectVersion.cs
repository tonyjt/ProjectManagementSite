using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class ProjectVersion
    {
        public ProjectVersion()
        {
            this.Requirements = new List<Requirement>();
            this.RequirementHistories = new List<RequirementHistory>();
        }

        public System.Guid VersionId { get; set; }
        public System.Guid ProjectId { get; set; }
        public System.Guid Creator { get; set; }
        public string VersionName { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public byte Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<RequirementHistory> RequirementHistories { get; set; }
        public virtual User User { get; set; }
    }
}
