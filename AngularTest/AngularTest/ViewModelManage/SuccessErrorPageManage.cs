using AngularTest.Cache;
using AngularTest.Models;

namespace AngularTest.VeiwModels
{
    public class SuccessErrorPageManage
    {
        public void SetTempPhoneEmpty(long userId)
        {
            TempPhone.SetTempNewPhoneByUserId(userId, new Phone());
            TempPhone.SetTempOldPhoneByUserId(userId, new Phone());
        }
    }
}
