using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class Project
    {
        public Project()
        {
            this.ProjectParticipators = new List<ProjectParticipator>();
            this.ProjectTasks = new List<ProjectTask>();
            this.ProjectVersions = new List<ProjectVersion>();
        }

        public System.Guid ProjectId { get; set; }
        public string Name { get; set; }
        public System.Guid Creator { get; set; }
        public System.DateTime CreateTime { get; set; }
        public byte Status { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProjectParticipator> ProjectParticipators { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
        public virtual ICollection<ProjectVersion> ProjectVersions { get; set; }
    }
}
