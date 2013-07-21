using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public partial class Plan
    {
        [NotMapped]
        public PlanStatus StatusEnum
        {
            get
            {
                return (PlanStatus)Status;
            }
            set
            {
                Status = (byte)value;
            }
        }
    }
}
