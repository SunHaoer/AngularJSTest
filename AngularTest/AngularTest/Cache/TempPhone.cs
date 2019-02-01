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
    }
}
