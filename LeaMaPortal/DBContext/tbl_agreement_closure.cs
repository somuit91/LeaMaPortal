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
    
    public partial class tbl_agreement_closure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_agreement_closure()
        {
            this.tbl_agreement_closure_checklist = new HashSet<tbl_agreement_closure_checklist>();
            this.tbl_agreement_closure_facility = new HashSet<tbl_agreement_closure_facility>();
            this.tbl_agreement_closure_pdc = new HashSet<tbl_agreement_closure_pdc>();
            this.tbl_agreement_closure_utility = new HashSet<tbl_agreement_closure_utility>();
        }
    
        public int id { get; set; }
        public int Agreement_No { get; set; }
        public Nullable<float> Advance_pending { get; set; }
        public string Advance_Security_Amount_Paid { get; set; }
        public string Less_any_damanges { get; set; }
        public Nullable<float> Amount_to_be_refunded { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Availabledate { get; set; }
        public Nullable<int> Accyear { get; set; }
        public Nullable<System.DateTime> Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
    
        public virtual tbl_agreement_status tbl_agreement_status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_checklist> tbl_agreement_closure_checklist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_facility> tbl_agreement_closure_facility { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_pdc> tbl_agreement_closure_pdc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_closure_utility> tbl_agreement_closure_utility { get; set; }
    }
}