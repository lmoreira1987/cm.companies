using System.Web;
using System.Web.Mvc;

using GServiceManagerMVC.ViewModels.Global;
using GServiceManagerMVC.DAL.Login;
using System.Web.Routing;
using System.Collections.Generic;

namespace GServiceManagerMVC.Filters
{
    public class AutenticarAttribute : ActionFilterAttribute
    {
        #region Propriedades

        private string controller;

        LoginDAL dal;

        #endregion
        public override void OnActionExecuting(ActionExecutingContext exe)
        {
            LoginViewModel login = (LoginViewModel)HttpContext.Current.Session["Usuario"];

            if (login == null)
                exe.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Desbloquear" }));
            else
            {
                dal = new LoginDAL();

                controller = exe.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();

                List<string> listaControllers = (List<string>)HttpContext.Current.Session["Controllers"];

                if (listaControllers == null)
                    listaControllers = new List<string>();

                if (!listaControllers.Contains(controller))
                    exe.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Dashboard", action = "Index" }));
            }
        }
    }
}