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
        DeletePhoneService deletePhoneService;
        DeleteReasonService deleteReasonService;

        public DeletePhoneController(DeleteReasonContext deleteReasonContext)
        {
            deletePhoneService = new DeletePhoneService();
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
            if(Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.deletePhone])
            {
            if (!string.IsNullOrEmpty(loginUserInfo))
                {
                    model.IsVisitLegal = true;
                    Step.nowNode = Step.deletePhone;
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
        public FormFeedbackViewModel SubmitMsg(string deleteReason, DateTime deleteDate, int state)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            if (Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.deletePhoneSubmit])
            {
                model.IsVisitLegal = true;
                if (!string.IsNullOrEmpty(deleteReason) && (state == 1 || state == 2))
                {
                    model.IsParameterNotEmpty = true;
                    if (Validation.IsDateNotBeforeToday(deleteDate))
                    {
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
    }
}