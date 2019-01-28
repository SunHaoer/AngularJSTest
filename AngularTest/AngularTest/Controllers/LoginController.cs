using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IQueryable<User> UserIQ;

        public LoginController(UserContext context)
        {
            _context = context;
            if(_context.Users.Count() == 0)
            {
                _context.Add(new User("admin", "root"));
                _context.Add(new User("Dillon", "sunhao"));
                _context.SaveChanges();
            }
            UserIQ = _context.Users;
        }

        /// <summary>
        /// 登录
        /// url: "/api/Login/Login"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<bool> Login(string username = "", string password = "")
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) )
            {
                User user = UserIQ.FirstOrDefault(item => item.Username.Equals(username) && item.Password.Equals(password));
                if(user != null)
                {
                    HttpContext.Session.SetString("loginUser", user.ToSessionString());
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 登出
        /// url: "/api/Login/Logout"
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<bool> Logout(string username, string password)
        {
            HttpContext.Session.Remove("loginUser");
            return true;
        }

        /// <summary>
        /// 获取登录者信息
        /// url: "/api/Login/GetLoginUserInfo"
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetLoginUserInfo()
        {
            return HttpContext.Session.GetString("loginUser");
        }
    }
}