using PMS.Model;
using PMS.PMSDBDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.PMSDBDataAccess
{
    public class PlanDAL
    {
        public void AddNewPlan(Plan newPlan)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.Plans.Add(newPlan);
                context.SaveChanges();
            }
        }
    }
}
