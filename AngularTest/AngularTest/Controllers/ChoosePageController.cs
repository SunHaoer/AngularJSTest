using AngularTest.Models;
using AngularTest.Service;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                //choosePageService.SetInitData();
                model.IsLogin = true;
                string loginUserInfo = HttpContext.Session.GetString("loginUser");
                model.LoginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                model.LoginUsername = loginUserInfo.Split(",")[1];
                phoneIQ = phoneIQ.Where(item => item.UserId == model.LoginUserId);
                model.PhoneList = choosePageService.GetPhoneList(phoneIQ, pageIndex, pageSize);
            }
            return model;
        }
    }
}