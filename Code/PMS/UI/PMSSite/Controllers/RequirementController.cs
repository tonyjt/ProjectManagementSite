using PMS.Model;
using PMS.PMSBLL;
using PMS.PMSSite.Models;
using PMS.Tool.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.PMSSite.Controllers
{
    [ValidateInput(false)]
    [ProjectActionFilter]
    public class RequirementController : BaseController
    {
        //
        // GET: /Requirement/

        public ActionResult Index(Guid requirementId)
        {
            Requirement re = RequirementManager.GetRequirement(requirementId);

            RequirementIndexModel model = GetModel(requirementId);

            model.IsNew = false;
            model.Item = re;

            return View("Index",model);
        }

        public ActionResult New(Guid? parentId)
        {
            //if (GuidHelper.IsValid(requirementId))
            //{

            //}
            RequirementIndexModel model = GetModel(GuidHelper.GetInvalidGuid());

            model.IsNew = true;
            model.Item = new Requirement
            {
                ParentId = parentId
            };

            return View("Index",model);
        }
        [HttpPost]
        public ActionResult New(Requirement model)
        {
            bool result = RequirementManager.CreateNewRequirement(model);

            if (result)
            {
                ShowSuccessMessage("新需求添加成功", true);

                return RedirectToAction("index", "requirement", new { requirementId = model.RequirementId });

            }
            else
            {
                ShowErrorMessage("项目删除失败，出现异常");

                RequirementIndexModel updateModel = GetModel(GuidHelper.GetInvalidGuid());

                updateModel.IsNew = true;
                updateModel.Item = model;

                return View("Index", updateModel);
            }
            
        }

        private RequirementIndexModel GetModel(Guid requirementId)
        {
            RequirementIndexModel model = new RequirementIndexModel
            {
                
                AllRequirement =  RequirementManager.GetAllRequirement(this.ProjectId)
            };

            model.ParentableRequirement = RequirementManager.GetParentableRequirement(model.AllRequirement, requirementId);

            model.StartVersion = VersionManager.GetStartVersion(this.ProjectId).OrderByDescending(p=>p.StartTime);

            return model;
        }
    
    }
}
