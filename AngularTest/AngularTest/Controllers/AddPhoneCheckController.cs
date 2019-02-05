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
        private readonly AddPhoneService addPhoneService;

        public AddPhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            addPhoneService = new AddPhoneService(_phoneContext);
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
            if (Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.addPhoneCheck])
            {
                model.IsVisitLegal = true;
                Step.nowNode = Step.addPhoneCheck;
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
            if (Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.addPhoneCheckSubmit])
            {
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
    }
}