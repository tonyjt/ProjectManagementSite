
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

        [NotMapped]
        public bool AllowStart
        {
            get
            {
                return ProjectStatus != ProjectStatus.Start;
            }
        }
        [NotMapped]
        public bool AllowPause
        {
            get
            {
                return ProjectStatus == ProjectStatus.Start;
            }
        }
        [NotMapped]
        public bool AllowStop
        {
            get
            {
                return ProjectStatus == ProjectStatus.Start || ProjectStatus == ProjectStatus.Pause;
            }
        }


    }
}
