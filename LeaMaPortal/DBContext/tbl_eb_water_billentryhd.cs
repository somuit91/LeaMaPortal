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
    
    public partial class tbl_eb_water_billentryhd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_eb_water_billentryhd()
        {
            this.tbl_eb_water_billentrydt = new HashSet<tbl_eb_water_billentrydt>();
        }
    
        public int id { get; set; }
        public int Refno { get; set; }
        public Nullable<System.DateTime> refdate { get; set; }
        public string Utility_id { get; set; }
        public string utility_name { get; set; }
        public Nullable<int> Supplier_id { get; set; }
        public string Supplier_name { get; set; }
        public Nullable<int> Accyear { get; set; }
        public Nullable<System.DateTime> Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_eb_water_billentrydt> tbl_eb_water_billentrydt { get; set; }
    }
}
