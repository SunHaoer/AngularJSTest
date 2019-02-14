using AngularTest.Cache;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using AngularTest.ViewModelManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuccessPageController : ControllerBase
    {
        private readonly SuccessPageManage successPageManage;

        public SuccessPageController()
        {
            successPageManage = new SuccessPageManage();
        }

        /// <summary>
        /// url: "/api/SuccessPage/GetSuccessPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SuccessPageViewModel GetSuccessPageViewModel()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            SuccessPageViewModel model = successPageManage.GetSuccessPageViewModel(loginUserId, nowNode, isSubmit);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.successPage.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
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
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.successPage;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

    }
}