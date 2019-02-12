using AngularTest.Cache;
using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
