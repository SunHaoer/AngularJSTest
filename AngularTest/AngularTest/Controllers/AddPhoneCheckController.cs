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
    public class AddPhoneCheckController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly AddPhoneManage addPhoneService;

        public AddPhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            addPhoneService = new AddPhoneManage(_phoneContext);
        }

        /// <summary>
        /// url: "/api/AddPhoneCheck/GetAddPhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AddPhoneCheckPageViewModel GetAddPhoneCheckPageViewModel()
        {
            AddPhoneCheckPageViewModel model = new AddPhoneCheckPageViewModel
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            if (Step.stepTable[nowNode * isSubmit, Step.addPhoneCheck] || nowNode == Step.addPhoneCheck)
            {
                HttpContext.Session.SetString("nowNode", Step.addPhoneCheck.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
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
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.addPhoneCheckSubmit])
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if(TempPhone.IsTempNewPhoneNotEmpty(loginUserId))
                {
                    model.IsParameterLegal = true;
                    addPhoneService.SetTempNewPhoneToDBByUserId(loginUserId);
                    model.IsSuccess = true;
                }              
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
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.addPhoneCheckSubmit])
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