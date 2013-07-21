using CustomExtension.Helper;
using log4net;
using PMS.Model;
using PMS.PMSDBDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.PMSBLL
{
    public class PlanManager
    {
        private static PlanDAL dataAccess = new PlanDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(PlanManager).Name);

        public static bool CreatePlan(Plan newPlan)
        {
            if (newPlan != null & !string.IsNullOrEmpty(newPlan.Title))
            {
                newPlan.PlanId = Guid.NewGuid();

                newPlan.StatusEnum = PlanStatus.New;

                return ManagerHelper.ActionVoid(newPlan, dataAccess.AddNewPlan, log);
            }
            else
                return false;
        }
    }
}
