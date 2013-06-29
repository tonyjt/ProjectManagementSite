using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PMS.Model;
using PMS.PMSDBDataAccess.Models;
using PMS.Tool.Helper;
using MVCExtension;
using System.Data.Entity;
using System.Data;

namespace PMS.PMSDBDataAccess
{
    public class TaskDAL
    {
        public void CreateTask(ProjectTask task)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.ProjectTasks.Add(task);

                context.SaveChanges();
            }
        }

        public IEnumerable<ProjectTask> SearchTask(Guid projectId,Guid versionId, Guid requirementId, IEnumerable<byte> statusList,short role, Guid userId,out int totalCount, int pageSize, int pageIndex) 
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var tasks = from t in context.ProjectTasks
                                .Include(r => r.Requirement)
                                .Include(r => r.TaskParticipators)
                                .Include(r => r.Requirement.ProjectVersion)
                                .Include(r => r.User)
                            where t.ProjectId == projectId
                                &&(versionId == Guid.Empty || (t.Requirement != null && t.Requirement.VersionId == versionId))
                                && (requirementId== Guid.Empty  || t. RequirementId == requirementId)
                                && (userId== Guid.Empty  || t.TaskParticipators.Where(p=> role == 0 || p.Roles == role).Select(p => p.UserId).Contains(userId))
                                && ((statusList.Count() == 0 && t.Status!= (byte)ProjectTaskStatus.Canceled) || statusList.Contains(t.Status))
                            orderby t.CreateTime descending
                            select t;
                IEnumerable<ProjectTask> result = PageHelper.GetDatas<ProjectTask>(tasks, pageIndex, pageSize, out totalCount);

                return result;
            }
        }

        public ProjectTask GetTask(Guid TaskId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                return (from t in context.ProjectTasks
                                .Include(r => r.Requirement)
                                .Include(r => r.TaskParticipators)
                                .Include(r => r.Requirement.ProjectVersion)
                                .Include(t => t.User)
                        where t.TaskId == TaskId
                        select t).FirstOrDefault();
            }
        }

        public void EnableRole(TaskParticipator tp)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.TaskParticipators.Add(tp);

                context.SaveChanges();
            }
        }

        public void DisableRole(TaskParticipator tp)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.Entry(tp).State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
        public void AssignRole(TaskParticipator tp)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var model = (from t in context.TaskParticipators
                             where t.TaskParticipatorId == tp.TaskParticipatorId
                             select t).FirstOrDefault();

                if (model != null)
                {
                    model.UserId = tp.UserId;
                    model.Status = tp.Status;
                    context.SaveChanges();
                }
            }
        }

        public bool UpdateTaskStatus(Guid taskId, ProjectTaskStatus status)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var model = (from t in context.ProjectTasks
                             where t.TaskId == taskId
                             select t).FirstOrDefault();

                if (model != null)
                {
                    model.StatusEnum = status;
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }

        }

        public bool UpdateTaskRole(TaskParticipator tp)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var model = (from t in context.TaskParticipators
                             where t.TaskParticipatorId == tp.TaskParticipatorId
                             select t).FirstOrDefault();

                if (model != null)
                {
                    model.StatusEnum = tp.StatusEnum;
                    model.UserId = tp.UserId;

                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
        
}
