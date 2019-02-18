using AngularTest.Cache;
using AngularTest.Models;
using AngularTest.PageVeiwModels;

namespace AngularTest.VeiwModels
{
    public class ErrorPageManage
    {

        public ErrorPageViewModel GetErrorPageViewModel(long userId)
        {
            ErrorPageViewModel model = new ErrorPageViewModel
            {
                IsLogin = true,
                IsVisitLegal = true
            };
            TempPhone.SetTempPhoneEmpty(userId);
            return model;
        }

    }
}
