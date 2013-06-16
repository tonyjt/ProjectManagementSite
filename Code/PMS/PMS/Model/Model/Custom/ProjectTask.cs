using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public partial class ProjectTask
    {
        [NotMapped]
        public ProjectTaskStatus StatusEnum
        {
            get
            {
                return (ProjectTaskStatus)Status;
            }
            set
            {
                Status = (byte)value;
            }
        }



        public bool ContainRole(RoleEnum role)
        {
            if (TaskParticipators != null)
            {
                return TaskParticipators.Where(p => p.RoleEnum == role).Count() > 0;
            }
            else
                return false;
        }

        public TaskParticipator GetRole(RoleEnum role)
        {
            if (TaskParticipators != null)
            {
                return TaskParticipators.Where(p => p.RoleEnum == role).FirstOrDefault();
            }
            else
                return null; ;
        }


        //public User 
    }
}
