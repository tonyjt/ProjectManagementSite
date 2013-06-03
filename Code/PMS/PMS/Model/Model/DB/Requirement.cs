using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class Requirement
    {
        public System.Guid RequirementId { get; set; }
        public System.Guid VersionId { get; set; }
        public System.Guid ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public bool IsValid { get; set; }
        public virtual ProjectVersion ProjectVersion { get; set; }
    }
}
