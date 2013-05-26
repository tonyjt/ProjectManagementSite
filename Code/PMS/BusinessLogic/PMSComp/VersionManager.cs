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
using PMS.Model.Enum;
 
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


            return versionList.Where(v=>v.VersionStatus != VersionStatus.Delete).OrderByDescending(v=>v.CreateTime);
        }
        public static bool StartVersion(Guid versionId)
        {
            IEnumerable<VersionStatus> referStatus = new List<VersionStatus>{
               VersionStatus.Ready,
               VersionStatus.Stop
           };

            return UpdateVersionStatus(versionId, VersionStatus.Start, referStatus);
        }

        public static bool StopVersion(Guid versionId)
        {
            IEnumerable<VersionStatus> referStatus = new List<VersionStatus>{
               VersionStatus.Start
           };

            return UpdateVersionStatus(versionId, VersionStatus.Stop, referStatus);
        }

        public static bool DeleteVersion(Guid versionId)
        {
            IEnumerable<VersionStatus> referStatus = new List<VersionStatus>{
               VersionStatus.Ready,
               VersionStatus.Start,
               VersionStatus.Stop
           };

            return UpdateVersionStatus(versionId, VersionStatus.Delete, referStatus);
        }

        public static bool UpdateVersionStatus(Guid versionId, VersionStatus status, IEnumerable<VersionStatus> referStatus)
        {
            if (GuidHelper.IsValid(versionId))
            {
                ProjectVersion version = GetVersion(versionId);

                if (version != null)
                {
                    if (referStatus.Contains(version.VersionStatus))
                    {
                        version.VersionStatus = status;
                        switch (version.VersionStatus)
                        {
                            case VersionStatus.Start:
                                version.StartTime = DateTime.Now;
                                version.EndTime = null;
                                break;
                            case VersionStatus.Stop:
                                version.EndTime = DateTime.Now;
                                break;
                        }
                        return UpdateVersion(version);
                    }
                }
            }
            return false;
        }

        public static ProjectVersion GetVersion(Guid versionId, bool nullable = false)
        {

            ProjectVersion version = ManagerHelper.GetModel<ProjectVersion>(versionId, dataAccess.GetVersion, log);

            return GetVersionNullable(version, nullable);
        
        }

        private static ProjectVersion GetVersionNullable(ProjectVersion version, bool nullable)
        {
            if (!nullable && version != null)
            {
                if (version.VersionStatus == VersionStatus.Delete)
                    version = null;
            }
            return version;
        }


        public static bool UpdateVersion(ProjectVersion version)
        {
            try
            {
                return dataAccess.UpdateVersion(version);
            }
            catch (Exception ex)
            {
                log.ErrorInFunction(ex);
                return false;
            }
        }
    }
}
