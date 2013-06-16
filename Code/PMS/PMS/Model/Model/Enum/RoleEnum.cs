using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{

    [Flags]
    public enum RoleEnum:byte
    {
        Designer = 1,

        Developer = 2,

        Tester = 4,

        Operator = 8
    }
}
