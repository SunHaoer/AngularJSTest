using System;
using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddPhoneController : ControllerBase
    {
        private AddPhoneManage addPhoneManage;
        
        public AddPhoneController(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext)
        {
            addPhoneManage = new AddPhoneManage(brandContext, brandTypeContext, brandTypeProductNoContext, typeYearContext);
        }

        /// <summary>
        /// url: "/api/AddPhone/GetAddPhoneModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AddPhonePageViewModel GetAddPhoneModel()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            AddPhonePageViewModel model = addPhoneManage.GetAddPhoneModel(loginUserId, nowNode, isSubmit);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.addPhone.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhone/ValidateBrandTypeProductNo"
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel ValidateBrandTypeProductNo(string brand, string type, string productNo)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long userId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = addPhoneManage.ValidateBrandTypeProductNo(userId, nowNode, Step.addPhone, brand, type, productNo);
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhone/SubmitMsg"
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        [HttpPost]
        public FormFeedbackViewModel SubmitMsg(string productNo, string brand, string type, DateTime startDate)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = addPhoneManage.SubmitMsg(loginUserInfo, nowNode, productNo, brand, type, startDate);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhone/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.addPhone;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

    }
}