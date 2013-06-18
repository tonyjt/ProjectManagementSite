using log4net;
using PMS.Model;
using PMS.PMSDBDataAccess;
using PMS.Tool.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomExtension;
using CustomExtension.Helper;

namespace PMS.PMSBLL
{
    public class TaskManager
    {
        private static TaskDAL dataAccess = new TaskDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(ProjectManager).Name);


        public static bool CreateTask(ProjectTask task)
        {
            if (task != null)
            {
                task.Creator = UserManager.GetCurrentUserId();

                task.TaskId = Guid.NewGuid();

                task.TaskParticipators = InitParticipators(task.TaskParticipators,task);

                task.CreateTime = DateTime.Now;
                task.UpdateTime = DateTime.Now;

                task.StatusEnum = GetTaskStatus(task.TaskParticipators);

                task.RequirementId = GuidHelper.SetNullable(task.RequirementId);
                
                task.History = string.Format("{0}在{1}创建任务",UserManager.GetCurrentUser().Account,task.CreateTime.ToStandardString());
                if (string.IsNullOrEmpty(task.Content)) task.Content = " ";

                return ManagerHelper.CreateModel(task, dataAccess.CreateTask, log);
            }
            else
                return false;
        }

        public static ICollection<TaskParticipator> InitParticipators(ICollection<TaskParticipator> tps,ProjectTask task)
        {
            foreach (TaskParticipator tp in tps)
            {
                if (tp.UserId.HasValue && GuidHelper.IsValid(tp.UserId.Value))
                {
                    tp.StatusEnum = TaskParticipatorStatus.Assigned;
                }
                else
                    tp.StatusEnum = TaskParticipatorStatus.Unassigned;

                tp.TaskId = task.TaskId;
                tp.TaskParticipatorId = Guid.NewGuid();
                tp.UserId = GuidHelper.SetNullable(tp.UserId);
            }
            return tps;
        }

        public static ProjectTaskStatus GetTaskStatus(ICollection<TaskParticipator> tps)
        {
            ProjectTaskStatus status = ProjectTaskStatus.Unassigned;

            int unAssignedCount = tps.Where(t => t.StatusEnum == TaskParticipatorStatus.Unassigned).Count();

            if (unAssignedCount == tps.Count())
                status = ProjectTaskStatus.Unassigned;
            else
            {
                int assignedCount = tps.Where(t => t.StatusEnum == TaskParticipatorStatus.Assigned).Count();

                if (assignedCount == tps.Count())
                {
                    status = ProjectTaskStatus.Assigned;
                }
                else if(assignedCount>0)
                {
                    status = ProjectTaskStatus.Assigning;
                }
            }

            return status;
        }

        public static ICollection<TaskParticipator> MergeParticipators(IList<TaskParticipator> tps)
        {
            if(tps != null)
            {
                for(int i =0;i<tps.Count();i++)
                {
                    if (tps[i].UserId.HasValue&&GuidHelper.IsValid(tps[i].UserId.Value))
                    {
                        IEnumerable<TaskParticipator> samePeople = tps.Where(p => p.UserId == tps[i].UserId);

                        foreach (TaskParticipator tp in samePeople)
                        {
                            tps[i].RoleEnum = tps[i].RoleEnum | tp.RoleEnum;

                            tps.Remove(tp);
                        }
                    }
                }
            }

            return tps;
        }

        public static IEnumerable<ProjectTask>  SearchTask(string version,string requirement,IEnumerable<ProjectTaskStatus> statusList, string  user,out int totalCount, int pageSize = 10,int pageIndex =1)
        {
            try
            {
                Guid versionId; Guid requirementId; Guid userId;

                versionId = VersionManager.GetVersionId(version);
                requirementId = RequirementManager.GetRequirementId(requirement);
                userId = UserManager.GetUserId(user);
                IEnumerable<byte> statusByteList = statusList.Select(s => s.GetByteEnumValue());

                IEnumerable<ProjectTask> tasks = dataAccess.SearchTask(versionId, requirementId, statusByteList, userId, out totalCount, pageSize, pageIndex);

                return tasks;
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);

                totalCount = 0;

                return new List<ProjectTask>();
            }
        }
    }
}
