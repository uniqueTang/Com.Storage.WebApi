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
    
    public partial class Supplier
    {
        public int ID { get; set; }
        public string SupNum { get; set; }
        public string SupName { get; set; }
        public Nullable<int> SupType { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string CreateUser { get; set; }
        public string Description { get; set; }
        public string ContractNum { get; set; }
        public int IsDelete { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
