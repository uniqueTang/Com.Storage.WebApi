using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Storage.WebApi.Models.BasicDto
{
    /// <summary>
    /// 库位条件查询（Dto层）
    /// </summary>
    public class LocationDto
    {
        /// <summary>
        /// 仓库类型
        /// </summary>
        public int StorageType { get; set; }

        /// <summary>
        /// 入库类型
        /// </summary>
        public int LocalType { get; set; }

        /// <summary>
        /// 库名
        /// </summary>
        public string LocalName { get; set; }
    }
}