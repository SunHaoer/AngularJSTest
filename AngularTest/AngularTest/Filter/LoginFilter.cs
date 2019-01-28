using AngularTest.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Filter
{
    public class LoginFilter : ActionFilterAttribute, IActionFilter
    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("loginUser") == null)
            {
                if (context.Controller.GetType() != typeof(LoginController))
                {
                    context.Result = new RedirectResult("/api/Login/Login");
                }
            }
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
