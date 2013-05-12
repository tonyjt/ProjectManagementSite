using PMS.Model;
using PMS.PMSDBDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace PMS.PMSDBDataAccess
{
    public class ProjectDAL
    {
        public void AddNewProject(Project newProject)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.Projects.Add(newProject);
                context.SaveChanges();
            }
        }

        public IEnumerable<Project> GetAllProjects()
        {
            using (PMSDBContext context = new PMSDBContext())
            {

                var projects = from p in context.Projects
                               .Include(u=>u.User)
                               select p;

                return projects.ToArray();
            }
        }
    }
}
