using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PMS.PMSDBDataAccess.Models;

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

        public ProjectVersion GetVersion(Guid versionId)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                    return (from p in context.ProjectVersions
                            where p.VersionId == versionId
                            select p).SingleOrDefault();
                
            }
        }

        public bool UpdateVersion(ProjectVersion version)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                var model = (from p in context.ProjectVersions
                             where p.VersionId == version.VersionId
                             select p).SingleOrDefault();

                if (model != null)
                {
                    model.VersionName = version.VersionName;
                    model.StartTime = version.StartTime;
                    model.EndTime = version.EndTime;
                    model.Status = version.Status;
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
