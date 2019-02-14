using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Dao;
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
        protected BrandDao brandDao;
        protected BrandTypeDao brandTypeDao;
        protected BrandTypeProductNoDao brandTypeProductNoDao;
        protected TypeYearDao typeYearDao;

        public AddPhoneManage()
        {
        }

        public AddPhoneManage(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext)
        {
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            brandDao = new BrandDao(_brandContext);
            brandTypeDao = new BrandTypeDao(_brandTypeContext);
            brandTypeProductNoDao = new BrandTypeProductNoDao(brandTypeProductNoContext);
            typeYearDao = new TypeYearDao(typeYearContext);
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
                model.BrandList = brandDao.GetBrandList();
                model.TypeList = brandTypeDao.GetBrandTypeList();
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
                    model.IsSuccess = brandTypeProductNoDao.ValidateBrandTypeProductNo(brand, type, productNo);
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
                    if (brandTypeProductNoDao.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate))
                    {
                        
                        model.IsParameterLegal = true;
                        string loginUsername = userInfo.Split(",")[1];
                        long userId = long.Parse(userInfo.Split(",")[0]);
                        int phoneLife = typeYearDao.GetYearByType(type);
                        DateTime endDate = GetPhoneEndDate(startDate, phoneLife);
                        Phone phone = new Phone(loginUsername, userId, brand, type, productNo, startDate, endDate);
                        SetTempNewPhoneByUserId(userId, phone);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }

        private DateTime GetPhoneEndDate(DateTime startDate, int phoneLife)
        {
            DateTime endDate = new DateTime(startDate.Year + phoneLife, startDate.Month, startDate.Day);
            return endDate;
        }

        private void SetTempNewPhoneByUserId(long userId, Phone phone)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, phone);
        }

    }
}
