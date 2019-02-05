using System.Linq;
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
    public class DeletePhoneCheckController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly IQueryable<Phone> phoneIQ;
        private readonly DeletePhoneService deletePhoneService;

        public DeletePhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = phoneContext.Phones;
            deletePhoneService = new DeletePhoneService(phoneContext);
        }

        /// <summary>
        /// url: "/api/DeletePhoneCheck/GetDeletePhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public DeletePhoneCheckPageViewModel GetDeletePhoneCheckPageViewModel()
        {
            DeletePhoneCheckPageViewModel model = new DeletePhoneCheckPageViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            if (Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.deletePhoneCheck])
            {
                model.IsVisitLegal = true;
                Step.nowNode = Step.deletePhoneCheck;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
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
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            if (Step.GetStepTableByUserId(loginUserId)[Step.nowNode, Step.deletePhoneCheckSubmit])
            {
                model.IsVisitLegal = true;
                model.IsParameterNotEmpty = true;
                if (TempPhone.IsTempNewPhoneNotEmpty(loginUserId))
                {
                    model.IsParameterLegal = true;
                    deletePhoneService.SetTempNewPhoneToDBByUserId(loginUserId);
                    model.IsSuccess = true;
                }
            }
            return model;
        }
    }
}