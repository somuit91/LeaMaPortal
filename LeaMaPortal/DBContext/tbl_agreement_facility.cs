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
    
    public partial class tbl_agreement_facility
    {
        public int id { get; set; }
        public Nullable<int> Agreement_No { get; set; }
        public string Facility_id { get; set; }
        public string Facility_Name { get; set; }
        public Nullable<int> Numbers_available { get; set; }
        public string Delmark { get; set; }
    
        public virtual tbl_agreement tbl_agreement { get; set; }
        public virtual tbl_facilitymaster tbl_facilitymaster { get; set; }
    }
}
