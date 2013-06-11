using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    
    public class TaskDetailModel:TaskDetailPostModel
    {
        public ProjectTask Item { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public IEnumerable<ProjectParticipator> Paticipators { get; set; }

        public bool IsNew { get; set; }
    }

    public class TaskDetailPostModel
    {
        public Guid RequirementId { get; set; }

        public string Content { get; set; }

        public bool NeedDesigner { get; set; }

        public bool NeedDeveloper { get; set; }

        public bool NeedTester { get; set; }

        public bool NeedOperator { get; set; }

        public Guid DesignerId { get; set; }

        public Guid DeveloperId { get; set; }

        public Guid TesterId { get; set; }

        public Guid OperatorId { get; set; }

        public bool Continue { get; set; }
    }
}