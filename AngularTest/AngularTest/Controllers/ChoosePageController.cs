using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChoosePageController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly ChoosePageService choosePageService;

        public ChoosePageController(PhoneContext context)
        {
            _phoneContext = context;
            choosePageService = new ChoosePageService(_phoneContext);
        }

        /// <summary>
        /// url: "/api/ChoosePage/GetChoosePageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ChoosePageViewModel GetChoosePageViewModel(int pageIndex = 1, int pageSize = 4)
        {
            ChoosePageViewModel model = new ChoosePageViewModel
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            model.LoginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            if (Step.stepTable[nowNode * isSubmit, Step.choosePage] || nowNode == Step.choosePage)
            {
                HttpContext.Session.SetString("nowNode", Step.choosePage.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                model.IsVisitLegal = true;
                model.LoginUsername = loginUserInfo.Split(",")[1];
                choosePageService.SetTempPhoneEmpty(model.LoginUserId);
                model.PhoneList = choosePageService.GetPhoneList(model.LoginUserId, pageIndex, pageSize);
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetUsingToAbandonById"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetUsingToAbandonById(long id, DateTime abandonDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (choosePageService.ValidateIdInAbandon(id, loginUserId) && Validation.IsDateLegal(abandonDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = choosePageService.GetPhoneById(id);
                    phone = choosePageService.UpdatePhoneState(phone, abandonDate, 2);
                    choosePageService.UpdatePhoneStateInDB(phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetAbanddonToUsingById"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetAbanddonToUsingById(long id, DateTime startDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (choosePageService.ValidateIdInAbandon(id, loginUserId) && Validation.IsDateLegal(startDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = choosePageService.GetPhoneById(id);
                    phone = choosePageService.UpdatePhoneState(phone, startDate, 1);
                    choosePageService.UpdatePhoneStateInDB(phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetTempOldPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetTempOldPhoneById(long id)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (choosePageService.ValidateIdInReplace(id, loginUserId))
                {
                    HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = choosePageService.GetPhoneById(id);
                    TempPhone.SetTempOldPhoneByUserId(loginUserId, phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetTempNewPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetTempNewPhoneById(long id)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
            {
                model.IsVisitLegal = true;
                if (choosePageService.ValidateIdInDelete(id, loginUserId))
                {
                    HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = choosePageService.GetPhoneById(id);
                    TempPhone.SetTempNewPhoneByUserId(loginUserId, phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetIsSubmit"
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
            if (Step.stepTable[nowNode, Step.choosePageSubmit])
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