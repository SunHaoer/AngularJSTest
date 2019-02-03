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
    public class SuccessPageController : ControllerBase
    {
        private readonly SuccessErrorPageService successErrorPageService;

        public SuccessPageController()
        {
            successErrorPageService = new SuccessErrorPageService();
        }

        /// <summary>
        /// url: "/api/SuccessPage/GetSuccessPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SuccessPageViewModel GetSuccessPageViewModel()
        {
            SuccessPageViewModel model = new SuccessPageViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                successErrorPageService.SetTempPhoneEmpty(loginUserId);
            }
            return model;
        }
    }
}