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
    
    public partial class CheckData
    {
        public int ID { get; set; }
        public string OrderNum { get; set; }
        public string LocalNum { get; set; }
        public string LocalName { get; set; }
        public string StorageNum { get; set; }
        public string ProductNum { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public string BatchNum { get; set; }
        public Nullable<double> LocalQty { get; set; }
        public Nullable<double> FirstQty { get; set; }
        public Nullable<double> SecondQty { get; set; }
        public Nullable<double> DifQty { get; set; }
        public string FirstUser { get; set; }
        public string SecondUser { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
