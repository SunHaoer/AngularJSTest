using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.Utils;
using System;
using System.Linq;

namespace AngularTest.Service
{
    public class ChoosePageService
    {
        private readonly PhoneContext _context;
        private readonly IQueryable<Phone> phoneIQ;

        public ChoosePageService(PhoneContext context, IQueryable<Phone> phoneIQ)
        {
            _context = context;
            this.phoneIQ = phoneIQ;
        }

        public PaginatedList<Phone> GetPhoneList(IQueryable<Phone> phoneIQ, int pageIndex, int pageSize)
        {
            return PaginatedList<Phone>.Create(phoneIQ, pageIndex, pageSize);
        }

        public void SetTempPhoneEmpty(long userId)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
            TempPhone.SetTempOldPhoneByUserId(userId, new Phone()); 
        }

        public bool ValidateId(long id, long loginUserId)
        {
            return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State != 3);
        }

        public Phone GetPhoneById(long id)
        {
            return phoneIQ.FirstOrDefault(item => item.Id == id);
        }

        public Phone UpdatePhoneState(Phone phone, DateTime abandonDate, int newState)
        {
            phone.State = newState;
            phone.AbandonDate = abandonDate;
            return phone;
        }

        public void UpdatePhoneStateInDB(Phone phone)
        {
            _context.Update(phone);
            _context.SaveChanges();
        }

    }
}
