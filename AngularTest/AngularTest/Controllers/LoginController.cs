using System.Linq;
using AngularTest.Data;
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
        private BrandContext _brandContext;
        private BrandTypeContext _brandTypeContext;
        private BrandTypeProductNoContext _brandTypeProductNoContext;
        private IQueryable<User> userIQ;
        private IQueryable<Phone> phoneIQ;
        private IQueryable<Brand> brandIQ;
        private IQueryable<BrandType> brandTypeIQ;
        private IQueryable<BrandTypeProductNo> brandTypeProductNoIQ;
        private readonly LoginService loginService;

        public LoginController(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext)
        {
            _userContext = userContext;
            _phoneContext = phoneContext;
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            userIQ = _userContext.Users;
            phoneIQ = _phoneContext.Phones;
            brandIQ = _brandContext.Brands;
            brandTypeIQ = _brandTypeContext.BrandTypes;
            brandTypeProductNoIQ = _brandTypeProductNoContext.BrandTypeProductNos;
            loginService = new LoginService(_userContext, _phoneContext, _brandContext, _brandTypeContext, _brandTypeProductNoContext, userIQ, phoneIQ, brandIQ, brandTypeIQ, brandTypeProductNoIQ);
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
                loginService.SetInitDataBase(user.Id);
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