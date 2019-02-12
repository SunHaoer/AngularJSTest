using AngularTest.Cache;
using AngularTest.Models;
using System;
using System.Linq;

namespace AngularTest.VeiwModels
{
    public class DeletePhoneManage
    {
        private PhoneContext _phoneContext;
        private IQueryable<Phone> phoneIQ;

        public DeletePhoneManage()
        {
        }

        public DeletePhoneManage(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = phoneContext.Phones;
        }

        public void SetTempNewPhoneDeleteByUserId(long userId, string deleteReason, DateTime deleteDate, int state)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.DeleteReason = deleteReason;
            phone.DeleteDate = deleteDate;
            phone.State = state;
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
