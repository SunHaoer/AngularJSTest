using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Cache;
using AngularTest.Models;
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
        DeletePhoneService deletePhoneService;

        public DeletePhoneController()
        {
            deletePhoneService = new DeletePhoneService();
        }

        /// <summary>
        /// url: "/api/DeletePhone/GetDeletePhonePageViewModel"
        /// </summary>
        /// <returns></returns>
        public DeletePhonePageViewModel GetDeletePhonePageViewModel()
        {
            DeletePhonePageViewModel model = new DeletePhonePageViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                if (!string.IsNullOrEmpty(loginUserInfo))
                {
                    model.IsLogin = true;
                    long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                    model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
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
        public FormFeedbackViewModel SubmitMsg(string deleteReason, DateTime deleteDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if (!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                if (!string.IsNullOrEmpty(deleteReason))
                {
                    model.IsParameterNotEmpty = true;
                    if (Validation.IsDateNotBeforeToday(deleteDate))
                    {
                        model.IsParameterLegal = true;
                        string loginUsername = loginUserInfo.Split(",")[1];
                        long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                        deleteReason = deleteReason.Trim();
                        deletePhoneService.SetTempNewPhoneDeleteByUserId(loginUserId, deleteReason, deleteDate);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }
    }
}