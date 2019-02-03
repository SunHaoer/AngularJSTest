using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReplacePhoneController : ControllerBase
    {
        private BrandContext _brandContext;
        private BrandTypeContext _brandTypeContext;
        private BrandTypeProductNoContext _brandTypeProductNoContext;
        private BrandService brandService;
        private BrandTypeService brandTypeService;
        private BrandTypeProductNoService brandTypeProductNoService;
        private readonly ReplacePhoneService replacePhoneService;

        public ReplacePhoneController(BrandContext brandContext, BrandTypeContext brandtypeContext, BrandTypeProductNoContext brandTypeProductNoContext)
        {
            _brandContext = brandContext;
            _brandTypeContext = brandtypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            brandService = new BrandService(_brandContext);
            brandTypeService = new BrandTypeService(_brandTypeContext);
            brandTypeProductNoService = new BrandTypeProductNoService(_brandTypeProductNoContext);
            replacePhoneService = new ReplacePhoneService();
        }

        /// <summary>
        /// url: "/api/ReplacePhone/GetReplacePhonePageViewModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ReplacePhonePageViewModel GetReplacePhonePageViewModel()
        {
            ReplacePhonePageViewModel model = new ReplacePhonePageViewModel()
            {
                IsLogin = false
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
                model.TempOldPhone = TempPhone.GetTempOldPhoneByUserId(loginUserId);
                model.BrandList = brandService.GetBrandList();
                model.TypeList = brandTypeService.GetBrandTypeList();
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ReplacePhone/ValidateBrandTypeProductNo"
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel ValidateBrandTypeProductNo(string brand, string type, string productNo)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = false,
                IsParameterNotEmpty = false
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if (!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                if (!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(productNo))
                {
                    model.IsParameterNotEmpty = true;
                    brand = brand.Trim();
                    type = type.Trim();
                    productNo = productNo.Trim();
                    model.IsParameterLegal = brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo);
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/ReplacePhone/SubmitMsg"
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SubmitMsg(string productNo, string brand, string type, DateTime startDate, DateTime abandonDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if (!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                if (!string.IsNullOrEmpty(productNo) && !string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && startDate != null)
                {
                    model.IsParameterNotEmpty = true;
                    if (brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate) && Validation.IsTwoDaysEquals(startDate, abandonDate))
                    {
                        model.IsParameterLegal = true;
                        string loginUsername = loginUserInfo.Split(",")[1];
                        long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                        DateTime endDate = new DateTime(2019, 02, 02);
                        Phone phone = new Phone(loginUsername, loginUserId, brand, type, productNo, startDate, endDate);
                        replacePhoneService.SetTempNewPhoneByUserId(loginUserId, phone);
                        replacePhoneService.SetTempOldPhoneAbandonDateByUserId(loginUserId, abandonDate);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }
    }
}