using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.Dao;
using System.Linq;

namespace AngularTest.VeiwModels
{
    public class LoginManage
    {
        private readonly UserContext _userContext;
        private readonly IQueryable<User> userIQ;

        private readonly PhoneDao phoneDao;
        private readonly BrandDao brandDao;
        private readonly BrandTypeDao brandTypeDao;
        private readonly BrandTypeProductNoDao brandTypeProductNoDao;
        private readonly TypeYearDao typeYearDao;
        private readonly DeleteReasonDao deleteReasonDao;

        public LoginManage(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext, DeleteReasonContext deleteReasonContext)
        {
            _userContext = userContext;
            userIQ = _userContext.Users;
            phoneDao = new PhoneDao(phoneContext);
            brandDao = new BrandDao(brandContext);
            brandTypeDao = new BrandTypeDao(brandTypeContext);
            brandTypeProductNoDao = new BrandTypeProductNoDao(brandTypeProductNoContext);
            typeYearDao = new TypeYearDao(typeYearContext);
            deleteReasonDao = new DeleteReasonDao(deleteReasonContext);
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

        public LoginPageViewModel Login(string username, string password)
        {
            LoginPageViewModel model = new LoginPageViewModel
            {
                IsVisitLegal = true
            };
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                User user = GetLoginUser(username, password);
                if (user != null)
                {
                    model.IsLegal = true;
                    model.LoginUserSessionString = user.ToSessionString();
                    SetInitDataBase(user.Id);
                    model.IsLogin = true;
                }
            }
            return model;
        }

        private User GetLoginUser(string username, string password)
        {
            return userIQ.FirstOrDefault(item => item.Username.Equals(username) && item.Password.Equals(password));
        }

        private void SetInitDataBase(long userId)
        {
            phoneDao.InitPhoneDataBase();
            brandDao.InitBrandDataBase();
            brandTypeDao.InitBrandTypeDataBase();
            brandTypeProductNoDao.InitBrandTypeProductNoDataBase();
            typeYearDao.InitTypeYearDataBase();
            deleteReasonDao.InitDeleteReasonDataBase();
            InitUserTempPhone(userId);
            InitUserStepTable(userId);
        }

        private void InitUserTempPhone(long userId)
        {
            if(TempPhone.GetTempNewPhoneByUserId(userId) == null)
            {
                TempPhone.tempNewPhone.Add(userId, new Phone());
            }
        }

        private void InitUserStepTable(long userId)
        {
            Step.InitStepTable();
        }

    }
}
