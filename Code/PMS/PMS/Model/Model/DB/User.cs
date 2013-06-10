using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class User
    {
        public User()
        {
            this.Projects = new List<Project>();
            this.ProjectParticipators = new List<ProjectParticipator>();
            this.ProjectTasks = new List<ProjectTask>();
            this.ProjectVersions = new List<ProjectVersion>();
            this.Requirements = new List<Requirement>();
            this.RequirementHistories = new List<RequirementHistory>();
            this.TaskParticipators = new List<TaskParticipator>();
        }

        public System.Guid UserId { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectParticipator> ProjectParticipators { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectVersion> ProjectVersions { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; }
        public virtual ICollection<RequirementHistory> RequirementHistories { get; set; }
        public virtual ICollection<TaskParticipator> TaskParticipators { get; set; }
    }
}
