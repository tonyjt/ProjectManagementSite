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
using CustomExtension.Helper;

namespace PMS.PMSBLL
{
    public class RequirementManager
    {
        private static RequirementDAL dataAccess = new RequirementDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(ProjectManager).Name);


        public static IEnumerable<Requirement> GetAllRequirement(Guid projectId, bool needValid = true)
        {
            IEnumerable<Requirement> res = ManagerHelper.GetModel<IEnumerable<Requirement>>(projectId,dataAccess.GetAllRequirement,log);

            if (res!=null&&needValid)
            {
                res = res.Where(r => r.IsValid);
            }
            return res;
        }

        public static Requirement GetFirstRequirement(Guid projectId)
        {
            IEnumerable<Requirement> requirements = GetAllRequirement(projectId);

            return requirements.OrderBy(r => r.CreateTime).FirstOrDefault();
        }

        public static Requirement GetRequirement(Guid projectId,string requirementName)
        {
            if (string.IsNullOrWhiteSpace(requirementName))
            {
                return null;
            }
            else
            {
                Requirement requirement = ManagerHelper.GetModel(projectId,requirementName, dataAccess.GetRequirement, log);

                return requirement;

            }
        }

        public static Guid GetRequirementId(Guid projectId,string requirementName)
        {
            Requirement requirement = GetRequirement(projectId,requirementName);

            return requirement != null ? requirement.RequirementId : GuidHelper.GetInvalidGuid();
        }

        public static IEnumerable<RequirementWithChildren> GetRequirementWithChildren(Guid projectId)
        {
            IEnumerable<Requirement> requirementList = GetAllRequirement(projectId);

            return GetRequirementWithChildren(requirementList);
        }

        public static IEnumerable<RequirementWithChildren> GetRequirementWithChildren(IEnumerable<Requirement> requirementList)
        {
            return GetRequirementWithChildren(requirementList, GuidHelper.GetInvalidGuid());
        }

        public static IEnumerable<RequirementWithChildren> GetRequirementWithChildren(IEnumerable<Requirement> requirementList,Guid rootId )
        {
            if (requirementList == null) return null;

            var rootNodes = from d in requirementList
                            where d.ParentId == rootId
                            orderby d.CreateTime
                            select d;

            List<RequirementWithChildren> deps = new List<RequirementWithChildren>();

            foreach (Requirement requirement in rootNodes)
            {
                RequirementWithChildren dwc = new RequirementWithChildren();

                dwc.Requirement = requirement;

                dwc.Children = GetRequirementWithChildren(requirementList, requirement.RequirementId);

                deps.Add(dwc);
            }

            return deps;
        }

        public static IEnumerable<Requirement> GetParentableRequirement(Guid projectId, Guid requirementId)
        {
            IEnumerable<Requirement> allRequirment = GetAllRequirement(projectId);

            return GetParentableRequirement(allRequirment, requirementId);
        }

        public static IEnumerable<Requirement> GetParentableRequirement( IEnumerable<Requirement> allRequirment, Guid requirementId)
        {
          
            IEnumerable<Requirement> children = GetChildren(allRequirment, requirementId);

            return allRequirment.Where(a => !children.Contains(a)&& a.RequirementId != requirementId);
        }

        public static IEnumerable<Requirement> GetChildren(IEnumerable<Requirement> list, Guid requirementId)
        {
            List<Requirement> childList = new List<Requirement> { };

            if (GuidHelper.IsValid(requirementId))
            {
                IEnumerable<Requirement> children = list.Where(r => r.ParentId == requirementId);

                childList.AddRange(children);

                foreach (Requirement re in children)
                {
                    childList.AddRange(GetChildren(list, re.RequirementId));
                }
            }
            return childList;
        }

        public static bool CreateNewRequirement(Requirement requirement)
        {

            bool result;

            if (requirement == null) result = false;

           
            else if (!GuidHelper.IsValid(requirement.VersionId)
              || string.IsNullOrWhiteSpace(requirement.Title))
            {
                result = false;
            }
            else
            {
               requirement.RequirementId = Guid.NewGuid();
               requirement.CreateTime = DateTime.Now;
               requirement.UpdateTime = DateTime.Now;
               requirement.IsValid = true;

               result = ManagerHelper.ActionVoid(requirement, dataAccess.CreateRequirement, log);

            }
            return result;
        }

        public static bool Save(Requirement requirement)
        {
            bool result;

            if (requirement == null
             || !GuidHelper.IsValid(requirement.RequirementId)
             || string.IsNullOrWhiteSpace(requirement.Title))
            {
                result = false;
            }

            else
            {
                requirement.UpdateTime = DateTime.Now;
                result = ManagerHelper.ActionBool(requirement,dataAccess.Save,log);
            }
            return result;
        }

        public static Requirement GetRequirement(Guid requirementId,bool isValid = true)
        {
            Requirement re = ManagerHelper.GetModel(requirementId, dataAccess.GetRequirement, log);


            return re!= null &&(re.IsValid||!isValid) ? re : null ;
            
        }

        public static IEnumerable<RequirementHistory> GetHistories(Guid requirementId)
        {
            if (GuidHelper.IsValid(requirementId))
            {
                IEnumerable<RequirementHistory> histories =  ManagerHelper.GetModel(requirementId, dataAccess.GetHistories, log);

                return histories != null ? histories.OrderByDescending(h => h.CreateDate) : null;
            }
            else
                return null;
        }

        public static bool DeleteRequirement(Guid requirementId)
        {
            if (GuidHelper.IsValid(requirementId))
            {
                Requirement re = RequirementManager.GetRequirement(requirementId);

                return DeleteRequirement(re);
            }
            return false;
        }

        public static bool DeleteRequirement(Requirement requirment)
        {
            if (requirment != null)
            {
                ProjectVersion version = VersionManager.GetVersion(requirment.VersionId, true);
                if (version != null)
                {
                    IEnumerable<Requirement> allRequirement = GetAllRequirement(version.ProjectId);

                    IEnumerable<Requirement> children = GetChildren(allRequirement, requirment.RequirementId);

                    if (children != null)
                    {
                        List<Requirement> requires = children.ToList();
                        requires.Add(requirment);

                        return ManagerHelper.ActionBool(requires.Select(r => r.RequirementId), dataAccess.DeleteRequirements, log);
                    }
                }
            }

            return false;
        }
    }
}
