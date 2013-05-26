using PMS.Model;
using PMS.PMSBLL;
using PMS.PMSSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.PMSSite.Controllers
{
    [ProjectActionFilter]
    
    public class ProjectController : BaseController
    {
        
        public ActionResult Index()
        {
            ProjectIndexModel model = new ProjectIndexModel
            {
                Projects = ProjectManager.GetAllProjects()
            };


            return View("Index",model);
        }

        public ActionResult List(Guid projectId, string op)
        {
            ActionResult result;
            switch (op.ToLower())
            {
                case "start":
                     result = Start(projectId);
                    break;
                case "pause":
                    result = Pause(projectId);
                    break;
                case "stop":
                    result = Stop(projectId);
                    break;
                case "delete":
                    result = Delete(projectId);
                    break;
                default:
                    result = Index();
                    break;
            }
            op = "";
            return result;
        }
        [HttpGet]
        public ActionResult New()
        {
            ProjectModel model = new ProjectModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult New(ProjectModel model)
        {
            Project project = GetProjectFromModel(model);

            project.Creator = CurrentUser.UserId;

            bool result = ProjectManager.AddNewProject(project);

            if (result)
            {
                ShowSuccessMessage("新项目添加成功",true);
                //show
                return RedirectToAction("index");
            }
            else
            {
                //show error
                ShowErrorMessage("新项目添加失败，出现异常");
            }

            return View(model);
        }


        public ActionResult Start(Guid projectId)
        {
            bool result = ProjectManager.StartProject(projectId);

            if (result)
            {
                ShowSuccessMessage("项目启动成功");

            }
            else
            {
                ShowErrorMessage("项目启动失败，出现异常");

            }
            return Index();
        }

        public ActionResult Pause(Guid projectId)
        {
            bool result = ProjectManager.PauseProject(projectId);

            if (result)
            {
                ShowSuccessMessage("项目暂停成功");

            }
            else
            {
                ShowErrorMessage("项目暂停失败，出现异常");

            }
            return Index();
        }

        public ActionResult Stop(Guid projectId)
        {
            bool result = ProjectManager.StopProject(projectId);

            if (result)
            {
                ShowSuccessMessage("项目停止成功");

            }
            else
            {
                ShowErrorMessage("项目停止失败，出现异常");

            }
            return Index();
        }

        public ActionResult Delete(Guid projectId)
        {
            bool result = ProjectManager.DeleteProject(projectId);

            if (result)
            {
                ShowSuccessMessage("项目删除成功");

            }
            else
            {
                ShowErrorMessage("项目删除失败，出现异常");

            }
            return Index();
        }

   


        private Project GetProjectFromModel(ProjectModel model)
        {
            return new Project
            {
                Name = model.Name,
                StartDate = model.StartDate
            };
        }
    }
}
