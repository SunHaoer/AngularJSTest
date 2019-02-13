using AngularTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Service
{
    public class TypeYearService
    {
        private readonly TypeYearContext _typeYearContext;
        private readonly IQueryable<TypeYear> typeYearIQ;

        public TypeYearService(TypeYearContext typeYearContext)
        {
            _typeYearContext = typeYearContext;
            typeYearIQ = _typeYearContext.TypeYears;
        }

        public int GetYearByType(string type)
        {
            return typeYearIQ.FirstOrDefault(item => item.Type.Equals(type.Trim())).Year;
        }
    }
}
