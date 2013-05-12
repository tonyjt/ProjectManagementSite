using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

    }

    public class ProjectIndexModel
    {
        public IEnumerable<Project> Projects { get; set; }
    }
}