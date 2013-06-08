using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class RequirementModel
    {

        

        public IEnumerable<RequirementWithChildren> RequirementChildren { get; set; }
    }

    public class RequirementDetailModel : RequirementModel
    {
        public bool IsNew { get; set; }

        public Requirement Item { get; set; }

        public IEnumerable<Requirement> AllRequirement { get; set; }

        public IEnumerable<Requirement> ParentableRequirement { get; set; }

        public IEnumerable<ProjectVersion> StartVersion { get; set; }

        public IEnumerable<RequirementHistory> HistoryArray { get; set; }
    }
}