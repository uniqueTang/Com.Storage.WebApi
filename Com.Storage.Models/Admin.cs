//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Com.Storage.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Admin
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string UserCode { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateIp { get; set; }
        public string CreateUser { get; set; }
        public int LoginCount { get; set; }
        public string Picture { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public short IsDelete { get; set; }
        public short Status { get; set; }
        public string DepartNum { get; set; }
        public string ParentCode { get; set; }
        public string RoleNum { get; set; }
        public string Remark { get; set; }
    }
}
