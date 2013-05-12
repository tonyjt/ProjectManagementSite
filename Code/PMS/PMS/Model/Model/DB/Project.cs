using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class Project
    {
        public System.Guid ProjectId { get; set; }
        public string Name { get; set; }
        public System.Guid Creator { get; set; }
        public System.DateTime CreateTime { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public virtual User User { get; set; }
    }
}
