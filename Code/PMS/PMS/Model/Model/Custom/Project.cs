using PMS.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Model
{
    public partial class Project
    {
        [NotMapped]
        public ProjectStatus ProjectStatus
        {
            get
            {
                return (ProjectStatus)Status;
            }
            set
            {
                Status = (byte)value;
            }
        }
    }
}
