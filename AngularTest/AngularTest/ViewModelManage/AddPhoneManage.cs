using AngularTest.Cache;
using AngularTest.Models;
using System;
using System.Linq;

namespace AngularTest.VeiwModels
{
    public class AddPhoneManage
    {
        public PhoneContext _phoneContext;
        public IQueryable<Phone> phoneIQ;

        public AddPhoneManage()
        {
        }

        public AddPhoneManage(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = _phoneContext.Phones;
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
