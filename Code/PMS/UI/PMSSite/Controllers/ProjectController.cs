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
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            ProjectIndexModel model = new ProjectIndexModel
            {
                Projects = ProjectManager.GetAllProjects()
            };
            return View(model);
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
                //show
                return RedirectToAction("index");
            }
            else
            {
                //show error
            }

            return View(model);
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
