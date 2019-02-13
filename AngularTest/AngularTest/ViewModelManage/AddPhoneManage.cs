using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using System;
using AngularTest.Utils;

namespace AngularTest.VeiwModels
{
    public class AddPhoneManage
    {
        protected BrandContext _brandContext;
        protected BrandTypeContext _brandTypeContext;
        protected BrandTypeProductNoContext _brandTypeProductNoContext;
        protected TypeYearContext _typeYearContext;
        protected BrandService brandService;
        protected BrandTypeService brandTypeService;
        protected BrandTypeProductNoService brandTypeProductNoService;
        protected TypeYearService typeYearService;

        public AddPhoneManage()
        {
        }

        public AddPhoneManage(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext)
        {
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            brandService = new BrandService(_brandContext);
            brandTypeService = new BrandTypeService(_brandTypeContext);
            brandTypeProductNoService = new BrandTypeProductNoService(brandTypeProductNoContext);
            typeYearService = new TypeYearService(typeYearContext);
        }

        public AddPhonePageViewModel GetAddPhoneModel(long userId, int nowNode, int isSubmit)
        {
            AddPhonePageViewModel model = new AddPhonePageViewModel
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.addPhone] || nowNode == Step.addPhone)
            {
                model.IsVisitLegal = true;
                Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
                model.TempNewPhone = phone;
                model.BrandList = brandService.GetBrandList();
                model.TypeList = brandTypeService.GetBrandTypeList();
            }
            return model;
        }

        public FormFeedbackViewModel ValidateBrandTypeProductNo(long userId, int nowNode, int visitNode, string brand, string type, string productNo)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, visitNode])
            {
                model.IsVisitLegal = true;
                if (!string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(productNo))
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

        public FormFeedbackViewModel SubmitMsg(string userInfo, int nowNode, string productNo, string brand, string type, DateTime startDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.addPhoneSubmit])
            {
                model.IsVisitLegal = true;
                if (!string.IsNullOrEmpty(productNo) && !string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && startDate != null)
                {
                    model.IsParameterNotEmpty = true;
                    if (brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate))
                    {
                        
                        model.IsParameterLegal = true;
                        string loginUsername = userInfo.Split(",")[1];
                        long userId = long.Parse(userInfo.Split(",")[0]);
                        int phoneLife = typeYearService.GetYearByType(type);
                        DateTime endDate = GetPhoneEndDate(startDate, phoneLife);
                        Phone phone = new Phone(loginUsername, userId, brand, type, productNo, startDate, endDate);
                        SetTempNewPhoneByUserId(userId, phone);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }

        protected DateTime GetPhoneEndDate(DateTime startDate, int phoneLife)
        {
            DateTime endDate = new DateTime(startDate.Year + phoneLife, startDate.Month, startDate.Day);
            return endDate;
        }

        protected void SetTempNewPhoneByUserId(long userId, Phone phone)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, phone);
        }

    }
}
