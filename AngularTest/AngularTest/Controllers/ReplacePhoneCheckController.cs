using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private PhoneContext _phoneContext;
        private IQueryable<Phone> phoneIQ;
        private readonly ReplacePhoneService replacePhoneService;

        public ReplacePhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = _phoneContext.Phones;
            replacePhoneService = new ReplacePhoneService(_phoneContext);
        }

        /// <summary>
        /// url: "/api/ReplacePhoneCheck/GetReplacePhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        public ReplacePhoneCheckPageViewModel GetReplacePhoneCheckPageViewModel()
        {
            ReplacePhoneCheckPageViewModel model = new ReplacePhoneCheckPageViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
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
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                model.IsParameterNotEmpty = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                if(TempPhone.IsTempNewPhoneNotEmpty(loginUserId) && TempPhone.IsTempOldPhoneNotEmpty(loginUserId))
                {
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