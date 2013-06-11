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
    }
}
