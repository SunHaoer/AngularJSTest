using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Cache
{
    public class TempPhone
    {
        public static Dictionary<long, Phone> tempNewPhone = new Dictionary<long, Phone>();
        public static Dictionary<long, Phone> tempOldPhone = new Dictionary<long, Phone>();

        public static void SetTempNewPhoneByUserId(long userId, Phone phone)
        {
            tempNewPhone[userId] = phone;
        }

        public static Phone GetTempNewPhoneByUserId(long userId)
        {
            return tempNewPhone.GetValueOrDefault(userId);
        }

        public static void SetTempOldPhoneByUserId(long userId, Phone phone)
        {
            tempOldPhone[userId] = phone;
        }

        public static Phone GetTempOldPhoneByUserId(long userId)
        {
            return tempOldPhone.GetValueOrDefault(userId);
        }

        public static bool IsTempNewPhoneNotEmpty(long userId)
        {
            return !string.IsNullOrEmpty(tempNewPhone.GetValueOrDefault(userId).ProductNo);
        }
    }
}
