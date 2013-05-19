using log4net;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.PMSBLL
{
    public class UserManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UserManager).Name);


        public static User GetCurrentUser()
        {
            return new User
            {
                UserId = new Guid("b6b40c01-5997-42fe-b669-447fc1cea267"),
                Account = "liujiangtao@idfsoft.com",
                UserName = "tonyjt"
            };
        }

        public static Guid GetCurrentUserId()
        {
            User user = GetCurrentUser();
            return user != null ? user.UserId : Guid.Empty;
        }
    }
}
