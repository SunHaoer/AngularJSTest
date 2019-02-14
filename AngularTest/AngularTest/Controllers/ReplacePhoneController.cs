using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReplacePhoneController : ControllerBase
    {
        private ReplacePhoneManage replacePhoneManage;

        public ReplacePhoneController(BrandContext brandContext, BrandTypeContext brandtypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext)
        {
            replacePhoneManage = new ReplacePhoneManage(brandContext, brandtypeContext, brandTypeProductNoContext, typeYearContext);
        }

        /// <summary>
        /// url: "/api/ReplacePhone/GetReplacePhonePageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ReplacePhonePageViewModel GetReplacePhonePageViewModel()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            ReplacePhonePageViewModel model = replacePhoneManage.GetReplacePhonePageViewModel(loginUserId, nowNode, isSubmit);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.replacePhone.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ReplacePhone/ValidateBrandTypeProductNo"
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel ValidateBrandTypeProductNo(string brand, string type, string productNo)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = replacePhoneManage.ValidateBrandTypeProductNo(loginUserId, nowNode, Step.replacePhone, brand, type, productNo);            return model;
        }

        /// <summary>
        /// url: "/api/ReplacePhone/SubmitMsg"
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpPost]
        public FormFeedbackViewModel SubmitMsg(string productNo, string brand, string type, DateTime startDate, DateTime abandonDate)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = replacePhoneManage.SubmitMsg(loginUserInfo, nowNode, productNo, brand, type, startDate, abandonDate);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ReplacePhone/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.replacePhone;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

    }
}