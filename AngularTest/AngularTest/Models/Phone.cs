using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Models
{
    public class Phone
    {
        public long Id { get; set; }    // Id
        public string PhoneUser { get; set; }    // 使用者
        public string Brand { get; set; }    // 品牌
        public string Type { get; set; }    // 型号
        public string ProductNo { get; set; }    // 编号
        public DateTime StartDate { get; set; }    // 使用日期
        public DateTime EndDate { get; set; }    // 停止使用日期，默认为保质期到期日
        public DateTime DeleteDate { get; set; }    // 删除日期
        public string AbandonReason { get; set; }    // 删除原因
        public int State { get; set; }    // 状态： 1-使用中，2-停用中，3-已删除

        public Phone()
        {
        }

        public Phone(long id, string phoneUser, string brand, string type, string productNo, DateTime startDate, DateTime endDate, DateTime deleteDate, string abandonReason, int state)
        {
            Id = id;
            PhoneUser = phoneUser;
            Brand = brand;
            Type = type;
            ProductNo = productNo;
            StartDate = startDate;
            EndDate = endDate;
            DeleteDate = deleteDate;
            AbandonReason = abandonReason;
            State = state;
        }

    }
}
