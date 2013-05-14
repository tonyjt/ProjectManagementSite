using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.PMSSite.Models;
namespace PMS.PMSSite.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        protected User CurrentUser
        {
            get
            {
                return new User
                {
                    UserId = new Guid("b6b40c01-5997-42fe-b669-447fc1cea267"),
                    Account = "liujiangtao@idfsoft.com",
                    UserName = "tonyjt"
                };
            }
        }


        #region View Message
        //public ViewMessageModel GetSuccessMessage(string message)
        //{
        //    return new ViewMessageModel
        //    {
        //        Display = true,
        //        Type = ViewMessageDispayType.Success,
        //        Message = message
        //    };
        //}
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public void ShowErrorMessage(string message, bool isRedirect = false, string title = "提示信息")
        {
            ShowMessage(ViewMessageDispayType.Top, message, MessageType.Error, isRedirect, title);
        }
        public void ShowSuccessMessage(string message, bool isRedirect = false)
        {
            ShowMessage(ViewMessageDispayType.Top, message, MessageType.Success, isRedirect);
        }
        public void ShowMessage(ViewMessageDispayType type, string message, MessageType messageType = MessageType.Success, bool isRedirect = false, string title = "提示信息")
        {
            ViewMessageModel modal = new ViewMessageModel
            {
                Title = title,
                Display = true,
                DisplayType = type,
                MessageType = messageType,
                Message = message
            };
            StoreMessageModal(modal, isRedirect);
        }

        private void StoreMessageModal(ViewMessageModel modal, bool isRedirect = false)
        {
            if (isRedirect)
            {
                TempData["ViewMessage"] = modal;
            }
            else
                ViewBag.ViewMessage = modal;
        }

        #endregion
    }
}
