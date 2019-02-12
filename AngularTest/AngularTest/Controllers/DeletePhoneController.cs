using System;
using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeletePhoneController : ControllerBase
    {
        DeleteReasonContext _deleteReasonContext;
        DeletePhoneManage deletePhoneService;
        DeleteReasonService deleteReasonService;

        public DeletePhoneController(DeleteReasonContext deleteReasonContext)
        {
            deletePhoneService = new DeletePhoneManage();
            _deleteReasonContext = deleteReasonContext;
            deleteReasonService = new DeleteReasonService(_deleteReasonContext);
        }

        /// <summary>
        /// url: "/api/DeletePhone/GetDeletePhonePageViewModel"
        /// </summary>
        /// <returns></returns>
        public DeletePhonePageViewModel GetDeletePhonePageViewModel()
        {
            DeletePhonePageViewModel model = new DeletePhonePageViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            if (Step.stepTable[nowNode * isSubmit, Step.deletePhone] || nowNode == Step.deletePhone)
            {
            if (!string.IsNullOrEmpty(loginUserInfo))
                {
                    HttpContext.Session.SetString("nowNode", Step.deletePhone.ToString());
                    HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                    model.IsVisitLegal = true;
                    model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
                    model.DeleteReasonList = deleteReasonService.GetDeleteReason(); 
                }
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
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.deletePhoneSubmit])
            {
                model.IsVisitLegal = true;
                if (!string.IsNullOrEmpty(deleteReason) && (state == 1 || state == 2))
                {
                    model.IsParameterNotEmpty = true;
                    if (Validation.IsDateNotBeforeToday(deleteDate))
                    {
                        HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                        model.IsParameterLegal = true;
                        string loginUsername = loginUserInfo.Split(",")[1];
                        deleteReason = deleteReason.Trim();
                        deletePhoneService.SetTempNewPhoneDeleteByUserId(loginUserId, deleteReason, deleteDate, state);
                        model.IsSuccess = true;
                    }
                }
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
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.deletePhoneSubmit])
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