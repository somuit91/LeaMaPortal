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
    
    public partial class tbl_receiptdt
    {
        public int id { get; set; }
        public Nullable<int> ReceiptNo { get; set; }
        public string Invno { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string Invtype { get; set; }
        public Nullable<float> InvoiceAmount { get; set; }
        public Nullable<float> CreditAmt { get; set; }
        public Nullable<float> ReceivedAmount { get; set; }
        public string Remarks { get; set; }
        public string Description { get; set; }
        public string Delmark { get; set; }
    
        public virtual tbl_receipthd tbl_receipthd { get; set; }
    }
}
