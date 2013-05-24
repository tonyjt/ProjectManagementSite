using PMS.Model;
using PMSDBDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PMS.PMSDBDataAccess
{
    public class VersionDAL
    {
        public void AddVersion(ProjectVersion version)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.ProjectVersions.Add(version);
                context.SaveChanges();
            }
        }

        public IEnumerable<ProjectVersion> GetVersionForProject(Guid projectId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var versionList = from v in context.ProjectVersions
                                    .Include(v=>v.User)
                                  where v.ProjectId == projectId
                                  select v;

                return versionList.ToList();
            }
        }
    }
}
