using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class Plan
    {
        public System.Guid PlanId { get; set; }
        public string Title { get; set; }
        public System.Guid TaskParticipatorId { get; set; }
        public bool AllDay { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public byte Status { get; set; }
        public virtual TaskParticipator TaskParticipator { get; set; }
    }
}
