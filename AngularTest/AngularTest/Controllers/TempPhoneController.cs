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
    public class TempPhoneController : ControllerBase
    {
        private readonly PhoneContext _context;
        private readonly long userId = 1;

        public TempPhoneController(PhoneContext context)
        {
            _context = context;
            //userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
        }

        static Phone newTempPhone = new Phone();
        static long oldId = -1;

        /// <summary>
        /// 存入tempPhone
        /// url: 'api/TempPhoneCheck/SetNewTempPhone'
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
        public ActionResult<bool> SetNewTempPhone(long id = 0, string phoneUser = "", string brand = "", string type = "", string productNo = "", DateTime startDate = new DateTime(), DateTime endDate = new DateTime(), DateTime deleteDate = new DateTime(), string AbandonReason = "", int state = 0)
        {
            newTempPhone = new Phone(id, phoneUser, userId, brand, type, productNo, startDate, endDate, deleteDate, AbandonReason, state);
            return true;
        }

        /// <summary>
        /// 将对应id的phone存入temp
        /// url: '/api/TempPhone/SetNewTempPhoneById'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetNewTempPhoneById(long id)
        {
            newTempPhone = _context.Phones.FirstOrDefault(item => item.Id == id);
            return true;
        }

        /// <summary>
        /// 从tempPhone中取出
        /// url: '/api/TempPhoneCheck/GetNewTempPhone'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Phone> GetNewTempPhone()
        {
            return newTempPhone;
        }

        /// <summary>
        /// 存入旧id
        /// url: '/api/DoubleCheck/SetOldId'
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
        /// url: '/api/DoubleCheck/GetOldId'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<long> GetOldId()
        {
            return oldId;
        }
    }
}