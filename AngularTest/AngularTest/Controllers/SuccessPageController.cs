using AngularTest.Cache;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuccessPageController : ControllerBase
    {
        private readonly SuccessErrorPageService successErrorPageService;

        public SuccessPageController()
        {
            successErrorPageService = new SuccessErrorPageService();
        }

        /// <summary>
        /// url: "/api/SuccessPage/GetSuccessPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SuccessPageViewModel GetSuccessPageViewModel()
        {
            SuccessPageViewModel model = new SuccessPageViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            if (Step.stepTable[nowNode * isSubmit, Step.successPage] || nowNode == Step.successPage)
            {
                HttpContext.Session.SetString("nowNode", Step.successPage.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                model.IsVisitLegal = true;
                successErrorPageService.SetTempPhoneEmpty(loginUserId);
            }
            return model;
        }

        /// <summary>
        /// url: "/api/SuccessPage/SetIsSubmit"
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
            if (Step.stepTable[nowNode, Step.successPageSubmit])
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