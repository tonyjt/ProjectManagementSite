﻿using PMS.Model;
using PMS.PMSBLL;
using PMS.PMSSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomExtension;
using CustomExtension.Helper;

namespace PMS.PMSSite.Controllers
{
    [ProjectActionFilter]
    public class TaskController : BaseController
    {
        //
        // GET: /Task/

        public ActionResult Index(TaskIndexModel model)
        {

            int totalCount;
            model.Tasks = TaskManager.SearchTask(model.Version, model.Requirement, GetStatus(model.Status), model.User, out totalCount, model.PageSize, model.PageIndex);


            model.TotalRecordCount = totalCount;

            model.Users = UserManager.GetProjectParticipators(this.ProjectId);

            model.Requirements = RequirementManager.GetAllRequirement(this.ProjectId);

            model.Versions = VersionManager.GetVersionForProject(this.ProjectId);

            model.Statuses = EnumHelper.GetList<TaskStatusModel>();

            return View("index",model);
        }
        [HttpGet]
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
        [HttpPost]
        public ActionResult New(TaskDetailPostModel model)
        {
            if(model !=null)
            {
                ICollection<TaskParticipator> tpc = new List<TaskParticipator>();

                if (model.NeedDesigner)
                {
                    tpc.Add(new TaskParticipator(RoleEnum.Designer,model.DesignerId));
                }

                if (model.NeedDeveloper)
                {
                    tpc.Add(new TaskParticipator(RoleEnum.Developer,model.DeveloperId));
                }

                if(model.NeedTester)
                {
                    tpc.Add(new TaskParticipator(RoleEnum.Tester,model.TesterId));
                }

                if(model.NeedOperator)
                {
                    tpc.Add(new TaskParticipator(RoleEnum.Operator,model.OperatorId));
                }

                ProjectTask task = new ProjectTask
                {
                    RequirementId = model.RequirementId,
                    Content = model.Content,
                    Creator = this.CurrentUserId,
                    TaskParticipators = tpc
                };

                bool result = TaskManager.CreateTask(task);

                if (result)
                {
                    string redirectUrl = Url.Action("new","task",new {requirementId = model.RequirementId});
                    return AjaxShowSuccessMessage("创建任务成功", true, redirectUrl);
                }
            }

            return AjaxShowErrorMessage("创建任务失败,参数有误");
        }

        public IEnumerable<ProjectTaskStatus> GetStatus(TaskStatusModel? model)
        {
            List<ProjectTaskStatus> statusList = new List<ProjectTaskStatus>();

            if (model.HasValue)
            {
                switch (model)
                {
                    case TaskStatusModel.NeedAssign:
                        statusList.Add(ProjectTaskStatus.Unassigned);
                        statusList.Add(ProjectTaskStatus.Assigning);
                        break;
                    case TaskStatusModel.Finishing:
                        statusList.Add(ProjectTaskStatus.Assigned);
                        statusList.Add(ProjectTaskStatus.Finishing);
                        break;
                    case TaskStatusModel.Finished:
                        statusList.Add(ProjectTaskStatus.Finished);
                        break;
                    case TaskStatusModel.Cacneled:
                        statusList.Add(ProjectTaskStatus.Canceled);
                        break;
                }
            }

            return statusList;
        }

    }
}
