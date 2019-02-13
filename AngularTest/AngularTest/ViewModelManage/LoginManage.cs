using AngularTest.Cache;
using AngularTest.Data;
using AngularTest.Models;
using AngularTest.Service;
using System.Linq;

namespace AngularTest.VeiwModels
{
    public class LoginManage
    {
        private readonly UserContext _userContext;
        private readonly IQueryable<User> userIQ;

        private readonly PhoneService phoneService;
        private readonly BrandService brandService;
        private readonly BrandTypeService brandTypeService;
        private readonly BrandTypeProductNoService brandTypeProductNoService;
        private readonly TypeYearService typeYearService;
        private readonly DeleteReasonService deleteReasonService;

        public LoginManage(UserContext userContext, PhoneContext phoneContext, BrandContext brandContext, BrandTypeContext brandTypeContext, BrandTypeProductNoContext brandTypeProductNoContext, TypeYearContext typeYearContext, DeleteReasonContext deleteReasonContext)
        {
            _userContext = userContext;
            userIQ = _userContext.Users;
            phoneService = new PhoneService(phoneContext);
            brandService = new BrandService(brandContext);
            brandTypeService = new BrandTypeService(brandTypeContext);
            brandTypeProductNoService = new BrandTypeProductNoService(brandTypeProductNoContext);
            typeYearService = new TypeYearService(typeYearContext);
            deleteReasonService = new DeleteReasonService(deleteReasonContext);
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
            phoneService.InitPhoneDataBase();
            brandService.InitBrandDataBase();
            brandTypeService.InitBrandTypeDataBase();
            brandTypeProductNoService.InitBrandTypeProductNoDataBase();
            typeYearService.InitTypeYearDataBase();
            deleteReasonService.InitDeleteReasonDataBase();
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
