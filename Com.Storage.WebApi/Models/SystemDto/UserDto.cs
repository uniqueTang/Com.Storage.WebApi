using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Com.Storage.WebApi.Models.SystemDto
{
    /// <summary>
    /// 用户（Dto）用于条件查询
    /// </summary>
   [Serializable]
    public class UserDto
    {
        // 真名
        public string RealName { get; set; }

        // 部门编号
        public string DepartNum { get; set; }

        // 角色编号
        public string RoleNum { get; set; }

    }
}