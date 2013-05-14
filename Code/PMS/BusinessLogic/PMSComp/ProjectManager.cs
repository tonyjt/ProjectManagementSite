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

                return projects.OrderByDescending(p=>p.CreateTime);
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                return new List<Project>();
            }
        }

        public static bool StartProject(Guid projectId) 
        {
            IEnumerable<ProjectStatus> referStatus = new List<ProjectStatus>{
               ProjectStatus.Ready,
               ProjectStatus.Pause,
               ProjectStatus.Stop
           };

           return UpdateProjectStatus(projectId,ProjectStatus.Start,referStatus);
        }
        public static bool PauseProject(Guid projectId)
        {
            IEnumerable<ProjectStatus> referStatus = new List<ProjectStatus>{
               ProjectStatus.Start
           };

            return UpdateProjectStatus(projectId, ProjectStatus.Pause, referStatus);
        }

        public static bool StopProject(Guid projectId)
        {
            IEnumerable<ProjectStatus> referStatus = new List<ProjectStatus>{
               ProjectStatus.Start,
               ProjectStatus.Pause
           };

            return UpdateProjectStatus(projectId, ProjectStatus.Stop, referStatus);
        }

        public static bool DeleteProject(Guid projectId)
        {
            IEnumerable<ProjectStatus> referStatus = new List<ProjectStatus>{
               ProjectStatus.Ready,
               ProjectStatus.Start,
               ProjectStatus.Pause,
               ProjectStatus.Stop
           };

            return UpdateProjectStatus(projectId, ProjectStatus.Delete, referStatus);
        }

        public static bool UpdateProjectStatus(Guid projectId, ProjectStatus status,IEnumerable<ProjectStatus> referStatus)
        {
            if (GuidHelper.IsValid(projectId))
            {
                Project project = GetProject(projectId);

                if (project != null)
                {
                    if (referStatus.Contains(project.ProjectStatus))
                    {
                        project.ProjectStatus = status;

                        return UpdateProject(project);
                    }
                }
            }
            return false;
        }

        public static Project GetProject(Guid projectId,bool nullable = false)
        {
            try
            {
                Project project = dataAccess.GetProject(projectId);
                if (!nullable && project!=null)
                {
                    if (project.ProjectStatus == ProjectStatus.Delete)
                        project = null;
                }
                return project;
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                return null;
            }
        }

        public static bool UpdateProject(Project project)
        {
            try
            {
                return dataAccess.UpdateProject(project);
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                return false;
            }
        }
    }
}
