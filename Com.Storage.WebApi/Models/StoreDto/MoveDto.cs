using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Storage.WebApi.Models.StoreDto
{
    /// <summary>
    /// 移库条件查询使用
    /// </summary>
    [Serializable]
    public class MoveDto
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

        // 编号
        public string OrderNum { get; set; }
    }
}