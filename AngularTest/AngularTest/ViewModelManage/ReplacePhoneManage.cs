using AngularTest.Cache;
using AngularTest.Models;
using System;

namespace AngularTest.VeiwModels
{
    public class ReplacePhoneManage : AddPhoneManage
    {

        public ReplacePhoneManage()
        {
        }

        public ReplacePhoneManage(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = _phoneContext.Phones;
        }

        public void SetTempOldPhoneAbandonDateByUserId(long userId, DateTime abandondate)
        {
            TempPhone.SetTempOldPhoneAbandonDateByUserId(userId, abandondate);
        }

        public void SetTemoOldPhoneAbandonToDBByUserId(long userId)
        {
            Phone phone = TempPhone.GetTempOldPhoneByUserId(userId);
            phone.State = 2;
            _phoneContext.Update(phone);
            _phoneContext.SaveChanges();
            TempPhone.SetTempOldPhoneByUserId(userId, new Phone());
        }
    }
}
