using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Dao;
using AngularTest.Utils;
using System;

namespace AngularTest.VeiwModels
{
    public class ReplacePhoneManage 
    {
        protected BrandContext _brandContext;
        protected BrandTypeContext _brandTypeContext;
        protected BrandTypeProductNoContext _brandTypeProductNoContext;
        protected TypeYearContext _typeYearContext;
        protected BrandDao brandDao;
        protected BrandTypeDao brandTypeDao;
        protected BrandTypeProductNoDao brandTypeProductNoDao;
        protected TypeYearDao typeYearDao;

        public ReplacePhoneManage(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext)
        {
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            brandDao = new BrandDao(_brandContext);
            brandTypeDao = new BrandTypeDao(_brandTypeContext);
            brandTypeProductNoDao = new BrandTypeProductNoDao(_brandTypeProductNoContext);
            typeYearDao = new TypeYearDao(typeYearContext);
        }

        public ReplacePhonePageViewModel GetReplacePhonePageViewModel(long userId, int nowNode, int isSubmit)
        {
            ReplacePhonePageViewModel model = new ReplacePhonePageViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode * isSubmit, Step.replacePhone] || nowNode == Step.replacePhone)
            {
                model.IsVisitLegal = true;
                model.TempNewPhone = TempPhone.GetTempNewPhoneByUserId(userId);
                model.TempOldPhone = TempPhone.GetTempOldPhoneByUserId(userId);
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

        public FormFeedbackViewModel SubmitMsg(string userInfo, int nowNode, string productNo, string brand, string type, DateTime startDate, DateTime abandonDate)
        {
            FormFeedbackViewModel model = new FormFeedbackViewModel()
            {
                IsLogin = true
            };
            if (Step.stepTable[nowNode, Step.replacePhoneSubmit])
            {
                model.IsVisitLegal = true;
                if (!string.IsNullOrEmpty(productNo) && !string.IsNullOrEmpty(brand) && !string.IsNullOrEmpty(type) && startDate != null)
                {
                    model.IsParameterNotEmpty = true;
                    if (brandTypeProductNoDao.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate) && Validation.IsTwoDaysEquals(startDate, abandonDate))
                    {
                        model.IsParameterLegal = true;
                        long userId = long.Parse(userInfo.Split(",")[0]);
                        string loginUsername = userInfo.Split(",")[1];
                        int phoneLife = typeYearDao.GetYearByType(type);
                        DateTime endDate = GetPhoneEndDate(startDate, phoneLife);
                        Phone phone = new Phone(loginUsername, userId, brand, type, productNo, startDate, endDate);
                        SetTempNewPhoneByUserId(userId, phone);
                        SetTempOldPhoneAbandonDateByUserId(userId, abandonDate);
                        model.IsSuccess = true;
                    }
                }
            }
            return model;
        }

        private void SetTempOldPhoneAbandonDateByUserId(long userId, DateTime abandondate)
        {
            TempPhone.SetTempOldPhoneAbandonDateByUserId(userId, abandondate);
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
