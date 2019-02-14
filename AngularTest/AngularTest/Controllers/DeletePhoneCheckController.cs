using System.Linq;
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
    public class DeletePhoneCheckController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly IQueryable<Phone> phoneIQ;
        private readonly DeletePhoneCheckManage deletePhoneCheckManage;

        public DeletePhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = phoneContext.Phones;
            deletePhoneCheckManage = new DeletePhoneCheckManage(phoneContext);
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/GetDeletePhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DeletePhoneCheckPageViewModel GetDeletePhoneCheckPageViewModel()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            DeletePhoneCheckPageViewModel model = deletePhoneCheckManage.GetDeletePhoneCheckPageViewModel(loginUserId, nowNode, isSubmit);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.deletePhoneCheck.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/SubmitMsg"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SubmitMsg()
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = deletePhoneCheckManage.SubmitMsg(loginUserId, nowNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.deletePhoneCheck;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }
    }
}