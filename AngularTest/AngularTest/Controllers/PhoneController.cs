using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Models;
using System;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly PhoneContext _context;

        public PhoneController(PhoneContext context)
        {
            _context = context;

            if (_context.Phones.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                //_context.Phones.Add(new Phone { PhoneUser = "user1", Brand = "HUAWEI", Type = "Mate 20", ProductNo = "110", StartDate = "2018-11-25", EndDate = "2019-11-25", State = 1 });
                //_context.Phones.Add(new Phone { PhoneUser = "user1", Brand = "IPHONE", Type = "X", ProductNo = "120", StartDate = "2018-01-09", EndDate = "2019-01-09", State = 1 });
                //_context.Phones.Add(new Phone { PhoneUser = "user1", Brand = "HUAWEI", Type = "Mate RS", ProductNo = "119", StartDate = "2017-11-25", EndDate = "2019-11-25", State = 2 });
                //_context.Phones.Add(new Phone { PhoneUser = "user1", Brand = "HUAWEI", Type = "Mate 20", ProductNo = "114", StartDate = "2018-11-25", EndDate = "2019-11-25", State = 1 });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// 获取用户所有的手机
        /// url: 'api/Phone/GetUserPhoneAll'
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Phone>> GetUserPhoneAll()
        {
            return _context.Phones.ToList();
        }

        /// <summary>
        /// 根据id找手机
        /// url: 'api/Phone/GetUserPhoneById'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Phone> GetUserPhoneById(long id)
        {
            return _context.Phones.FirstOrDefault(item => item.Id == id);
        }

        /// <summary>
        /// 添加 / 启用
        /// url: 'api/Phone/SaveUserPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="AbandonReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SaveUserPhone(long id = 0, string phoneUser = "", string brand = "", string type = "", string productNo = "", DateTime startDate = new DateTime(), DateTime endDate = new DateTime(), DateTime deleteDate = new DateTime(), string AbandonReason = "", int state = 0)
        {
            Phone phone = new Phone(phoneUser, brand, type, productNo, startDate, endDate, deleteDate, AbandonReason, 1);
            _context.Phones.Add(phone);
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 删除
        /// url: 'api/Phone/DeleteUserPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> DeleteUserPhone(long id)
        {
            _context.Phones.Remove(_context.Phones.FirstOrDefault(item => item.Id == id));
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// 弃用
        /// url: 'api/Phone/AbandonUserPhoneBy'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="AbandonReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> AbandonUserPhone(long id, string phoneUser, string brand, string type, string productNo, DateTime startDate, DateTime endDate, DateTime deleteDate, string AbandonReason, int state)
        {
            Phone phone = new Phone(id, phoneUser, brand, type, productNo, startDate, endDate, deleteDate, AbandonReason, 2);
            _context.Phones.Update(phone);
            _context.SaveChanges();
            return true;
        }

        [HttpPost]
        public ActionResult<bool> AbandonUserPhoneById(long id, DateTime deleteDate)
        {
            Phone phone = _context.Phones.FirstOrDefault(item => item.Id == id);
            phone.State = 2;
            phone.DeleteDate = deleteDate;
            _context.Phones.Update(phone);
            _context.SaveChanges();
            return true;
        }
    }
}