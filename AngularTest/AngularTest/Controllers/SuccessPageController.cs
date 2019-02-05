using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Cache;
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
            SuccessPageViewModel model = new SuccessPageViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            if(Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.successPage])
            {
                model.IsVisitLegal = true;
                Step.nowNode = Step.successPage;
                successErrorPageService.SetTempPhoneEmpty(loginUserId);
            }
            return model;
        }
    }
}