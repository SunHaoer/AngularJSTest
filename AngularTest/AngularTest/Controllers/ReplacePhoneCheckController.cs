using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReplacePhoneCheckController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly ReplacePhoneService replacePhoneService;

        public ReplacePhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            replacePhoneService = new ReplacePhoneService(_phoneContext);
        }

        /// <summary>
        /// url: "/api/ReplacePhoneCheck/GetReplacePhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ReplacePhoneCheckPageViewModel GetReplacePhoneCheckPageViewModel()
        {
            ReplacePhoneCheckPageViewModel model = new ReplacePhoneCheckPageViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            if (Step.stepTable[nowNode * isSubmit, Step.replacePhoneCheck])
            {
                HttpContext.Session.SetString("nowNode", Step.replacePhoneCheck.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
                model.TempOldPhone = TempPhone.GetTempOldPhoneByUserId(loginUserId);
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ReplacePhoneCheck/SubmitMsg"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SubmitMsg()
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.replacePhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if(TempPhone.IsTempNewPhoneNotEmpty(loginUserId) && TempPhone.IsTempOldPhoneNotEmpty(loginUserId))
                {
                    HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                    model.IsParameterLegal = true;
                    replacePhoneService.SetTempNewPhoneToDBByUserId(loginUserId);
                    replacePhoneService.SetTemoOldPhoneAbandonToDBByUserId(loginUserId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }
    }
}