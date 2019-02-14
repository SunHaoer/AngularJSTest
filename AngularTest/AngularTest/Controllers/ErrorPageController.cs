﻿using AngularTest.Cache;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ErrorPageController : ControllerBase
    {
        private readonly SuccessErrorPageManage successErrorPageManage;

        public ErrorPageController()
        {
            successErrorPageManage = new SuccessErrorPageManage();
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
            model.IsLogin = true;
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            model.IsVisitLegal = true;
            HttpContext.Session.SetString("nowNode", Step.errorPage.ToString());
            HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            successErrorPageManage.SetTempPhoneEmpty(loginUserId);
            return model;
        }

        /// <summary>
        /// url: "/api/ErrorPage/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.errorPage;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }
    }
}