using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public enum VersionStatus:byte
    {
        
        Ready = 1,

        Start = 2,

        Stop = 3,

        Delete = 4
    }
}
