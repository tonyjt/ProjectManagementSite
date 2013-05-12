using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class ProjectParticipator
    {
        public System.Guid ProjectId { get; set; }
        public System.Guid UserId { get; set; }
        public short Roles { get; set; }
        public System.DateTime JoinTime { get; set; }
    }
}
