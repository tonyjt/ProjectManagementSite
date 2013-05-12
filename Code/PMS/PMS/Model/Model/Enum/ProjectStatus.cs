using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model.Enum
{
    public enum ProjectStatus:byte
    {
        Ready = 1,

        Start = 2,

        Pause = 3,

        Stop = 4,

        Delete = 5
    }
}
