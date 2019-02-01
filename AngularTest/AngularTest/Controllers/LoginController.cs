using System;
using System.Collections.Generic;
using System.Linq;
using AngularTest.Models;
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
        private UserContext _userContext;
        private PhoneContext _phoneContext;
        private IQueryable<User> userIQ;
        private IQueryable<Phone> phoneIQ;
        private readonly LoginService loginService;

        public LoginController(UserContext userContext, PhoneContext phoneContext)
        {
            _userContext = userContext;
            _phoneContext = phoneContext;
            userIQ = _userContext.Users;
            phoneIQ = _phoneContext.Phones;
            loginService = new LoginService(_userContext, _phoneContext, userIQ, phoneIQ);
        }

        /// <summary>
        /// 登录
        /// url: "/api/Login/Login"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public LoginPageViewModel Login(string username, string password)
        {
            loginService.SetInitData();
            LoginPageViewModel model = new LoginPageViewModel
            {
                IsLegal = false
            };
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) )
            {
                User user = loginService.GetLoginUser(username.Trim(), password.Trim());
                if(user != null)
                {
                    HttpContext.Session.SetString("loginUser", user.ToSessionString());
                    model.IsLegal = true;
                }
                loginService.SetInitDataBase();
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

    }
}