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
 
namespace PMS.PMSBLL
{
    public class VersionManager
    {
        
        private static VersionDAL dataAccess = new VersionDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(VersionManager).Name);

        public static bool CreateVersion(ProjectVersion version)
        {
            try
            {
                if (version == null
                    || string.IsNullOrWhiteSpace(version.VersionName)
                    || !GuidHelper.IsValid(version.ProjectId)
                    || !GuidHelper.IsValid(version.Creator)) return false;

                if (!GuidHelper.IsValid(version.VersionId))
                {
                    version.VersionId = Guid.NewGuid();
                }
                version.CreateTime = DateTime.Now;
                version.VersionStatus = Model.Enum.VersionStatus.Ready;
                dataAccess.AddVersion(version);

                return true;
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                return false;
            }
         }


        public static IEnumerable<ProjectVersion> GetVersionForProject(Guid projectId)
        {
            IEnumerable<ProjectVersion> versionList = ManagerHelper.GetModel<IEnumerable<ProjectVersion>>(projectId, dataAccess.GetVersionForProject, log);

            return versionList;
        }
    }
}
