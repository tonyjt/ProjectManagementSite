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
    public class PlanController : BaseController
    {
        //
        // GET: /Plan/

        public ActionResult Index(Guid? tpId)
        {
            //PlanNewModel

            IEnumerable<ProjectTask> myTasks = TaskManager.GetMyRunningTask(ProjectId, CurrentUserId);

            IEnumerable<RoleEnum> myRoles = TaskManager.GetMyRunningRoles(myTasks, CurrentUserId).OrderBy(t=>t);

            TaskParticipator tp;

            if (tpId.HasValue)
            {
                tp = myTasks.SelectMany(p => p.TaskParticipators.Where(t => t.TaskParticipatorId == tpId.Value)).FirstOrDefault();
            }
            else
            {
                tp = null;
            }
            PlanNewModel newModel = new PlanNewModel
            {
                SelectedTaskParticipator = tp,

                Tasks = GetPlanTaskModels(myTasks,CurrentUserId),

                Roles = myRoles,

                SelectedRole  = tp != null ?tp.RoleEnum : RoleEnum.Designer
            };


            PlanIndexModel model = new PlanIndexModel
            {
                NewModel = newModel
            };
            return View("index",model);
        }


        private IEnumerable<PlanTaskModel> GetPlanTaskModels(IEnumerable<ProjectTask> tasks,Guid userId)
        {
            List<PlanTaskModel>  models = new List<PlanTaskModel>(); 
            if (tasks == null) return models;
            IEnumerable<TaskParticipatorStatus> runningStatuses =  TaskManager.GetRunningTaskParticipatorStatuses();

            var tps =  tasks.SelectMany(p=>p.TaskParticipators.Where(t=>t.UserId == userId && runningStatuses.Contains(t.StatusEnum)));

            foreach(TaskParticipator item in tps)
            {
                models.Add(new PlanTaskModel{
                    Role = item.RoleEnum,
                    TaskParticipatorId = item.TaskParticipatorId,
                    TaskTitle = item.ProjectTask.Title
                });
            }

            return models;
        }

        public ActionResult New(PlanNewPostModel model)
        {
            Plan newPlan = new Plan
            {
                TaskParticipatorId = model.TaskParticipatorId,
                Title = model.Title,
                AllDay = model.AllDay,
                StartTime = model.StartTime,
                EndTime = model.EndTime
            };

            bool result = PlanManager.CreatePlan(newPlan);

            if (result)
            {
                return AjaxShowSuccessMessage("新建计划成功", true, "");
            }
            else
                return AjaxShowErrorMessage("参数有误");
        }
    }
}
