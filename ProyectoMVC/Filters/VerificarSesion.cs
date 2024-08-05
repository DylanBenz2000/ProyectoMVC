using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoMVC.Controllers;
using ProyectoMVC.Models;

namespace ProyectoMVC.Filters
{
    public class VerificarSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var oUsuario = (user)HttpContext.Current.Session["User"];

            // Permitir acceso a la acción de registro
            if (filterContext.ActionDescriptor.ActionName == "Register" &&
                filterContext.Controller is UserController)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            if (oUsuario == null)
            {
                if (filterContext.Controller is AccessController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
            else
            {
                if (filterContext.Controller is AccessController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
