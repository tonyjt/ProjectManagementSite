﻿using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PMS.PMSSite.Models;
using PMS.PMSBLL;
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
                return UserManager.GetCurrentUser();
            }
        }

        protected Guid CurrentUserId
        {
            get
            {
                return UserManager.GetCurrentUserId();
            }
        }

        private Project project;

        protected Project Project
        {
            get
            {
                if (project == null)
                {
                    project = (Project)HttpContext.Items["project"];
                }
                return project;
            }
        }

        protected Guid ProjectId
        {
            get
            {
                return Project != null ? Project.ProjectId : Guid.Empty;
            }
        }
        #region  
        #endregion

        #region View Message

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

        #region Ajax View Message

        public ActionResult AjaxShowSuccessMessage(string message, bool isRedirect = false, string redirectUrl = "")
        {
            AjaxResult result = new AjaxResult
            {
                Message = message,
                Success = true,
                Redirect = isRedirect,
                RedirectUrl = redirectUrl
            };

            return AjaxShowMessage(result);
        }
        public ActionResult AjaxShowErrorMessage(string message)
        {
            AjaxResult result = new AjaxResult
            {
                Message = message,
                Success = false
            };

            return AjaxShowMessage(result);
        }

        public ActionResult AjaxShowMessage(AjaxResult result)
        {
            if (result.Success && result.Redirect)
            {
                ShowMessage(ViewMessageDispayType.Top, result.Message, MessageType.Success, true);
            }

            return Json(result);
        }

        #endregion
    }
}
