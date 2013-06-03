using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class RequirementHistory
    {
        public System.Guid HistoryId { get; set; }
        public System.Guid RequirementId { get; set; }
        public System.Guid VersionId { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual ProjectVersion ProjectVersion { get; set; }
    }
}