using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularTest.Data;
using AngularTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeleteReasonModelController : ControllerBase
    {
        private readonly DeleteReasonContext _context;
        public DeleteReasonModelController(DeleteReasonContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 初始化BrandTypeProductNo数据库
        /// url: "DeleteReasonModel/InitDeleteReasonDB"
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public bool InitDeleteReasonDB()
        {
            if (_context.DeleteReasons.Count() == 0)
            {
                _context.DeleteReasons.Add(new deleteReason("lost"));
                _context.DeleteReasons.Add(new deleteReason("buy new phone"));
                _context.DeleteReasons.Add(new deleteReason("time end"));
                _context.DeleteReasons.Add(new deleteReason("other"));
                _context.SaveChanges();
            
            }
            return true;
        }
        /// <summary>
        /// 获取删除原因列表
        /// url: '/api/DeleteReason/GetdeleteReasonAll'
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult<List<deleteReason>> GetdeleteReasonAll()
        {
            return _context.DeleteReasons.ToList();
        }
    }
}
