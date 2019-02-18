using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginManage loginManage;

        public LoginController(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext, DeleteReasonContext deleteReasonContext)
        {
            loginManage = new LoginManage(userContext, phoneContext, brandContext, brandTypeContext, brandTypeProductNoContext, typeYearContext, deleteReasonContext);
        }

        /// <summary>
        /// url: "/api/Login/InitLogin"
        /// </summary>
        [HttpGet]
        public void InitLogin()
        {
            loginManage.SetInitData();
        }

        /// <summary>
        /// 登录
        /// url: "/api/Login/Login"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public LoginPageViewModel Login(string username, string password)
        {
            LoginPageViewModel model = loginManage.Login(username, password);
            if(model.IsLegal)
            {
                HttpContext.Session.SetString("loginUser", model.LoginUserSessionString);
                model.LoginUserSessionString = null;
                HttpContext.Session.SetString("nowNode", Step.loginPage.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// 登出
        /// url: "/api/Login/Logout"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public void Logout()
        {
            HttpContext.Session.Remove("loginUser");
            HttpContext.Session.Remove("nowNode");
            HttpContext.Session.Remove("isSubmit");
        }

        /// <summary>
        /// url: "/api/Login/NotLogin"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BasePageViewModel NotLogin()
        {
            BasePageViewModel model = new BasePageViewModel();
            return model;
        }

    }
}