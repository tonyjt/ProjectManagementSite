using PMS.Model;
using PMS.PMSDBDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace PMS.PMSDBDataAccess
{
    public class ProjectParticipatorDAL
    {
        public IEnumerable<ProjectParticipator> GetAllProjectForUser(Guid userId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                return (from p in context.ProjectParticipators
                            .Include(p => p.Project)
                        where p.UserId == userId
                        select p).ToArray();
            }
        }

        public void CreateProjectParticipator(ProjectParticipator pp)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.ProjectParticipators.Add(pp);
                context.SaveChanges();
            }
        }

        public IEnumerable<ProjectParticipator> GetProjectParticipators(Guid projectId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var pp = from p in context.ProjectParticipators
                            .Include(p=>p.User)
                         where p.ProjectId == projectId
                         select p;

                return pp.ToArray();
            }
        }

        //public IEnumerable<ProjectParticipator> GetProjectParticipators(Guid projectId,RoleEnum role)
        //{
        //    using (PMSDBContext context = new PMSDBContext())
        //    {
        //        var pp = from p in context.ProjectParticipators
        //                    .Include(p => p.User)
        //                 where p.ProjectId == projectId && (p.Roles & (short)role != 0)
        //                 select p;

        //        return pp.ToArray();
        //    }
        //}
    }
}
