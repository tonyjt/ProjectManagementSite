using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{

    [Flags]
    public enum RoleEnum:byte
    {
        [Display(Name = "设计")]
        Designer = 1,

        [Display(Name = "开发")]
        Developer = 2,

        [Display(Name = "测试")]
        Tester = 4,

        [Display(Name = "运维")]
        Operator = 8
    }
}
