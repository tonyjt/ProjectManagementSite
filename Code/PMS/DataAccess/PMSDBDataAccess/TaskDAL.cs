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

        public IEnumerable<ProjectTask> SearchTask(Guid versionid, Guid requirementId, IEnumerable<byte> statusList, Guid userId,out int totalCount, int pageSize, int pageIndex) 
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var tasks = from t in context.ProjectTasks
                                .Include(r => r.Requirement)
                                .Include(r => r.TaskParticipators)
                                .Include(r => r.Requirement.ProjectVersion)
                                .Include(r => r.User)
                            where (versionid== Guid.Empty || t.Requirement.VersionId ==versionid)
                                && (requirementId== Guid.Empty  || t.RequirementId == requirementId)
                                && (userId== Guid.Empty  || t.TaskParticipators.Select(p => p.UserId).Contains(userId))
                                && ((statusList.Count() == 0 && t.Status!= (byte)ProjectTaskStatus.Canceled) || statusList.Contains(t.Status))
                            orderby t.CreateTime descending
                            select t;
                IEnumerable<ProjectTask> result = PageHelper.GetDatas<ProjectTask>(tasks, pageIndex, pageSize, out totalCount);

                return result;
            }
        }
    }
        
}
