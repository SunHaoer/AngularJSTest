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
        static Phone tempPhone = new Phone();

        /// <summary>
        /// 存入tempPhone
        /// url: 'api/DoubleCheck/SetTempPhone'
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="inputDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="AbandonReason"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> SetTempPhone(long id = -1, string phoneUser = "", string brand = "", string type = "", string productNo = "", string inputDate = "", string endDate = "", string deleteDate = "", string AbandonReason = "", int state = -1)
        {
            tempPhone = new Phone(id, phoneUser, brand, type, productNo, inputDate, endDate, deleteDate, AbandonReason, state);
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
    }
}