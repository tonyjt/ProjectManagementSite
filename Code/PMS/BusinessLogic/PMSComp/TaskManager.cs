using log4net;
using PMS.PMSDBDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.PMSBLL
{
    public class TaskManager
    {
        private static TaskDAL dataAccess = new TaskDAL();

        private static readonly ILog log = LogManager.GetLogger(typeof(ProjectManager).Name);


        //public static CreateTask(Task task)
        //{

        //}
    }
}
