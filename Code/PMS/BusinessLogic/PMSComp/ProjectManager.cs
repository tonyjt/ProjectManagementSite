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
            Project project = ManagerHelper.GetModel<Project>(projectId, dataAccess.GetProject, log);

            return GetProjectNullable(project, nullable);
        }

        public static Project GetProject(string projectName, bool nullable = false)
        {
            Project project = ManagerHelper.GetModel<Project>(projectName,dataAccess.GetProject, log);

            return GetProjectNullable(project, nullable);
        }

        private static Project GetProjectNullable(Project project, bool nullable)
        {
            if (!nullable && project != null)
            {
                if (project.ProjectStatus == ProjectStatus.Delete)
                    project = null;
            }
            return project;
        }

        public static ProjectParticipator GetLastJoinProjectForUser(Guid userId)
        {
            IEnumerable<ProjectParticipator> projectList = GetAllProjectsForUser(userId);

            if (projectList != null && projectList.Count() > 0)
                return projectList.OrderByDescending(p => p.JoinTime).FirstOrDefault();
            else
                return null;
        }

        public static IEnumerable<ProjectParticipator> GetAllProjectsForUser(Guid userId)
        {
            IEnumerable<ProjectParticipator> projectList = ManagerHelper.GetModel<IEnumerable<ProjectParticipator>>(userId, dataAccess.GetAllProjectForUser, log);

            foreach (ProjectParticipator pp in projectList)
            {
                pp.Project = GetProjectNullable(pp.Project, false);
            }
            return projectList.Where(p => p.Project != null);
        }

        public static IEnumerable<Project> GetOtherProjectsForUser(Guid userId, Guid projectId)
        {
            List<ProjectParticipator> projectList = GetAllProjectsForUser(userId).ToList();


            if (GuidHelper.IsValid(projectId))
            {
                ProjectParticipator pp = projectList.Find(p => p.ProjectId == projectId);
                if (pp != null)
                {
                    projectList.Remove(pp);
                }
            }
            return projectList.Select(p=>p.Project);
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
