using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class Phone
    {
        public long Id { set; get; }    // Id
        public string PhoneUser { set; get; }    // 使用者
        public long UserId { set; get; }    // 所有者Id
        public string Brand { set; get; }    // 品牌
        public string Type { set; get; }    // 型号
        public string ProductNo { set; get; }    // 编号
        public DateTime StartDate { set; get; }    // 使用日期
        public DateTime EndDate { set; get; }    // 停止使用日期，默认为保质期到期日
        public DateTime AbandonDate { set; get; }   // 弃用日期
        public DateTime DeleteDate { set; get; }    // 删除日期
        public string DeleteReason { set; get; }    // 删除原因
        public int State { set; get; }    // 状态： 1-使用中，2-停用中，3-已删除

        public Phone()
        {
        }

        /// <summary>
        /// all paramters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phoneUser"></param>
        /// <param name="userId"></param>
        /// <param name="brand"></param>
        /// <param name="type"></param>
        /// <param name="productNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="abandonDate"></param>
        /// <param name="deleteDate"></param>
        /// <param name="deleteReason"></param>
        /// <param name="state"></param>
        public Phone(long id, string phoneUser, long userId, string brand, string type, string productNo, DateTime startDate, DateTime endDate, int state)
        {
            Id = id;
            PhoneUser = phoneUser;
            UserId = userId;
            Brand = brand;
            Type = type;
            ProductNo = productNo;
            StartDate = startDate;
            EndDate = endDate;
            State = state;
        }

        public Phone(string phoneUser, long userId, string brand, string type, string productNo, DateTime startDate, DateTime endDate)
        {
            PhoneUser = phoneUser;
            UserId = userId;
            Brand = brand;
            Type = type;
            ProductNo = productNo;
            StartDate = startDate;
            EndDate = endDate;
        }

    }
}
