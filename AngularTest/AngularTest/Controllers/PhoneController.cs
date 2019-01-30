using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AngularTest.Models;
using System;
using Microsoft.AspNetCore.Http;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly PhoneContext _context;
        private IQueryable<Phone> PhoneIQ;
        private long userId;
        //private static int ans = 0;    // 用于调试

        public PhoneController(PhoneContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 初始化Phone数据库
        /// url:"/api/Phone/InitDB"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool InitPhoneDB()
        {
            if (_context.Phones.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate 20", ProductNo = "110", StartDate = new DateTime(2018, 11, 2), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _context.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "IPHONE", Type = "X", ProductNo = "120", StartDate = new DateTime(2018, 01, 09), EndDate = new DateTime(2019, 01, 09), State = 1 });
                _context.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate RS", ProductNo = "119", StartDate = new DateTime(2017, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 2 });
                _context.Phones.Add(new Phone { PhoneUser = "admin", UserId = 1, Brand = "HUAWEI", Type = "Mate 20", ProductNo = "114", StartDate = new DateTime(2018, 11, 25), EndDate = new DateTime(2019, 11, 25), State = 1 });
                _context.SaveChanges();
                //ans++;
            }
            return true;
        }

        /// <summary>
        /// 获取用户所有的手机
        /// url: "/api/Phone/GetUserPhoneAll"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Phone>> GetUserPhoneAll(string searchString = "")
        {
            //HttpContext.Session.SetString("loginUser", "1,admin");
            //HttpContext.Session.GetString("loginUser");
            userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
            PhoneIQ = _context.Phones.Where(item => item.UserId == userId);
            searchString = searchString.Trim().ToLower();
            PhoneIQ = PhoneIQ.Where(item => item.PhoneUser.ToLower().Contains(searchString) || item.Brand.ToLower().Contains(searchString) || item.Type.ToLower().Contains(searchString) || item.DeleteReason.ToLower().Contains(searchString));
            return PhoneIQ.ToList();
        }

        /// <summary>
        /// 根据id找手机
        /// url: '/api/Phone/GetUserPhoneById'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Phone> GetUserPhoneById(long id)
        {
            return PhoneIQ.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        /// 添加 / 启用
        /// url: '/api/Phone/SaveUserPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="deleteReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SaveUserPhone(long id = 0, string phoneUser = "", string brand = "", string type = "", string productNo = "", DateTime startDate = new DateTime(), DateTime endDate = new DateTime(), DateTime abandonDate = new DateTime(), DateTime deleteDate = new DateTime(), string deleteReason = "", int state = 0)
        {
            userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
            PhoneIQ = _context.Phones.Where(item => item.UserId == userId);
            Phone phone = new Phone(phoneUser, userId, brand, type, productNo, startDate, endDate, abandonDate, deleteDate, deleteReason, 1);
            _context.Phones.Add(phone);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 从数据库删除手机
        /// url: '/api/Phone/DeleteUserPhoneInDBById'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> DeleteUserPhoneInDBById(long id)
        {
            _context.Phones.Remove(_context.Phones.FirstOrDefault(item => item.Id == id));
            _context.SaveChanges();
            return true;
        }

        ///// <summary>
        ///// 弃用
        ///// url: 'api/Phone/AbandonUserPhone'
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="phoneUser"></param>
        ///// <param name="brand"></param>
        ///// <param name="type"></param>
        ///// <param name="productNo"></param>
        ///// <param name="startDate"></param>
        ///// <param name="endDate"></param>
        ///// <param name="deleteDate"></param>
        ///// <param name="deleteReason"></param>
        ///// <param name="state"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult<bool> AbandonUserPhone(long id, string phoneUser, string brand, string type, string productNo, DateTime startDate, DateTime endDate, DateTime abandon, DateTime deleteDate, string deleteReason, int state)
        //{
        //    Phone phone = new Phone(id, phoneUser, userId, brand, type, productNo, startDate, endDate, deleteDate, deleteReason, 2);
        //    _context.Phones.Update(phone);
        //    _context.SaveChanges();
        //    return true;
        //}

        /// <summary>
        /// 弃用手机
        /// url: "/api/Phone/AbandonUserPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> AbandonUserPhoneById(long id, DateTime abandonDate = new DateTime())
        {
            userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
            PhoneIQ = _context.Phones.Where(item => item.UserId == userId);
            Phone phone = PhoneIQ.FirstOrDefault(item => item.Id == id);
            phone.State = 2;
            phone.AbandonDate = abandonDate;
            _context.Phones.Update(phone);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 根据id弃用 -> 启用
        /// url: "/api/Phone/UsingPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> UsingPhoneById(long id)
        {
            userId = long.Parse(HttpContext.Session.GetString("loginUser").Split(",")[0]);
            PhoneIQ = _context.Phones.Where(item => item.UserId == userId);
            Phone phone = PhoneIQ.FirstOrDefault(item => item.Id == id);
            phone.State = 1;
            phone.AbandonDate = new DateTime();
            _context.Phones.Update(phone);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 删除手机
        /// url: "/api/Phone/AbandonUserPhoneById"
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> DeleteUserPhoneById(long id, DateTime deleteDate, string deleteReason)
        {
            Phone phone = _context.Phones.FirstOrDefault(item => item.Id == id);
            phone.State = 3;
            phone.DeleteDate = deleteDate;
            phone.DeleteReason = deleteReason;
            _context.Phones.Update(phone);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 获取登录者信息
        /// url: "/api/Phone/GetLoginUserInfo"
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetLoginUserInfo()
        {
            return HttpContext.Session.GetString("loginUser");
        }

        /// <summary>
        /// 获取登录的用户名
        /// url: "/api/Phone/GetLoginUsername"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> GetLoginUsername()
        {
            return HttpContext.Session.GetString("loginUser").Split(",")[1];
        }

        /// <summary>
        /// 空方法检查是否登录
        /// url: "/api/Phone/CheckLogin"
        /// </summary>
        [HttpGet]
        public void CheckLogin()
        {

        }

    }
}