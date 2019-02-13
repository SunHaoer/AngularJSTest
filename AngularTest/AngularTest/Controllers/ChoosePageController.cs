using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChoosePageController : ControllerBase
    {
        private readonly PhoneContext _phoneContext;
        private readonly ChoosePageManage choosePageManage;

        public ChoosePageController(PhoneContext context)
        {
            _phoneContext = context;
            choosePageManage = new ChoosePageManage(_phoneContext);
        }

        /// <summary>
        /// url: "/api/ChoosePage/GetChoosePageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ChoosePageViewModel GetChoosePageViewModel(int pageIndex = 1, int pageSize = 4)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            ChoosePageViewModel model = choosePageManage.GetChoosePageViewModel(loginUserInfo, nowNode, isSubmit, pageIndex, pageSize);
            if(model.IsVisitLegal)
            {
                HttpContext.Session.SetString("nowNode", Step.choosePage.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
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
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = choosePageManage.SetUsingToAbandonById(loginUserId, nowNode, id, abandonDate);
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
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = choosePageManage.SetAbanddonToUsingById(loginUserId, nowNode, id, startDate);
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetTempOldPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetTempOldPhoneById(long id)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = choosePageManage.SetTempOldPhoneById(loginUserId, nowNode, id);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetTempNewPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetTempNewPhoneById(long id)
        {
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            FormFeedbackViewModel model = choosePageManage.SetTempNewPhoneById(loginUserId, nowNode, id);
            if(model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ChoosePage/SetIsSubmit"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SetIsSubmit()
        {
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int visitNode = Step.choosePage;
            FormFeedbackViewModel model = Step.SetIsSubmit(nowNode, visitNode);
            if (model.IsSuccess)
            {
                HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
            }
            return model;
        }

    }
}