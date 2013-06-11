using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PMS.Model;
using PMS.PMSDBDataAccess.Models;

namespace PMS.PMSDBDataAccess
{
    public class TaskDAL
    {
        public void CreateTask(ProjectTask task)
        {
            using (PMSDBContext context = new PMSDBContext())
            {
                context.ProjectTasks.Add(task);

                context.SaveChanges();
            }
        }
    }
}
