using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class TaskParticipator
    {
        public System.Guid TaskParticipatorId { get; set; }
        public System.Guid TaskId { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public string Role { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public virtual User User { get; set; }
    }
}
