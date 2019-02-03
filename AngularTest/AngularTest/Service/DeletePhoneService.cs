using AngularTest.Cache;
using AngularTest.Models;
using System;
using System.Linq;

namespace AngularTest.Service
{
    public class DeletePhoneService
    {
        private PhoneContext _phoneContext;
        private IQueryable<Phone> phoneIQ;

        public DeletePhoneService()
        {
        }

        public DeletePhoneService(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = phoneContext.Phones;
        }

        public void SetTempNewPhoneDeleteByUserId(long userId, string deleteReason, DateTime deleteDate)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.DeleteReason = deleteReason;
            phone.DeleteDate = deleteDate;
            TempPhone.SetTempNewPhoneByUserId(userId, phone);
        }

        public void SetTempNewPhoneToDBByUserId(long userId)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.State = 3;
            _phoneContext.Update(phone);
            _phoneContext.SaveChanges();
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
        }
    }
}
