using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class ViewMessageModel
    {
        public bool Display { get; set; }
        public MessageType MessageType { get; set; }
        public ViewMessageDispayType DisplayType{get;set;}
        public string Message { get; set; }
        public string Title { get; set; }

    }

    public enum MessageType
    {
        Success = 1,
        Error = 2,
        Block = 3,
        Info = 4
    }
    public enum ViewMessageDispayType
    {
        Top = 1,
        Modal = 2
    }
}