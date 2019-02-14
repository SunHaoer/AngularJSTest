using AngularTest.Models;
using System;
using System.Collections.Generic;

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

        public static void SetTempOldPhoneByUserId(long userId, Phone phone)
        {
            tempOldPhone[userId] = phone;
        }

        public static void SetTempPhoneEmpty(long userId)
        {
            SetTempNewPhoneByUserId(userId, new Phone());
            SetTempOldPhoneByUserId(userId, new Phone());
        }

        public static Phone GetTempNewPhoneByUserId(long userId)
        {
            return tempNewPhone.GetValueOrDefault(userId);
        }

        public static Phone GetTempOldPhoneByUserId(long userId)
        {
            return tempOldPhone.GetValueOrDefault(userId);
        }

        public static bool IsTempNewPhoneNotEmpty(long userId)
        {
            return !string.IsNullOrEmpty(tempNewPhone.GetValueOrDefault(userId).ProductNo);
        }

        public static bool IsTempOldPhoneNotEmpty(long userId)
        {
            return !string.IsNullOrEmpty(tempOldPhone.GetValueOrDefault(userId).ProductNo);
        }

        public static void SetTempOldPhoneAbandonDateByUserId(long userId, DateTime abandonDate)
        {
            Phone phone = GetTempOldPhoneByUserId(userId);
            phone.AbandonDate = abandonDate;
            SetTempOldPhoneByUserId(userId, phone);
        }
    }
}
