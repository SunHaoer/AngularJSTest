using AngularTest.Models;
using System;
using System.Linq;

namespace AngularTest.Service
{
    public class LoginService
    {
        private readonly UserContext _userContext;
        private readonly PhoneContext _phoneContext;
        private readonly IQueryable<User> userIQ;
        private readonly IQueryable<Phone> phoneIQ;

        public LoginService(UserContext userContext, PhoneContext phoneContext, IQueryable<User> userIQ, IQueryable<Phone> phoneIQ)
        {
            _userContext = userContext;
            _phoneContext = phoneContext;
            this.userIQ = userIQ;
            this.phoneIQ = phoneIQ;
        }

        public void SetInitData()
        {
            if(userIQ.Count() == 0)
            {
                _userContext.Add(new User("admin", "root"));
                _userContext.Add(new User("dillon", "sunhao"));
                _userContext.SaveChanges();
            }
        }

        public User GetLoginUser(string username, string password)
        {
            return userIQ.FirstOrDefault(item => item.Username.Equals(username) && item.Password.Equals(password));
        }

        public void SetInitDataBase()
        {
            InitPhoneDataBase();
        }

        private void InitPhoneDataBase()
        {
            if (phoneIQ.Count() == 0)
            {
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 2), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "IPHONE", Type = "X", ProductNo = "IPHONEX1", StartDate = new DateTime(2018, 01, 09), EndDate = new DateTime(2019, 01, 09), State = 3 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "MateRS", ProductNo = "HUAWEIMateRS1", StartDate = new DateTime(2017, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 2 });
                _phoneContext.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate20", ProductNo = "HUAWEIMate201", StartDate = new DateTime(2018, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _phoneContext.SaveChanges();
            }
        }
    }
}
