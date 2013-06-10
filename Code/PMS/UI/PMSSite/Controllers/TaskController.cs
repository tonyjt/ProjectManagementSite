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
    public class TaskController : BaseController
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(Guid? requirementId)
        {

            TaskDetailModel model = new TaskDetailModel
            {
                Item = new Model.ProjectTask
                {
                    RequirementId = requirementId,
                    
                },
                IsNew = true,
                Requirements = RequirementManager.GetAllRequirement(this.ProjectId),
                Paticipators = UserManager.GetProjectParticipators(this.ProjectId)
            };

            return View("Detail", model);
        }


    }
}
