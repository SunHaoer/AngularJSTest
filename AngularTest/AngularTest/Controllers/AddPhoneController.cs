using System;
using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using AngularTest.VeiwModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddPhoneController : ControllerBase
    {
        private readonly BrandContext _brandContext;
        private readonly BrandTypeContext _brandtypeContext;
        private readonly BrandTypeProductNoContext _brandTypeProductNoContext;
        private readonly TypeYearContext _typeYearContext;
        private BrandService brandService;
        private BrandTypeService brandTypeService;
        private BrandTypeProductNoService brandTypeProductNoService;
        private AddPhoneService addPhoneService;
        private TypeYearService typeYearService;
        
        public AddPhoneController(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext)
        {
            _brandContext = brandContext;
            _brandtypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            brandService = new BrandService(_brandContext);
            brandTypeService = new BrandTypeService(_brandtypeContext);
            brandTypeProductNoService = new BrandTypeProductNoService(brandTypeProductNoContext);
            addPhoneService = new AddPhoneService();
            typeYearService = new TypeYearService(typeYearContext);
        }

        /// <summary>
        /// url: "/api/AddPhone/GetAddPhoneModel"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public AddPhonePageViewModel GetAddPhoneModel()
        {
            AddPhonePageViewModel model = new AddPhonePageViewModel
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            int isSubmit = int.Parse(HttpContext.Session.GetString("isSubmit"));
            if (Step.stepTable[nowNode * isSubmit, Step.addPhone] || nowNode == Step.addPhone)
            {
                HttpContext.Session.SetString("nowNode", Step.addPhone.ToString());
                HttpContext.Session.SetString("isSubmit", Step.isSubmitFalse.ToString());
                model.IsVisitLegal = true;
                Phone phone = TempPhone.GetTempNewPhoneByUserId(loginUserId);
                model.TempNewPhone = phone;
                model.BrandList = brandService.GetBrandList();
                model.TypeList = brandTypeService.GetBrandTypeList();
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhone/ValidateBrandTypeProductNo"
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
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long userId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.addPhone]) 
            {
                model.IsVisitLegal = true;
                if(!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(productNo) )
                {
                    model.IsParameterNotEmpty = true;
                    model.IsParameterLegal = true;
                    brand = brand.Trim();
                    type = type.Trim();
                    productNo = productNo.Trim();
                    model.IsSuccess = brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo);
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhone/SubmitMsg"
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        [HttpPost]
        public FormFeedbackViewModel SubmitMsg(string productNo, string brand, string type, DateTime startDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
            int nowNode = int.Parse(HttpContext.Session.GetString("nowNode"));
            if (Step.stepTable[nowNode, Step.addPhoneSubmit]) 
            {
                model.IsVisitLegal = true;
                if(!string.IsNullOrEmpty(productNo) && !string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && startDate != null)
                {
                    model.IsParameterNotEmpty = true;
                    if(brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate))
                    {
                        HttpContext.Session.SetString("isSubmit", Step.isSubmitTrue.ToString());
                        model.IsParameterLegal = true;
                        string loginUsername = loginUserInfo.Split(",")[1];
                        int phoneLife = typeYearService.GetYearByType(type);
                        DateTime endDate = addPhoneService.GetPhoneEndDate(startDate, phoneLife);
                        Phone phone = new Phone(loginUsername, loginUserId, brand, type, productNo, startDate, endDate);
                        addPhoneService.SetTempNewPhoneByUserId(loginUserId, phone);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// url: "/api/AddPhone/SetIsSubmit"
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
            if (Step.stepTable[nowNode, Step.addPhoneSubmit])
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