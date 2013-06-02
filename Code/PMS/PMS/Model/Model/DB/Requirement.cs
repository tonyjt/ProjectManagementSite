using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class Requirement
    {
        public Requirement()
        {
            this.Requirement1 = new List<Requirement>();
            this.RequirementHistories = new List<RequirementHistory>();
        }

        public System.Guid RequirementId { get; set; }
        public System.Guid VersionId { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public bool IsValid { get; set; }
        public virtual ProjectVersion ProjectVersion { get; set; }
        public virtual ICollection<Requirement> Requirement1 { get; set; }
        public virtual Requirement Requirement2 { get; set; }
        public virtual ICollection<RequirementHistory> RequirementHistories { get; set; }
    }
}
