using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChoosePageController : ControllerBase
    {
        private PhoneContext _context;
        private IQueryable<Phone> phoneIQ;
        private readonly ChoosePageService choosePageService;

        public ChoosePageController(PhoneContext context)
        {
            _context = context;
            phoneIQ = _context.Phones;
            choosePageService = new ChoosePageService(_context, phoneIQ);
        }

        /// <summary>
        /// url: "/api/ChoosePage/GetChoosePageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ChoosePageViewModel GetChoosePageViewModel(int pageIndex = 1, int pageSize = 2)
        {
            ChoosePageViewModel model = new ChoosePageViewModel
            {
                IsLogin = false
            };
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("loginUser")))
            {
                model.IsLogin = true;
                string loginUserInfo = HttpContext.Session.GetString("loginUser");
                model.LoginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                model.LoginUsername = loginUserInfo.Split(",")[1];
                choosePageService.SetTempPhoneEmpty(model.LoginUserId);
                phoneIQ = phoneIQ.Where(item => item.UserId == model.LoginUserId);
                model.PhoneList = choosePageService.GetPhoneList(phoneIQ, pageIndex, pageSize);
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetUsingToAbandonById"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetUsingToAbandonById(long id, DateTime abandonDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if (!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                if(choosePageService.ValidateId(id, loginUserId) && Validation.IsDateLegal(abandonDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = choosePageService.GetPhoneById(id);
                    phone = choosePageService.UpdatePhoneState(phone, abandonDate, 2);
                    choosePageService.UpdatePhoneStateInDB(phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetAbanddonToUsingById"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetAbanddonToUsingById(long id, DateTime startDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if (!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                if (choosePageService.ValidateId(id, loginUserId) && Validation.IsDateLegal(startDate))
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    Phone phone = choosePageService.GetPhoneById(id);
                    phone = choosePageService.UpdatePhoneState(phone, startDate, 1);
                    choosePageService.UpdatePhoneStateInDB(phone);
                    model.IsSuccess = true;
                }
            }
            return model;
        }
    }
}