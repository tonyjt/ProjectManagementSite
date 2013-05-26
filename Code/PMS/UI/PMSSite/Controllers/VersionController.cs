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
    public class VersionController : BaseController
    {
        //
        // GET: /Version/

        public ActionResult Index()
        {
            IEnumerable<ProjectVersion> versionList = VersionManager.GetVersionForProject(ProjectId);

            VersionIndexModel model = new VersionIndexModel
            {
                VersionList = versionList.Where(p=>p.VersionStatus != Model.Enum.VersionStatus.Stop).OrderByDescending(p=>p.CreateTime),
                VersionHistory = versionList.Where(p => p.VersionStatus == Model.Enum.VersionStatus.Stop).OrderByDescending(p => p.CreateTime)
            };

            return View("Index",model);
        }
        public ActionResult List(Guid versionId, string op)
        {
            ActionResult result;
            switch (op.ToLower())
            {
                case "start":
                    result = Start(versionId);
                    break;
 
                case "stop":
                    result = Stop(versionId);
                    break;
                case "delete":
                    result = Delete(versionId);
                    break;
                case "restart":
                    result = Restart(versionId);
                    break;
                default:
                    result = Index();
                    break;
            }
            op = "";
            return result;
        }

        private ActionResult Start(Guid versionId)
        {
            bool result = VersionManager.StartVersion(versionId);

            if (result)
            {
                ShowSuccessMessage("版本启动成功");

            }
            else
            {
                ShowErrorMessage("版本启动失败，出现异常");

            }
            return Index(); 
        }
        private ActionResult Stop(Guid versionId)
        {
            bool result = VersionManager.StopVersion(versionId);

            if (result)
            {
                ShowSuccessMessage("版本结束成功");

            }
            else
            {
                ShowErrorMessage("版本结束失败，出现异常");

            }
            return Index();
        }
        private ActionResult Delete(Guid versionId)
        {
            bool result = VersionManager.DeleteVersion(versionId);

            if (result)
            {
                ShowSuccessMessage("版本删除成功");

            }
            else
            {
                ShowErrorMessage("版本删除失败，出现异常");

            }
            return Index();
        }
        private ActionResult Restart(Guid versionId)
        {
            bool result = VersionManager.StartVersion(versionId);

            if (result)
            {
                ShowSuccessMessage("版本重新开始成功");

            }
            else
            {
                ShowErrorMessage("版本重新开始失败，出现异常");

            }
            return Index();
        }
        public ActionResult Create(VersionCreateModel model)
        {
            string message;
            if (model == null)
            {
                message = "信息有误";
            }
            else
            {
                ProjectVersion version = new ProjectVersion
                {
                    VersionName = model.Name,
                    Creator = CurrentUserId,
                    ProjectId  = ProjectId
                };

                if (VersionManager.CreateVersion(version))
                {
                    ShowSuccessMessage("创建版本成功",true);

                    return RedirectToAction("index");
                }
                else
                {
                    message = "信息有误";
                }
            }
            ShowErrorMessage(string.Format("创建版本失败:{0}",message),true);

             return RedirectToAction("index");
        }
    }
}
