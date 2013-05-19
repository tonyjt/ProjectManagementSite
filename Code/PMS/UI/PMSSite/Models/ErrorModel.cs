using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.PMSSite.Models
{
    public class ErrorMessage
    {
        public ErrorMessage()
        {
        }

        public ErrorMessage(int code, string title, string message)
        {
            Code = code;
            Title = title;
            Message = message;
        }

        public ErrorMessage(string title, string message)
        {
            this.Title = title;
            this.Message = message;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string Message { get; set; }

    }
}