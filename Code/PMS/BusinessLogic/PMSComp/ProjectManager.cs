using log4net;
using PMS.Model;
using PMS.PMSDBDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExtension;
using PMS.Tool.Helper;
using PMS.Model.Enum;
namespace PMS.PMSBLL
{
    public class ProjectManager
    {
        private static ProjectDAL dataAccess = new ProjectDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(ProjectManager).Name);

        public ProjectManager()
        {
            //dataAccess = new ProjectDAL();
        }

        public static bool AddNewProject(Project newProject)
        {
            try
            {
                if (newProject == null) return false;

                if (!GuidHelper.IsValid(newProject.ProjectId))
                {
                    newProject.ProjectId = Guid.NewGuid();
                }
                newProject.CreateTime = DateTime.Now;
                newProject.ProjectStatus = ProjectStatus.Ready;
                newProject.Description = "";

                dataAccess.AddNewProject(newProject);

                return true;
            }
            catch(Exception ex)
            {
                log.ErrorInFunction(ex);
                return false;
            }
        }

        public static IEnumerable<Project> GetAllProjects()
        {
            try
            {
                IEnumerable<Project> projects = dataAccess.GetAllProjects();

                projects = projects.Where(p => p.ProjectStatus != ProjectStatus.Delete);

                return projects;
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                return new List<Project>();
            }
        }
    }
}
