using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Model
{
    public enum ProjectTaskStatus : byte
    {
        Unassigned = 0,

        Assigning = 1,

        Assigned = 2,

        Finishing = 3,

        Finished = 4
    }


    public enum TaskParticipatorStatus : byte
    {
        Unassigned = 0,

        Assigned = 1,

        Working = 2,

        Finished = 3,

        Delayed = 4,

        Canceld  = 5
    }

}
