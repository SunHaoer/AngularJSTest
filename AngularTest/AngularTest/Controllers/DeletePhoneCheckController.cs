﻿using System.Linq;
using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeletePhoneCheckController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly IQueryable<Phone> phoneIQ;
        private readonly DeletePhoneManage deletePhoneService;

        public DeletePhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = phoneContext.Phones;
            deletePhoneService = new DeletePhoneManage(phoneContext);
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/GetDeletePhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DeletePhoneCheckPageViewModel GetDeletePhoneCheckPageViewModel()
        {
            DeletePhoneCheckPageViewModel model = new DeletePhoneCheckPageViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            if (Step.stepTable[nowNode * isSubmit, Step.deletePhoneCheck] || nowNode == Step.deletePhoneCheck)
            {
                HttpContext.Session.SetString("nowNode", Step.deletePhoneCheck.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
            }
            return model;
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/SubmitMsg"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SubmitMsg()
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.deletePhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if (TempPhone.IsTempNewPhoneNotEmpty(loginUserId))
                {
                    HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                    model.IsParameterLegal = true;
                    deletePhoneService.SetTempNewPhoneToDBByUserId(loginUserId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.deletePhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                model.IsParameterNotEmpty = true;
                model.IsParameterLegal = true;
                model.IsSuccess = true;
            }
            return model;
        }
    }
}