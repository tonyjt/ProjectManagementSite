using System;
using System.Collections.Generic;

namespace PMS.Model
{
    public partial class User
    {
        public User()
        {
            this.Projects = new List<Project>();
        }

        public System.Guid UserId { get; set; }
        public string Account { get; set; }
        public string UserName { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
