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
    
    public partial class tbl_suppliermaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_suppliermaster()
        {
            this.tbl_supplierdt = new HashSet<tbl_supplierdt>();
            this.tbl_supplierdt1 = new HashSet<tbl_supplierdt1>();
        }
    
        public int id { get; set; }
        public int Supplier_Id { get; set; }
        public string Supplier_Name { get; set; }
        public string Marital_Status { get; set; }
        public string Title { get; set; }
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string address { get; set; }
        public string address1 { get; set; }
        public string Locationlink { get; set; }
        public string Emirate { get; set; }
        public string City { get; set; }
        public string PostboxNo { get; set; }
        public string Email { get; set; }
        public string Mobile_Countrycode { get; set; }
        public string Mobile_Areacode { get; set; }
        public string Mobile_No { get; set; }
        public string Landline_Countrycode { get; set; }
        public string Landline_Areacode { get; set; }
        public string Landline_No { get; set; }
        public string Fax_Countrycode { get; set; }
        public string Fax_Areacode { get; set; }
        public string Fax_No { get; set; }
        public string Nationality { get; set; }
        public string Actitvity { get; set; }
        public string Cocandindustryuid { get; set; }
        public string TradelicenseNo { get; set; }
        public Nullable<System.DateTime> License_issueDate { get; set; }
        public Nullable<System.DateTime> License_ExpiryDate { get; set; }
        public string Issuance_authority { get; set; }
        public string ADWEA_Regid { get; set; }
        public string Type { get; set; }
        public Nullable<int> Accyear { get; set; }
        public Nullable<System.DateTime> Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_supplierdt> tbl_supplierdt { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_supplierdt1> tbl_supplierdt1 { get; set; }
    }
}
