using System;
using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeletePhoneController : ControllerBase
    {
        DeletePhoneManage deletePhoneManage;

        public DeletePhoneController(DeleteReasonContext deleteReasonContext)
        {
            deletePhoneManage = new DeletePhoneManage(deleteReasonContext);
        }

        /// <summary>
        /// url: "/api/DeletePhone/GetDeletePhonePageViewModel"
        /// </summary>
        /// <returns></returns>
        public DeletePhonePageViewModel GetDeletePhonePageViewModel()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            DeletePhonePageViewModel model = deletePhoneManage.GetDeletePhonePageViewModel(loginUserId, nowNode, isSubmit);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.deletePhone.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/DeletePhone/SubmitMsg"
        /// </summary>
        /// <param name="deleteReason"></param>
        /// <param name="deleteDate"></param>
        /// <returns></returns>
        [HttpPost]
        public FormFeedbackViewModel SubmitMsg(string deleteReason, DateTime deleteDate, int state)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = deletePhoneManage.SubmitMsg(loginUserId, nowNode, deleteReason, deleteDate, state);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/DeletePhone/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.deletePhone;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }
    }
}