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
    
    public partial class tbl_agreement_pdc
    {
        public int id { get; set; }
        public Nullable<int> Agreement_No { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string BankName { get; set; }
        public string Cheque_No { get; set; }
        public string Cheque_Date { get; set; }
        public Nullable<float> Cheque_Amount { get; set; }
        public string Payment_Mode { get; set; }
        public string Delmark { get; set; }
    
        public virtual tbl_agreement tbl_agreement { get; set; }
    }
}
