using AngularTest.Cache;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ErrorPageController : ControllerBase
    {
        private readonly SuccessErrorPageManage successErrorPageService;

        public ErrorPageController()
        {
            successErrorPageService = new SuccessErrorPageManage();
        }

        /// <summary>
        /// url: "/api/ErrorPage/GetErrorPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ErrorPageViewModel GetErrorPageViewModel()
        {
            ErrorPageViewModel model = new ErrorPageViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            model.IsLogin = true;
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            model.IsVisitLegal = true;
            HttpContext.Session.SetString("nowNode", Step.errorPage.ToString());
            HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            successErrorPageService.SetTempPhoneEmpty(loginUserId);
            return model;
        }

        /// <summary>
        /// url: "/api/ErrorPage/SetIsSubmit"
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
            if (Step.stepTable[nowNode, Step.errorPageSubmit])
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