using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public enum ProjectTaskStatus : byte
    {
        [Display(Name = "未分配")]
        Unassigned = 0,

        [Display(Name = "分配中")]
        Assigning = 1,

        [Display(Name = "已分配")]
        Assigned = 2,

        [Display(Name = "进行中")]
        Finishing = 3,

        [Display(Name = "已完成")]
        Finished = 4,

        [Display(Name = "已取消")]
        Canceled = 5
    }


    public enum TaskParticipatorStatus : byte
    {
        Unassigned = 0,

        Assigned = 1,

        Working = 2,

        Finished = 3,

        Delayed = 4,

        Canceled  = 5
    }

}
