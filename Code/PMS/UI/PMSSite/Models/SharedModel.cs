using MVCExtension;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.PMSSite.Models
{
    public class SharedModel
    {
    }

    public class SharedPagerModel
    {
        public string Action { get; set; }

        

        public HtmlHelper Html { get; set; }

        public PagerModel PagerModel { get; set; }
    }
}