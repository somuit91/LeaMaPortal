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
    
    public partial class tbl_agreement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_agreement()
        {
            this.tbl_agreement_facility = new HashSet<tbl_agreement_facility>();
            this.tbl_agreement_unit_inner = new HashSet<tbl_agreement_unit_inner>();
            this.tbl_agreement_pdc = new HashSet<tbl_agreement_pdc>();
            this.tbl_agreement_checklist = new HashSet<tbl_agreement_checklist>();
            this.tbl_agreement_doc = new HashSet<tbl_agreement_doc>();
            this.tbl_agreement_utility = new HashSet<tbl_agreement_utility>();
        }
    
        public int id { get; set; }
        public string Single_Multiple_Flag { get; set; }
        public Nullable<int> Agreement_Refno { get; set; }
        public string New_Renewal_flag { get; set; }
        public int Agreement_No { get; set; }
        public Nullable<System.DateTime> Agreement_Date { get; set; }
        public int Ag_Tenant_id { get; set; }
        public string Ag_Tenant_Name { get; set; }
        public int property_id { get; set; }
        public string Property_ID_Tawtheeq { get; set; }
        public string Properties_Name { get; set; }
        public string Unit_ID_Tawtheeq { get; set; }
        public string Unit_Property_Name { get; set; }
        public int Caretaker_id { get; set; }
        public string Caretaker_Name { get; set; }
        public Nullable<System.DateTime> Vacantstartdate { get; set; }
        public Nullable<System.DateTime> Agreement_Start_Date { get; set; }
        public Nullable<System.DateTime> Agreement_End_Date { get; set; }
        public Nullable<float> Total_Rental_amount { get; set; }
        public Nullable<float> Perday_Rental { get; set; }
        public Nullable<float> Advance_Security_Amount { get; set; }
        public string Security_Flag { get; set; }
        public string Security_chequeno { get; set; }
        public Nullable<System.DateTime> Security_chequedate { get; set; }
        public Nullable<int> Notice_Period { get; set; }
        public Nullable<int> nofopayments { get; set; }
        public Nullable<int> Approval_Flag { get; set; }
        public string Approved_By { get; set; }
        public Nullable<System.DateTime> Approved_Date { get; set; }
        public string Tenant_Type { get; set; }
        public string Status { get; set; }
        public Nullable<int> Accyear { get; set; }
        public Nullable<System.DateTime> Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_facility> tbl_agreement_facility { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_unit_inner> tbl_agreement_unit_inner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_pdc> tbl_agreement_pdc { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_checklist> tbl_agreement_checklist { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_doc> tbl_agreement_doc { get; set; }
        public virtual tbl_propertiesmaster tbl_propertiesmaster { get; set; }
        public virtual tbl_caretaker tbl_caretaker { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_agreement_utility> tbl_agreement_utility { get; set; }
        public virtual tbl_agreement_status tbl_agreement_status { get; set; }
    }
}