using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using AngularTest.ViewModelManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddPhoneCheckController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly AddPhoneCheckManage addPhoneCheckManage;

        public AddPhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            addPhoneCheckManage = new AddPhoneCheckManage(_phoneContext);
        }

        /// <summary>
        /// url: "/api/AddPhoneCheck/GetAddPhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AddPhoneCheckPageViewModel GetAddPhoneCheckPageViewModel()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            AddPhoneCheckPageViewModel model = addPhoneCheckManage.GetAddPhoneCheckPageViewModel(loginUserId, nowNode, isSubmit);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.addPhoneCheck.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhoneCheck/SubmitMsg"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SubmitMsg()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = addPhoneCheckManage.SubmitMsg(loginUserId, nowNode);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhoneCheck/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.addPhoneCheckSubmit;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

    }
}