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
        Assigning,

        [Display(Name = "已分配")]
        Assigned ,

        [Display(Name = "进行中")]
        Finishing,

        [Display(Name="设计完成")]
        DesignFinish,

        [Display(Name="需要测试")]
        NeedTest,
        
        [Display(Name="需要部署")]
        NeedDeploy,

        [Display(Name = "已完成")]
        Finished ,

        [Display(Name = "已取消")]
        Canceled 
    }


    public enum TaskParticipatorStatus : byte
    {
        [Display(Name = "未分配")]
        Unassigned = 0,

        [Display(Name = "已分配")]
        Assigned = 1,

        [Display(Name = "进行中")]
        Working = 2,

        [Display(Name = "已完成")]
        Finished = 3,

        [Display(Name = "推迟")]
        Delayed = 4,

        [Display(Name = "已取消")]
        Canceled  = 5
    }

}
