using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ErrorPageController : ControllerBase
    {
        private readonly SuccessErrorPageService successErrorPageService;

        public ErrorPageController()
        {
            successErrorPageService = new SuccessErrorPageService();
        }

        /// <summary>
        /// url: "/api/ErrorPage/GetErrorPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ErrorPageViewModel GetErrorPageViewModel()
        {
            ErrorPageViewModel model = new ErrorPageViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if (!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                successErrorPageService.SetTempPhoneEmpty(loginUserId);
            }
            return model;
        }
    }
}