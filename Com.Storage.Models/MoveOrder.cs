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
    
    public partial class MoveOrder
    {
        public int ID { get; set; }
        public string OrderNum { get; set; }
        public int MoveType { get; set; }
        public int ProductType { get; set; }
        public string StorageNum { get; set; }
        public string ContractOrder { get; set; }
        public int Status { get; set; }
        public int IsDelete { get; set; }
        public double Num { get; set; }
        public Nullable<double> Amout { get; set; }
        public Nullable<double> Weight { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUser { get; set; }
        public string AuditUser { get; set; }
        public Nullable<System.DateTime> AuditeTime { get; set; }
        public string PrintUser { get; set; }
        public Nullable<System.DateTime> PrintTime { get; set; }
        public string Reason { get; set; }
        public int OperateType { get; set; }
        public string EquipmentNum { get; set; }
        public string EquipmentCode { get; set; }
        public string Remark { get; set; }
    }
}
