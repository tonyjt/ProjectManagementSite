using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.PMSSite.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected User CurrentUser
        {
            get
            {
                return new User
                {
                    UserId = new Guid("b6b40c01-5997-42fe-b669-447fc1cea267"),
                    Account = "liujiangtao@idfsoft.com",
                    UserName = "tonyjt"
                };
            }
        }
    }
}
