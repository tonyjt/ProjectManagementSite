using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class BaseDetailModel
    {
        public bool IsNew { get; set; }
    }

    public class AjaxResult
    {
        public bool Success { get; set; }

        public bool Redirect { get; set; }

        public string RedirectUrl { get; set; }

        public string Message { get; set; }
    }
}