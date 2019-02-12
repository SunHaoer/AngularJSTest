using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.Service;
using AngularTest.VeiwModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.ViewModelManage
{
    public class AddPhoneCheckManage
    {
        public PhoneContext _phoneContext;
        public IQueryable<Phone> phoneIQ;
        private readonly BrandContext _brandContext;
        private readonly BrandTypeContext _brandtypeContext;
        private readonly BrandTypeProductNoContext _brandTypeProductNoContext;
        private readonly TypeYearContext _typeYearContext;
        private BrandService brandService;
        private BrandTypeService brandTypeService;
        private BrandTypeProductNoService brandTypeProductNoService;
        private TypeYearService typeYearService;

        public AddPhoneCheckManage()
        {
        }

        public AddPhoneCheckManage(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = _phoneContext.Phones;
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

        public DateTime GetPhoneEndDate(DateTime startDate, int phoneLife)
        {
            DateTime endDate = new DateTime(startDate.Year + phoneLife, startDate.Month, startDate.Day);
            return endDate;
        }

        public void SetTempNewPhoneByUserId(long userId, Phone phone)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, phone);
        }

        public void SetTempNewPhoneToDBByUserId(long userId)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.State = 1;
            _phoneContext.Add(phone);
            _phoneContext.SaveChanges();
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
        }
    }
}

