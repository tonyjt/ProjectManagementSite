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
                VersionList = versionList.Where(p=>p.VersionStatus != Model.Enum.VersionStatus.Stop),
                VersionHistory = versionList.Where(p=>p.VersionStatus == Model.Enum.VersionStatus.Stop)
            };

            return View("Index",model);
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
                    ShowSuccessMessage("创建版本成功");

                    return Index();
                }
                else
                {
                    message = "信息有误";
                }
            }
            ShowErrorMessage(string.Format("创建版本失败:{0}",message));

            return Index();
        }
    }
}
