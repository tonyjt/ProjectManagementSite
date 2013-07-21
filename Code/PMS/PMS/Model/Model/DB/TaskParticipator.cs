using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class TaskParticipator
    {
        public TaskParticipator()
        {
            this.Plans = new List<Plan>();
        }

        public System.Guid TaskParticipatorId { get; set; }
        public System.Guid TaskId { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public short Roles { get; set; }
        public byte Status { get; set; }
        public virtual ICollection<Plan> Plans { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public virtual User User { get; set; }
    }
}
