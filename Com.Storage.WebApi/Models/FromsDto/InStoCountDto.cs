using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Storage.WebApi.Models.FromsDto
{
    [Serializable]
    public class InStoCountDto
    {
        // 状态
        public int sky { get; set; } = 0;

        // <summary>
        // 起始时间往前5年
        DateTime startTime = DateTime.Now.AddYears(-5);
        public DateTime StartTime { get { return startTime; } set { startTime = value; } }

        // 结束时间往后1年
        DateTime endTime = DateTime.Now.AddYears(1);
        public DateTime EndTime { get { return endTime; } set { endTime = value; } }
    }
}