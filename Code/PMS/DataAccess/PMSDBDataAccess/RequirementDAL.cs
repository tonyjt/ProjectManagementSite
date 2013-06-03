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

        public bool Save(Requirement requirement)
        {
            
            using (PMSDBContext context = new PMSDBContext())
            {
                var model = (from m in context.Requirements
                            where m.RequirementId == requirement.RequirementId
                            select m).FirstOrDefault();

                if (model == null)
                {
                    return false;
                }
                else
                {
                    RequirementHistory history = new RequirementHistory(model);

                    model.Title = requirement.Title;
                    model.Content = requirement.Content;
                    model.ParentId = requirement.ParentId;
                    model.VersionId = requirement.VersionId;
                    model.UpdateTime = requirement.UpdateTime;

                    context.RequirementHistories.Add(history);
                    context.SaveChanges();

                    return true;    
                }
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
