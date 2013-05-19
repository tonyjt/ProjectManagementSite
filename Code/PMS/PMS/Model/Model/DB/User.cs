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
        }

        public System.Guid UserId { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<ProjectParticipator> ProjectParticipators { get; set; }
    }
}
