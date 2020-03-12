using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SIDEVIC.Filtros
{
    public class NotLoggedUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Access" },
                    { "action", "Login" }
                });

                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "AccesoDenegado",
                //    ViewData = filterContext.Controller.ViewData,
                //    TempData = filterContext.Controller.TempData
                //};
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class AdminPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Rol"].ToString() != "Administrador")
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Access" },
                    { "action", "AccessDenied" }
                });

                /* filterContext.Result = new ViewResult
                 {
                     ViewName = "AccessDenied",
                     ViewData = filterContext.Controller.ViewData,
                     TempData = filterContext.Controller.TempData
                 };
                 */
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class LoggedUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Home" }
                });

                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "AccesoDenegado",
                //    ViewData = filterContext.Controller.ViewData,
                //    TempData = filterContext.Controller.TempData
                //};
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class DirectorPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Rol"].ToString() != "Director" && HttpContext.Current.Session["Rol"].ToString() != "Administrador")
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });

                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "AccesoDenegado",
                //    ViewData = filterContext.Controller.ViewData,
                //    TempData = filterContext.Controller.TempData
                //};
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class CapturistaPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Rol"].ToString() != "Capturista" && HttpContext.Current.Session["Rol"].ToString() != "Administrador") 
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Home" }
                });

                //filterContext.Result = new ViewResult
                //{
                //    ViewName = "AccesoDenegado",
                //    ViewData = filterContext.Controller.ViewData,
                //    TempData = filterContext.Controller.TempData
                //};
            }
            base.OnActionExecuting(filterContext);
        }
    }
}