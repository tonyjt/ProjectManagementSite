﻿using log4net;
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
    public class RequirementManager
    {
        private static RequirementDAL dataAccess = new RequirementDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(ProjectManager).Name);


        public static IEnumerable<Requirement> GetAllRequirement(Guid projectId, bool needValid = true)
        {
            IEnumerable<Requirement> res = ManagerHelper.GetModel<IEnumerable<Requirement>>(projectId,dataAccess.GetAllRequirement,log);

            if (needValid)
            {
                res = res.Where(r => r.IsValid);
            }
            return res;
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
               
               result = ManagerHelper.CreateModel(requirement, dataAccess.CreateRequirement, log);

            }
            return result;
        }

        public static Requirement GetRequirement(Guid requirementId)
        {
            Requirement re = ManagerHelper.GetModel(requirementId, dataAccess.GetRequirement, log);

            return re;
            
        }
    }
}
