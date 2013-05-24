using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class VersionIndexModel
    {
        public IEnumerable<ProjectVersion> VersionList { get; set; }

        public IEnumerable<ProjectVersion> VersionHistory { get; set; }
    }

    public class VersionCreateModel
    {
        public string Name { get; set; }

        
    }
}