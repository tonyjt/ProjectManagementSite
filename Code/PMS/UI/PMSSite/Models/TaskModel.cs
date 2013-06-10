using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    
    public class TaskDetailModel
    {
        public ProjectTask Item { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public IEnumerable<ProjectParticipator> Paticipators { get; set; }

        public bool IsNew { get; set; }
    }
}