using AngularTest.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AngularTest.Filter
{   
    // 未开启
    public class LoginFilter : IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("loginUser") == null)
            {
                if (context.Controller.GetType() != typeof(LoginController))
                {
                    context.Result = new RedirectResult("/api/Login/NotLogin");
                }
            }
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
