using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class PlanNewModel
    {
        public IEnumerable<RoleEnum> Roles { get; set; }

        public IEnumerable<PlanTaskModel> Tasks { get; set; }

        public TaskParticipator SelectedTaskParticipator { get; set; }

        public RoleEnum SelectedRole { get; set; }
    }

    public class PlanNewPostModel
    {
        public Guid TaskParticipatorId { get; set; }

        public string Title { get; set; }

        public bool AllDay { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }

    public class PlanIndexModel
    {
        public PlanNewModel NewModel { get; set; }
    }

    public class PlanTaskModel
    {
        public Guid TaskParticipatorId { get; set; }

        public string TaskTitle { get; set; }

        public RoleEnum Role { get; set; }
    }
}