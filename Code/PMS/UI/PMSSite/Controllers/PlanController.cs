using PMS.PMSSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.PMSSite.Controllers
{
    [ProjectActionFilter]
    public class PlanController : BaseController
    {
        //
        // GET: /Plan/

        public ActionResult Index()
        {
            return View();
        }

    }
}
