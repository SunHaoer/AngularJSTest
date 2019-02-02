using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Utils
{
    public class Validation
    {
        public static bool IsDateLegal(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            if(year < 1900 || year > 2100)
            {
                return false;
            }
            else
            {
                int[,] yearDate = new int[2, 13] { { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 }, { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 } };    
                int yearType = 0;    
                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                {
                    yearType = 1;    
                }
                if (month < 1 || month > 12)
                {
                    return false;    
                }
                if (day > yearDate[yearType, month])
                {
                    return false;    
                }
            }
            return true;
        } 

        public static bool IsDateNotBeforeToday(DateTime date)
        {
            if(IsDateLegal(date))
            {
                DateTime today = DateTime.Now;
                if(date.Year != today.Year)
                {
                    return date.Year > today.Year;
                }
                else if(date.Month != today.Month)
                {
                    return date.Month > today.Month;
                }
                else
                {
                    return date.Day >= today.Day;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
