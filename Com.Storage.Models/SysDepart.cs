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
    
    public partial class SysDepart
    {
        public int ID { get; set; }
        public string DepartNum { get; set; }
        public string DepartName { get; set; }
        public int ChildCount { get; set; }
        public string ParentNum { get; set; }
        public int Depth { get; set; }
        public int IsDelete { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
