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
            Requirement requirement = RequirementManager.GetFirstRequirement(this.ProjectId);

            return Detail(requirement!= null?requirement.RequirementId:Guid.Empty);
        }

        public ActionResult Detail(Guid? requirementId)
        {
            RequirementDetailModel model ;
            if (requirementId.HasValue&& GuidHelper.IsValid(requirementId.Value))
            {
                Requirement re = RequirementManager.GetRequirement(requirementId.Value);
                IEnumerable<RequirementHistory> histories = RequirementManager.GetHistories(requirementId.Value);

                model = GetIndexModel(requirementId.Value);

                model.IsNew = false;
                model.Item = re;
                model.HistoryArray = histories;

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
            if (model != null) model.UserId = this.CurrentUserId;

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
            RequirementDetailModel model = GetIndexModel(GuidHelper.GetInvalidGuid());

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
            if (model != null) model.UserId = this.CurrentUserId;

            bool result = RequirementManager.CreateNewRequirement(model);

            if (result)
            {
                ShowSuccessMessage("新需求添加成功", true);

                return RedirectToAction("detail", "requirement", new { requirementId = model.RequirementId });

            }
            else
            {
                ShowErrorMessage("需求添加失败，出现异常：请检查输入项");

                RequirementDetailModel updateModel = GetIndexModel(GuidHelper.GetInvalidGuid());

                updateModel.IsNew = true;
                updateModel.Item = model;

                return View("Detail", updateModel);
            }
            
        }

        public ActionResult Delete(Guid requirementId)
        {
            if (GuidHelper.IsValid(requirementId))
            {
                Requirement re = RequirementManager.GetRequirement(requirementId);

                bool result = RequirementManager.DeleteRequirement(re);

                if (result)
                {
                    ShowSuccessMessage("需求已删除", true);

                    return RedirectToAction("detail", "requirement", new { requirementId = re.ParentId });
                }
                else
                {
                    ShowErrorMessage("需求删除失败，请重新尝试", true);

                    return RedirectToAction("detail", "requirement", new { requirementId = re.RequirementId });
                }
            }
            else
            {
                ShowErrorMessage("参数有误，请重新尝试",true);

                return RedirectToAction("detail", "requirement", new { requirementId = requirementId });
            }
        }

        private RequirementDetailModel GetIndexModel(Guid requirementId)
        {
            RequirementDetailModel model = new RequirementDetailModel
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
