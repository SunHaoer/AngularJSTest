using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.Utils;
using System;
using System.Linq;

namespace AngularTest.Dao
{
    public class PhoneDao
    {
        private readonly PhoneContext _phoneContext;
        private readonly IQueryable<Phone> phoneIQ;

        public PhoneDao(PhoneContext phoneContext)
        {
            _phoneContext = phoneContext;
            phoneIQ = _phoneContext.Phones;
        }

        public IQueryable<Phone> GetPhoneIQByUserId(long userId)
        {
            return phoneIQ.Where(item => item.UserId == userId);
        }

        public PaginatedList<Phone> GetPhoneList(long userId, int pageIndex, int pageSize)
        {
            IQueryable<Phone> phoneIQ = GetPhoneIQByUserId(userId);
            phoneIQ = phoneIQ.OrderBy(item => item.State);
            return PaginatedList<Phone>.Create(phoneIQ, pageIndex, pageSize);
        }

        public bool ValidateIdInAbandonOrDelete(long id, long loginUserId)
        {
            return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State != Consts.DELETED);
        }

        public bool ValidateIdInReplace(long id, long loginUserId)
        {
            return phoneIQ.Any(item => item.Id == id && item.UserId == loginUserId && item.State == Consts.IN_USING);
        }

        public Phone GetPhoneById(long id)
        {
            return phoneIQ.FirstOrDefault(item => item.Id == id);
        }

        public void UpdatePhoneStateInDB(Phone phone)
        {
            _phoneContext.Update(phone);
            _phoneContext.SaveChanges();
        }

        public void SetTempNewPhoneToDBByUserId(long userId)
        {
            Phone phone = TempPhone.GetTempNewPhoneByUserId(userId);
            phone.State = Consts.IN_USING;
            _phoneContext.Add(phone);
            _phoneContext.SaveChanges();
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
        }

        public void SetTemoOldPhoneAbandonToDBByUserId(long userId)
        {
            Phone phone = TempPhone.GetTempOldPhoneByUserId(userId);
            phone.State = Consts.ABANDONED;
            _phoneContext.Update(phone);
            _phoneContext.SaveChanges();
            TempPhone.SetTempOldPhoneByUserId(userId, new Phone());
        }

        public void InitPhoneDataBase()
        {
            if (phoneIQ.Count() == 0)
            {
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 2), EndDate = new DateTime(2019, 11, 2), State = Consts.IN_USING });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "IPHONE", Type = "iphoneX", ProductNo = "IPHONEiphoneX1", StartDate = new DateTime(2018, 01, 09), EndDate = new DateTime(2019, 01, 09), State = Consts.IN_USING });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "MateRS", ProductNo = "HUAWEIMateRS1", StartDate = new DateTime(2017, 11, 25), EndDate = new DateTime(2019, 11, 25), State = Consts.IN_USING });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 25), EndDate = new DateTime(2019, 11, 25), State = Consts.IN_USING });
                _phoneContext.SaveChanges();
            }
        }

    }
}
