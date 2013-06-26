using PMS.Model;
using PMS.PMSBLL;
using PMS.Tool.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace PMS.PMSSite.Models
{
    public class ProjectActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object objProject;
            string strProject;

            filterContext.Controller.ViewData["UserId"] = UserManager.GetCurrentUserId();
   
            if (filterContext.RouteData.Values.TryGetValue("project", out objProject))
            {
                strProject = objProject.ToString();
            }
            else
            {
                strProject = "";
            }

            Project project = ProjectManager.GetProject(strProject);

            if (project == null)
            {
                if (filterContext.HttpContext.Request.Cookies["project"] != null)
                {
                    strProject = filterContext.HttpContext.Request.Cookies["project"].Value;

                    project = ProjectManager.GetProject(strProject);
                }

                if (project == null)
                {
                    ProjectParticipator pp = ProjectManager.GetLastJoinProjectForUser(UserManager.GetCurrentUserId());

                    if (pp != null && pp.Project != null)
                    
                        project = pp.Project;
                }
            }
            object objController;

            bool inPMPage = false;

            if (filterContext.RouteData.Values.TryGetValue("controller", out objController))
            {
                if (objController.ToString().ToLower().Equals("project"))
                {
                    inPMPage = true;
                }
            }

            filterContext.Controller.ViewData["InPMPage"] = inPMPage;

            if (project != null)
            {

                HttpContext.Current.Items["project"] = project;

                filterContext.Controller.ViewData["OtherProjects"] = ProjectManager.GetOtherProjectsForUser(UserManager.GetCurrentUserId(), project.ProjectId);
                filterContext.Controller.ViewData["Project"] = project;

                HttpCookie cookie = new HttpCookie("project", project.Name);
                cookie.Expires = DateTime.Now.AddYears(1);
                filterContext.HttpContext.Response.Cookies.Add(cookie);


                base.OnActionExecuting(filterContext);
            }
            else
            {

                if (inPMPage)
                {
                    filterContext.Controller.ViewData["OtherProjects"] = ProjectManager.GetOtherProjectsForUser(UserManager.GetCurrentUserId(), GuidHelper.GetInvalidGuid());
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                            {"controller","error"},
                            {"action","index"},
                            {"title","无效的项目"},
                            {"message","未找到相关项目或您未参与此项目，您可以尝试创建新的项目或加入新的项目"}
                        });
                }
            }
        }
    }


}