using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public partial class TaskParticipator
    {
        [NotMapped]
        public RoleEnum RoleEnum
        {
            get
            {
                return (RoleEnum)Roles;
            }
            set
            {
                Roles = (short)value;
            }
        }

        [NotMapped]
        public TaskParticipatorStatus StatusEnum
        {
            get
            {
                return (TaskParticipatorStatus)Status;
            }
            set
            {
                Status = (byte)value;
            }
        }
        public TaskParticipator() { }

        public TaskParticipator(RoleEnum role, Guid userId)
        {
            this.RoleEnum = role;
            this.UserId = userId;
        }
    }
}