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

        public bool ValidateIdInAbandon(long id, long loginUserId)
        {
            return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State != 3);
        }

        public bool ValidateIdInReplace(long id, long loginUserId)
        {
            return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State == 1);
        }

        public Phone GetPhoneById(long id)
        {
            return phoneIQ.FirstOrDefault(item => item.Id == id);
        }

        public Phone UpdatePhoneState(Phone phone, DateTime date, int newState)
        {
            phone.State = newState;
            if(newState == 2)
            {
                phone.AbandonDate = date;
            }
            else
            {
                phone.StartDate = date;
                phone.AbandonDate = new DateTime();
            }
            return phone;
        }

        public void UpdatePhoneStateInDB(Phone phone)
        {
            _context.Update(phone);
            _context.SaveChanges();
        }

    }
}
