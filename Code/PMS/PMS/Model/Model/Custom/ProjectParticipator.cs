using PMS.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public partial class ProjectParticipator
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
    }
}
