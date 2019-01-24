using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoubleCheckController : ControllerBase
    {
        private readonly PhoneContext _context;

        public DoubleCheckController(PhoneContext context)
        {
            _context = context;
        }

        static Phone tempPhone = new Phone();
        static long oldId = -1;

        /// <summary>
        /// 存入tempPhone
        /// url: 'api/DoubleCheck/SetTempPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="StartDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="AbandonReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetTempPhone(long id, string phoneUser, string brand, string type, string productNo, DateTime startDate, DateTime endDate, DateTime deleteDate, string AbandonReason, int state)
        {
            
            tempPhone = new Phone(id, phoneUser, brand, type, productNo, startDate, endDate, deleteDate, AbandonReason, state);
            return true;
        }

        /// <summary>
        /// 将对应id的phone存入temp
        /// url: 'api/DoubleCheck/SetTempPhoneById'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetTempPhoneById(long id)
        {
            tempPhone = _context.Phones.FirstOrDefault(item => item.Id == id);
            return true;
        }

        /// <summary>
        /// 从tempPhone中取出
        /// url: 'api/DoubleCheck/GetTempPhone'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Phone> GetTempPhone()
        {
            return tempPhone;
        }

        /// <summary>
        /// 存入旧id
        /// url: 'api/DoubleCheck/SetOldId'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetOldId(long id = -1)
        {
            oldId = id;
            return true;
        }

        /// <summary>
        /// 取出旧id
        /// url: 'api/DoubleCheck/GetOldId'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<long> GetOldId()
        {
            return oldId;
        }
    }
}