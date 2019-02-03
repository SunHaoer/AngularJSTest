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
    public class AddPhoneCheckController : ControllerBase
    {
        private PhoneContext _phoneContext;
        private IQueryable<Phone> phoneIQ;
        private readonly AddPhoneService addPhoneService;

        public AddPhoneCheckController(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = _phoneContext.Phones;
            addPhoneService = new AddPhoneService(_phoneContext);
        }

        /// <summary>
        /// url: "/api/AddPhoneCheck/GetAddPhoneCheckPageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AddPhoneCheckPageViewModel GetAddPhoneCheckPageViewModel()
        {
            AddPhoneCheckPageViewModel model = new AddPhoneCheckPageViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
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
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                model.IsParameterNotEmpty = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
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