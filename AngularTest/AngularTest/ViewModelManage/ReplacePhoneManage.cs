using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.PageVeiwModels;
using AngularTest.Service;
using AngularTest.Utils;
using System;

namespace AngularTest.VeiwModels
{
    public class ReplacePhoneManage : AddPhoneManage
    {

        public ReplacePhoneManage(BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext) : base(brandContext, brandTypeContext, brandTypeProductNoContext, typeYearContext)
        {
            _brandContext = brandContext;
            _brandTypeContext = brandTypeContext;
            _brandTypeProductNoContext = brandTypeProductNoContext;
            _typeYearContext = typeYearContext;
            brandService = new BrandService(_brandContext);
            brandTypeService = new BrandTypeService(_brandTypeContext);
            brandTypeProductNoService = new BrandTypeProductNoService(_brandTypeProductNoContext);
            typeYearService = new TypeYearService(typeYearContext);
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
                model.BrandList = brandService.GetBrandList();
                model.TypeList = brandTypeService.GetBrandTypeList();
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
                    if (brandTypeProductNoService.ValidateBrandTypeProductNo(brand, type, productNo) && Validation.IsDateNotBeforeToday(startDate) && Validation.IsTwoDaysEquals(startDate, abandonDate))
                    {
                        model.IsParameterLegal = true;
                        long userId = long.Parse(userInfo.Split(",")[0]);
                        string loginUsername = userInfo.Split(",")[1];
                        int phoneLife = typeYearService.GetYearByType(type);
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

    }
}
