using System;
using System.Linq;
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
        private long userId;

        public TempPhoneController(PhoneContext context)
        {
            _context = context;
            //userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
        }

        static Phone newTempPhone = new Phone();
        static Phone oldTempPhone = new Phone();
        static long oldId = -1;
        static int tempPageIndex = 1;

        /// <summary>
        /// 存入newNempPhone
        /// url: '/api/TempPhone/SetNewTempPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="StartDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="deleteReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetNewTempPhone(long id = 0, string phoneUser = "", string brand = "", string type = "", string productNo = "", DateTime startDate = new DateTime(), DateTime endDate = new DateTime(), DateTime abandonDate = new DateTime(), DateTime deleteDate = new DateTime(), string deleteReason = "", int state = 0)
        {
            userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
            newTempPhone = new Phone(id, phoneUser, userId, brand, type, productNo, startDate, endDate, abandonDate, deleteDate, deleteReason, state);
            return true;
        }

        /// <summary>
        /// 存入oldNempPhone
        /// url: '/api/TempPhoneheck/SetOldTempPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="StartDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="deleteReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetOldTempPhone(long id = 0, string phoneUser = "", string brand = "", string type = "", string productNo = "", DateTime startDate = new DateTime(), DateTime endDate = new DateTime(), DateTime abandonDate = new DateTime(), DateTime deleteDate = new DateTime(), string deleteReason = "", int state = 0)
        {
            userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
            oldTempPhone = new Phone(id, phoneUser, userId, brand, type, productNo, startDate, endDate, abandonDate, deleteDate, deleteReason, state);
            return true;
        }

        /// <summary>
        /// 设置旧手机的abandon时间
        /// url: "/api/TempPhone/UpdateOldTempPhoneAbandonDate"
        /// </summary>
        /// <param name="abandonDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> UpdateOldTempPhoneAbandonDate(DateTime abandonDate = new DateTime())
        {
            oldTempPhone.AbandonDate = abandonDate;
            return true;
        }

        /// <summary>
        /// 将对应id的phone存入newTemp
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
        /// 将对应id的phone存入oldTemp
        /// url: '/api/TempPhone/SetOldTempPhoneById'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetOldTempPhoneById(long id)
        {
            oldTempPhone = _context.Phones.FirstOrDefault(item => item.Id == id);
            return true;
        }

        /// <summary>
        /// 从newTempPhone中取出
        /// url: '/api/TempPhone/GetNewTempPhone'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Phone> GetNewTempPhone()
        {
            return newTempPhone;
        }

        /// <summary>
        /// 从oldTempPhone中取出
        /// url: '/api/TempPhone/GetOldTempPhone'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Phone> GetOldTempPhone()
        {
            return oldTempPhone;
        }

        /// <summary>
        /// 存入旧id
        /// url: '/api/TempPhone/SetOldId'
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
        /// url: '/api/TempPhone/GetOldId'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<long> GetOldId()
        {
            return oldId;
        }

        /// <summary>
        /// 保存当前页面
        /// url: '/api/TempPhone/SetTempPageIndex'
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<bool> SetTempPageIndex(int pageIndex)
        {
            tempPageIndex = pageIndex;
            return true;
        }

        /// <summary>
        /// 获取当前页码
        /// url: '/api/TempPhone/GetTempPageIndex'
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public ActionResult<int> GetTempPageIndex()
        {
            return tempPageIndex;
        }
    }
}