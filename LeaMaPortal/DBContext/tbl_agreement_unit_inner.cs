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
    
    public partial class tbl_agreement_unit_inner
    {
        public int id { get; set; }
        public Nullable<int> Agreement_No { get; set; }
        public Nullable<int> Property_ID { get; set; }
        public string Property_ID_Tawtheeq { get; set; }
        public string Properties_Name { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }
        public string Delmark { get; set; }
    
        public virtual tbl_agreement tbl_agreement { get; set; }
    }
}
