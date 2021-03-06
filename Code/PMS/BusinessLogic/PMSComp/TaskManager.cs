﻿using log4net;
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

                task.StatusEnum = GetTaskStatus(task);

                task.RequirementId = GuidHelper.SetNullable(task.RequirementId);
                
                task.History = string.Format("{0}在{1}创建任务",UserManager.GetCurrentUser().Account,task.CreateTime.ToStandardString());
                if (string.IsNullOrEmpty(task.Content)) task.Content = " ";

                return ManagerHelper.ActionVoid(task, dataAccess.CreateTask, log);
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

        public static IEnumerable<ProjectTask>  SearchTask(Guid projectId,string version,string requirement,IEnumerable<ProjectTaskStatus> statusList,RoleEnum? role, string  user,out int totalCount, int pageSize = 10,int pageIndex =1)
        {
            
                Guid versionId; Guid requirementId; Guid userId;

                versionId = VersionManager.GetVersionId(projectId,version);
                requirementId = RequirementManager.GetRequirementId(projectId, requirement);
                userId = UserManager.GetUserId(user);

                return SearchTask(projectId, userId, versionId, requirementId, statusList, role, out totalCount, pageSize, pageIndex);
        
        }

        public static IEnumerable<ProjectTask> SearchTask(Guid projectId, Guid userId, Guid versionId, Guid requirementId, IEnumerable<ProjectTaskStatus> statusList, RoleEnum? role, out int totalCount, int pageSize = 10, int pageIndex = 1)
        {
            IEnumerable<byte> statusByteList = statusList.Select(s => s.GetByteEnumValue());

            short shortRole = role.HasValue ? (short)role.Value : (short)0;

            try
            {
                return dataAccess.SearchTask(projectId, versionId, requirementId, statusByteList, shortRole, userId, out totalCount, pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                totalCount = 0;
                return null;
            }
        }

        public static ProjectTask GetTask(Guid taskId,bool nullable = false)
        {
            ProjectTask task = ManagerHelper.GetModel(taskId,dataAccess.GetTask,log);

            return task != null && task.StatusEnum != ProjectTaskStatus.Canceled? task :null;
        }

        public static bool EnableRole(Guid taskId, RoleEnum role)
        {
            ProjectTask task = GetTask(taskId);

            if(task != null)
            {
                if (!task.ContainRole(role)) 
                {
                    TaskParticipator tpNew = new TaskParticipator
                    {
                        TaskParticipatorId = Guid.NewGuid(),
                        TaskId = taskId,
                        RoleEnum = role,
                        StatusEnum = TaskParticipatorStatus.Unassigned
                    };
                    if (ManagerHelper.ActionVoid(tpNew, dataAccess.EnableRole, log))
                    {
                        return SetTaskStatus(taskId);
                    }
                }
            }

            return false;
        }

        public static bool DisableRole(Guid taskId, RoleEnum role)
        {
            ProjectTask task = GetTask(taskId);

            if (task != null)
            {
                TaskParticipator tp = task.TaskParticipators.Where(p => p.RoleEnum == role).FirstOrDefault();

                if (tp != null)
                {
                    TaskParticipator tpDelete = new TaskParticipator
                    {
                        TaskParticipatorId = tp.TaskParticipatorId
                    };

                    if (ManagerHelper.ActionVoid(tpDelete, dataAccess.DisableRole, log))
                    {
                        return SetTaskStatus(taskId);

                    }
                }
            }
            return false;
        }

        public static bool AssignRole(Guid taskId, RoleEnum role,Guid userId)
        {
            ProjectTask task = GetTask(taskId);

            if (task != null)
            {
                TaskParticipator tp = task.TaskParticipators.Where(p => p.RoleEnum == role).FirstOrDefault();

                if (tp != null)
                {
                    tp.UserId = GuidHelper.SetNullable(userId);
                    tp.StatusEnum = TaskParticipatorStatus.Assigned;
                    if (ManagerHelper.ActionVoid(tp, dataAccess.AssignRole, log))
                    {
                        return SetTaskStatus(taskId);
                    }
                }
            }
            return false;
        }

        private static bool CheckTaskRoleStatus(TaskParticipator tp, TaskParticipatorStatus status)
        {
            //TaskParticipatorStatus status = tp.StatusEnum;

            bool result = false;

            if (tp != null)
            {
                if (status == TaskParticipatorStatus.Unassigned)
                {
                    if (!tp.UserId.HasValue) result = true;
                }
                else if (status == TaskParticipatorStatus.Assigned)
                {
                    if (tp.StatusEnum == TaskParticipatorStatus.Unassigned && tp.UserId.HasValue) result = true;
                }

                else if (status == TaskParticipatorStatus.Working)
                {
                    if (tp.StatusEnum == TaskParticipatorStatus.Assigned || tp.StatusEnum == TaskParticipatorStatus.Delayed)
                    {
                        result = true;
                    }
                }
                else if (status == TaskParticipatorStatus.Delayed)
                {
                    if (tp.StatusEnum == TaskParticipatorStatus.Assigned || tp.StatusEnum == TaskParticipatorStatus.Working)
                    {
                        result = true;
                    }
                }
                else if (status == TaskParticipatorStatus.Finished)
                {
                    if (tp.StatusEnum == TaskParticipatorStatus.Working)
                    {
                        result = true;
                    }
                }
                else if (status == TaskParticipatorStatus.Canceled)
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool SetTaskStatus(Guid taskId)
        {
            ProjectTask task = GetTask(taskId);

            return SetTaskStatus(task);
        }

        public static bool SetTaskStatus(ProjectTask task)
        {
            if (task != null)
            {
                ProjectTaskStatus status = GetTaskStatus(task);

                task.StatusEnum = status;

                return UpdateTaskStatus(task);
            }
            else
                return false;
        }

        public static bool UpdateTaskStatus(ProjectTask task)
        {
            return ManagerHelper.GetModel(task.TaskId, task.StatusEnum, dataAccess.UpdateTaskStatus, log);
        }

        public static ProjectTaskStatus GetTaskStatus(ProjectTask task)
        {
            if (task == null) return ProjectTaskStatus.Unassigned;

            List<TaskParticipator> tps  = task.TaskParticipators.ToList();

            ProjectTaskStatus status =task.StatusEnum;

            if (status != ProjectTaskStatus.Canceled)
            {
                int countFinished = tps.Where(t => t.StatusEnum == TaskParticipatorStatus.Finished).Count();

                if (countFinished == tps.Count())
                {
                    status = ProjectTaskStatus.Finished;
                }
                else if (tps.Exists(t => t.RoleEnum == RoleEnum.Operator) && tps.Exists(t => t.RoleEnum == RoleEnum.Tester && t.StatusEnum == TaskParticipatorStatus.Finished))
                {
                    status = ProjectTaskStatus.NeedDeploy;
                }
                else if (tps.Exists(t => t.RoleEnum == RoleEnum.Tester) && tps.Exists(t => t.RoleEnum == RoleEnum.Developer && t.StatusEnum == TaskParticipatorStatus.Finished))
                {
                    status = ProjectTaskStatus.NeedTest;
                }
                else if (tps.Exists(t => t.RoleEnum == RoleEnum.Designer && t.StatusEnum == TaskParticipatorStatus.Finished))
                {
                    status = ProjectTaskStatus.DesignFinish;
                }
                else
                {
                    int countStart = tps.Where(t => t.StatusEnum == TaskParticipatorStatus.Working).Count();

                    if (countStart > 0)
                    {
                        status = ProjectTaskStatus.Finishing;
                    }
                    else
                    {

                        int unAssignedCount = tps.Where(t => t.StatusEnum == TaskParticipatorStatus.Unassigned).Count();

                        if (unAssignedCount == tps.Count())

                            status = ProjectTaskStatus.Unassigned;

                        else if (unAssignedCount == 0)
                        {
                            status = ProjectTaskStatus.Assigned;
                        }

                        else
                        {
                            status = ProjectTaskStatus.Assigning;
                        }
                    }
                }
            }
            return status;
        }

        //private static bool SetHistory(Task task, 

        public static bool SetTaskRoleStatus(Guid taskId, RoleEnum role, TaskParticipatorStatus status)
        {
            bool result = false;
            ProjectTask task = GetTask(taskId);

            if (task != null)
            {
                TaskParticipator tp = task.TaskParticipators.Where(p => p.RoleEnum == role).FirstOrDefault();

                result = CheckTaskRoleStatus(tp, status);


                if (result)
                {
                    tp.StatusEnum = status;
                    result = ManagerHelper.ActionBool(tp, dataAccess.UpdateTaskRole, log);

                    if (result)
                    {
                        result = SetTaskStatus(task);

                    }
                }
            }
            return result;
        }

        public static bool Cancel(Guid taskId)
        {
            bool result;

            if (GuidHelper.IsValid(taskId))
            {
                ProjectTask task = GetTask(taskId, true);

                if (task == null) result = false;

                else if (task.StatusEnum == ProjectTaskStatus.Canceled) result = true;

                else
                {
                    task.StatusEnum = ProjectTaskStatus.Canceled;

                    result = UpdateTaskStatus(task);
                }

            }
            else
                result = false;

            return result;
        }

        public static IEnumerable<ProjectTask> GetMyRunningTask(Guid projectId, Guid userId)
        {
            Guid emptyGuid = GuidHelper.GetInvalidGuid();

            IEnumerable<ProjectTaskStatus> statusList = new List<ProjectTaskStatus>{
                ProjectTaskStatus.Assigning,
                ProjectTaskStatus.Assigned,
                ProjectTaskStatus.DesignFinish,
                ProjectTaskStatus.Finishing,
                ProjectTaskStatus.NeedDeploy,
                ProjectTaskStatus.NeedTest
            };

            int totalCount;

            IEnumerable<ProjectTask> tasks = SearchTask(projectId, userId, emptyGuid, emptyGuid, statusList, null, out totalCount, 100000, 1);

            List<ProjectTask> myTasks = new List<ProjectTask>();

            if (tasks != null)
            {
                IEnumerable<TaskParticipatorStatus> runningStatuses = GetRunningTaskParticipatorStatuses();
                foreach (ProjectTask item in tasks)
                {
                    if (item.TaskParticipators.Select(p => p.UserId == userId && runningStatuses.Contains(p.StatusEnum)).Count() > 0)
                    {
                        myTasks.Add(item);
                    }
                }
            }
            return myTasks;
        }

        public static IEnumerable<TaskParticipatorStatus> GetRunningTaskParticipatorStatuses()
        {
            return new List<TaskParticipatorStatus>{
                TaskParticipatorStatus.Assigned ,
                TaskParticipatorStatus.Delayed ,
                TaskParticipatorStatus.Working
            };
        }

        public static IEnumerable<RoleEnum> GetMyRunningRoles(IEnumerable<ProjectTask> tasks, Guid userId)
        {
            if(tasks == null) return new List<RoleEnum>();

            IEnumerable<TaskParticipatorStatus> runningStatuses = GetRunningTaskParticipatorStatuses();

            var tp = tasks.SelectMany(p=>p.TaskParticipators.Where(t=> t.UserId == userId && runningStatuses.Contains(t.StatusEnum)));

            return tp.Select(p => p.RoleEnum).Distinct();

        }
    }
}
