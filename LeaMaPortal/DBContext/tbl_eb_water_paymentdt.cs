//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeaMaPortal.DBContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_eb_water_paymentdt
    {
        public int id { get; set; }
        public Nullable<int> PaymentNo { get; set; }
        public Nullable<int> Refno { get; set; }
        public string Meterno { get; set; }
        public string billNo { get; set; }
        public Nullable<System.DateTime> billDate { get; set; }
        public Nullable<float> billAmount { get; set; }
        public Nullable<float> PaidAmount { get; set; }
        public Nullable<float> Debitamt { get; set; }
        public string Remarks { get; set; }
        public string Delmark { get; set; }
    
        public virtual tbl_eb_water_paymenthd tbl_eb_water_paymenthd { get; set; }
    }
}