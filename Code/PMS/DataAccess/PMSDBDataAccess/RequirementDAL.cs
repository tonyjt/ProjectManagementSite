using PMS.Model;
using PMS.PMSDBDataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PMS.PMSDBDataAccess
{
    public class RequirementDAL
    {
        public IEnumerable<Requirement> GetAllRequirement(Guid projectId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {

                var res = from re in context.Requirements
                            .Include(v => v.ProjectVersion)
                          where re.ProjectVersion.ProjectId == projectId
                          select re;

                return res.ToArray();
            }

        }

        public void CreateRequirement(Requirement requirement)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.Requirements.Add(requirement);
                context.SaveChanges();
            }
        }

        public Requirement GetRequirement(Guid requirementId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var re = (from r in context.Requirements
                          where r.RequirementId == requirementId
                          select r).FirstOrDefault();

                return re;
            }
        }
    }
}
