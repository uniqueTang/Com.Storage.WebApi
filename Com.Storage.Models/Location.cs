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
    
    public partial class Location
    {
        public int ID { get; set; }
        public string LocalNum { get; set; }
        public string LocalBarCode { get; set; }
        public string LocalName { get; set; }
        public string StorageNum { get; set; }
        public int StorageType { get; set; }
        public int LocalType { get; set; }
        public string Rack { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public string UnitNum { get; set; }
        public string UnitName { get; set; }
        public string Remark { get; set; }
        public int IsForbid { get; set; }
        public int IsDefault { get; set; }
        public int IsDelete { get; set; }
        public System.DateTime CreateTime { get; set; }
    }
}
