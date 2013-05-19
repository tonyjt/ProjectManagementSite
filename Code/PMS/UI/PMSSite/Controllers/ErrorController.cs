using PMS.PMSSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.PMSSite.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index(string title,string message)
        {
            ErrorMessage em = new ErrorMessage
            {
                Title = title,
                Message = message
            };
            return View();
        }
    }
}
