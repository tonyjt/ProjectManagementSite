using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Tool.Helper
{
    public class GuidHelper
    {
        public static bool IsValid(Guid guid)
        {
            return guid != null && guid != Guid.Empty;
        }
    }
}
