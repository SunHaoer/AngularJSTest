using System.Linq;
using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly PhoneContext _phoneContext;
        private readonly BrandContext _brandContext;
        private readonly BrandTypeContext _brandTypeContext;
        private readonly BrandTypeProductNoContext _brandTypeProductNoContext;
        private readonly TypeYearContext _typeYearContext;
        private readonly DeleteReasonContext _deleteReasonContext;
        private readonly LoginService loginService;

        public LoginController(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext, DeleteReasonContext deleteReasonContext)
        {
            _userContext = userContext;
            _phoneContext = phoneContext;
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            _deleteReasonContext = deleteReasonContext;
            loginService = new LoginService(_userContext, _phoneContext, _brandContext, _brandTypeContext, _brandTypeProductNoContext, _typeYearContext, _deleteReasonContext);
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
            loginService.SetInitData();
            LoginPageViewModel model = new LoginPageViewModel
            {
                IsVisitLegal = true
            };
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                User user = loginService.GetLoginUser(username.Trim(), password.Trim());
                if(user != null)
                {
                    HttpContext.Session.SetString("loginUser", user.ToSessionString());
                    HttpContext.Session.SetString("nowNode", Step.loginPage.ToString());
                    HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                    model.IsLegal = true;
                    loginService.SetInitDataBase(user.Id);
                    model.IsLogin = true;
                }
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