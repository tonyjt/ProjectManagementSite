using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class Version
    {
        public System.Guid VersionId { get; set; }
        public System.Guid ProjectId { get; set; }
        public string VersionName { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
    }
}
