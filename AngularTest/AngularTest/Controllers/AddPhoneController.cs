using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private BrandContext _brandContext;
        private BrandTypeContext _brandtypeContext;
        private BrandTypeProductNoContext _brandTypeProductNoContext;
        private IQueryable<Brand> brandIQ;
        private IQueryable<BrandType> brandTypeIQ;
        private IQueryable<BrandTypeProductNo> brandTypeProductNoIQ;
        private BrandService brandService;
        private BrandTypeService brandTypeService;
        private BrandTypeProductNoService brandTypeProductNoService;
        private AddPhoneService addPhoneService;
        
        public AddPhoneController(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext)
        {
            _brandContext = brandContext;
            _brandtypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            brandIQ = _brandContext.Brands;
            brandTypeIQ = _brandtypeContext.BrandTypes;
            brandTypeProductNoIQ = _brandTypeProductNoContext.BrandTypeProductNos;
            brandService = new BrandService(_brandContext);
            brandTypeService = new BrandTypeService(_brandtypeContext);
            brandTypeProductNoService = new BrandTypeProductNoService(brandTypeProductNoContext);
            addPhoneService = new AddPhoneService();
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
                IsLogin = false
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
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
                IsLogin = false,
                IsParameterNotEmpty = false
            };
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                if(!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(productNo) )
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
        /// url: "/api/AddPhone/SubmitMsg"
        /// </summary>
        /// <param name="productNo"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        [HttpGet]
        public FormFeedbackViewModel SubmitMsg(string productNo, string brand, string type, DateTime startDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel();
            string loginUserInfo = HttpContext.Session.GetString("loginUser");
            if(!string.IsNullOrEmpty(loginUserInfo))
            {
                model.IsLogin = true;
                if(!string.IsNullOrEmpty(productNo) && !string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && startDate != null)
                {
                    model.IsParameterNotEmpty = true;
                    if(brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate))
                    {
                        model.IsParameterLegal = true;
                        string loginUsername = loginUserInfo.Split(",")[1];
                        long loginUserId = long.Parse(loginUserInfo.Split(",")[0]);
                        DateTime endDate = new DateTime(2019, 02, 02);
                        Phone phone = new Phone(loginUsername, loginUserId, brand, type, productNo, startDate, endDate);
                        addPhoneService.SetTempNewPhoneByUserId(loginUserId, phone);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }
    }
}