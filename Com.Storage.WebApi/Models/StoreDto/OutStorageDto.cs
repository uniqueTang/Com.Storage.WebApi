using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Storage.WebApi.Models.StoreDto
{
    /// <summary>
    /// 出库条件查询（Dto）
    /// </summary>
    [Serializable]
    public class OutStorageDto
    {
        // 状态
        public int Status { get; set; } = 0;

        // <summary>
        // 起始时间往前5年
        DateTime startTime = DateTime.Now.AddYears(-5);
        public DateTime StartTime { get { return startTime; } set { startTime = value; } }

        // 结束时间往后1年
        DateTime endTime = DateTime.Now.AddYears(1);
        public DateTime EndTime { get { return endTime; } set { endTime = value; } }

        // 供应商名称
        public string cusName { get; set; }

        // 关联单号
        public string ContractOrder { get; set; }

        // 入库类型
        public int OutType { get; set; } = 0;
    }
}