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
        public ActionResult Index()
        {
            RequirementModel model = new RequirementModel
            {
                RequirementChildren = RequirementManager.GetRequirementWithChildren(this.ProjectId)
            };
            return View("Index",model);
        }

        public ActionResult Detail(Guid? requirementId)
        {
            RequirementIndexModel model ;
            if (requirementId.HasValue)
            {
                Requirement re = RequirementManager.GetRequirement(requirementId.Value);

                model = GetIndexModel(requirementId.Value);

                model.IsNew = false;
                model.Item = re;

                return View("Detail", model);
            }
            else
            {
                return RedirectToAction("new");
            }
        }
        [HttpPost]
        public ActionResult Detail(Requirement model)
        {
            bool result = RequirementManager.Save(model);

            if (result)
            {
                ShowSuccessMessage("保存成功");
            }
            else
            {
                ShowErrorMessage("保存失败，出现异常：请检查输入项");

            }
            return Detail(model.RequirementId);
        }

        public ActionResult New(Guid? parentId)
        {
            if (!parentId.HasValue)
            {
                parentId = GuidHelper.GetInvalidGuid();
            }
            RequirementIndexModel model = GetIndexModel(GuidHelper.GetInvalidGuid());

            model.IsNew = true;
            model.Item = new Requirement
            {
                ParentId = parentId.Value
            };

            return View("Detail",model);
        }
        [HttpPost]
        public ActionResult New(Requirement model)
        {
            bool result = RequirementManager.CreateNewRequirement(model);

            if (result)
            {
                ShowSuccessMessage("新需求添加成功", true);

                return RedirectToAction("detail", "requirement", new { requirementId = model.RequirementId });

            }
            else
            {
                ShowErrorMessage("需求添加失败，出现异常：请检查输入项");

                RequirementIndexModel updateModel = GetIndexModel(GuidHelper.GetInvalidGuid());

                updateModel.IsNew = true;
                updateModel.Item = model;

                return View("Detail", updateModel);
            }
            
        }

        private RequirementIndexModel GetIndexModel(Guid requirementId)
        {
            RequirementIndexModel model = new RequirementIndexModel
            {
                AllRequirement = RequirementManager.GetAllRequirement(this.ProjectId)
            };

            model.ParentableRequirement = RequirementManager.GetParentableRequirement(model.AllRequirement, requirementId);

            model.StartVersion = VersionManager.GetStartVersion(this.ProjectId).OrderByDescending(p=>p.StartTime);

            model.RequirementChildren = GetModel(model.AllRequirement).RequirementChildren;

            return model;
        }

        private RequirementModel GetModel(IEnumerable<Requirement> requirements)
        {
            RequirementModel model = new RequirementModel();
            model.RequirementChildren = RequirementManager.GetRequirementWithChildren(requirements);
            return model;
        }

    }
}
