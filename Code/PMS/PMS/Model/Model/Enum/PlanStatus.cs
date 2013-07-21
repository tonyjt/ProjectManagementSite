using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public enum  PlanStatus : byte
    {
        New = 1,

        Apllying = 2,

        Approved = 3,

        Finished = 4,

        Expired = 5
    }
}
