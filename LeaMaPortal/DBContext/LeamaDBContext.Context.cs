﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LeamaEntities : DbContext
    {
        public LeamaEntities()
            : base("name=LeamaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<dashboard_rental> dashboard_rental { get; set; }
        public virtual DbSet<dashboard_utility> dashboard_utility { get; set; }
        public virtual DbSet<dashboard_vacancy> dashboard_vacancy { get; set; }
        public virtual DbSet<ebwater_report> ebwater_report { get; set; }
        public virtual DbSet<email_output> email_output { get; set; }
        public virtual DbSet<eventtest> eventtests { get; set; }
        public virtual DbSet<outstanding_report> outstanding_report { get; set; }
        public virtual DbSet<pdc_report> pdc_report { get; set; }
        public virtual DbSet<renewal_report> renewal_report { get; set; }
        public virtual DbSet<summarycollection_report> summarycollection_report { get; set; }
        public virtual DbSet<summaryebwater_report> summaryebwater_report { get; set; }
        public virtual DbSet<tbl_aging_range> tbl_aging_range { get; set; }
        public virtual DbSet<tbl_agreement> tbl_agreement { get; set; }
        public virtual DbSet<tbl_agreement_checklist> tbl_agreement_checklist { get; set; }
        public virtual DbSet<tbl_agreement_closure> tbl_agreement_closure { get; set; }
        public virtual DbSet<tbl_agreement_closure_checklist> tbl_agreement_closure_checklist { get; set; }
        public virtual DbSet<tbl_agreement_closure_facility> tbl_agreement_closure_facility { get; set; }
        public virtual DbSet<tbl_agreement_closure_pdc> tbl_agreement_closure_pdc { get; set; }
        public virtual DbSet<tbl_agreement_closure_utility> tbl_agreement_closure_utility { get; set; }
        public virtual DbSet<tbl_agreement_doc> tbl_agreement_doc { get; set; }
        public virtual DbSet<tbl_agreement_facility> tbl_agreement_facility { get; set; }
        public virtual DbSet<tbl_agreement_pdc> tbl_agreement_pdc { get; set; }
        public virtual DbSet<tbl_agreement_status> tbl_agreement_status { get; set; }
        public virtual DbSet<tbl_agreement_unit_inner> tbl_agreement_unit_inner { get; set; }
        public virtual DbSet<tbl_agreement_utility> tbl_agreement_utility { get; set; }
        public virtual DbSet<tbl_approvalconfig> tbl_approvalconfig { get; set; }
        public virtual DbSet<tbl_caretaker> tbl_caretaker { get; set; }
        public virtual DbSet<tbl_checklistmaster> tbl_checklistmaster { get; set; }
        public virtual DbSet<tbl_combo_master> tbl_combo_master { get; set; }
        public virtual DbSet<tbl_country> tbl_country { get; set; }
        public virtual DbSet<tbl_eb_water_billentrydt> tbl_eb_water_billentrydt { get; set; }
        public virtual DbSet<tbl_eb_water_billentryhd> tbl_eb_water_billentryhd { get; set; }
        public virtual DbSet<tbl_eb_water_paymentdt> tbl_eb_water_paymentdt { get; set; }
        public virtual DbSet<tbl_eb_water_paymenthd> tbl_eb_water_paymenthd { get; set; }
        public virtual DbSet<tbl_emailconfigurationmaster> tbl_emailconfigurationmaster { get; set; }
        public virtual DbSet<tbl_emailtemplate> tbl_emailtemplate { get; set; }
        public virtual DbSet<tbl_errorlog> tbl_errorlog { get; set; }
        public virtual DbSet<tbl_facilitymaster> tbl_facilitymaster { get; set; }
        public virtual DbSet<tbl_formmaster> tbl_formmaster { get; set; }
        public virtual DbSet<tbl_invoicedt> tbl_invoicedt { get; set; }
        public virtual DbSet<tbl_invoicehd> tbl_invoicehd { get; set; }
        public virtual DbSet<tbl_log> tbl_log { get; set; }
        public virtual DbSet<tbl_metermaster> tbl_metermaster { get; set; }
        public virtual DbSet<tbl_paymentdt> tbl_paymentdt { get; set; }
        public virtual DbSet<tbl_paymenthd> tbl_paymenthd { get; set; }
        public virtual DbSet<tbl_propertiesdt> tbl_propertiesdt { get; set; }
        public virtual DbSet<tbl_propertiesdt1> tbl_propertiesdt1 { get; set; }
        public virtual DbSet<tbl_propertiesmaster> tbl_propertiesmaster { get; set; }
        public virtual DbSet<tbl_propertytypemaster> tbl_propertytypemaster { get; set; }
        public virtual DbSet<tbl_receiptdt> tbl_receiptdt { get; set; }
        public virtual DbSet<tbl_receipthd> tbl_receipthd { get; set; }
        public virtual DbSet<tbl_region> tbl_region { get; set; }
        public virtual DbSet<tbl_slabmaster> tbl_slabmaster { get; set; }
        public virtual DbSet<tbl_supplierdt> tbl_supplierdt { get; set; }
        public virtual DbSet<tbl_supplierdt1> tbl_supplierdt1 { get; set; }
        public virtual DbSet<tbl_suppliermaster> tbl_suppliermaster { get; set; }
        public virtual DbSet<tbl_tenant_company> tbl_tenant_company { get; set; }
        public virtual DbSet<tbl_tenant_company_government_doc> tbl_tenant_company_government_doc { get; set; }
        public virtual DbSet<tbl_tenant_companydt> tbl_tenant_companydt { get; set; }
        public virtual DbSet<tbl_tenant_companydt1> tbl_tenant_companydt1 { get; set; }
        public virtual DbSet<tbl_tenant_individual> tbl_tenant_individual { get; set; }
        public virtual DbSet<tbl_tenant_individual_doc> tbl_tenant_individual_doc { get; set; }
        public virtual DbSet<tbl_userrights> tbl_userrights { get; set; }
        public virtual DbSet<tbl_utilitiesmaster> tbl_utilitiesmaster { get; set; }
        public virtual DbSet<vacancy_report> vacancy_report { get; set; }
        public virtual DbSet<collection_summary> collection_summary { get; set; }
        public virtual DbSet<test_report1> test_report1 { get; set; }
        public virtual DbSet<test_report2> test_report2 { get; set; }
        public virtual DbSet<vacancy_caretaker_report> vacancy_caretaker_report { get; set; }
        public virtual DbSet<view_auto_receipt> view_auto_receipt { get; set; }
        public virtual DbSet<view_find_pdcstatus> view_find_pdcstatus { get; set; }
        public virtual DbSet<view_invoice_agreement> view_invoice_agreement { get; set; }
        public virtual DbSet<view_invoice_receipt_pending> view_invoice_receipt_pending { get; set; }
        public virtual DbSet<view_tenant> view_tenant { get; set; }
        public virtual DbSet<dashboard_earning> dashboard_earning { get; set; }
        public virtual DbSet<dashboard_expenses> dashboard_expenses { get; set; }
    
        public virtual ObjectResult<string> Usp_split(string screen_name, string combo_name, string deli, string target)
        {
            var screen_nameParameter = screen_name != null ?
                new ObjectParameter("Screen_name", screen_name) :
                new ObjectParameter("Screen_name", typeof(string));
    
            var combo_nameParameter = combo_name != null ?
                new ObjectParameter("combo_name", combo_name) :
                new ObjectParameter("combo_name", typeof(string));
    
            var deliParameter = deli != null ?
                new ObjectParameter("deli", deli) :
                new ObjectParameter("deli", typeof(string));
    
            var targetParameter = target != null ?
                new ObjectParameter("target", target) :
                new ObjectParameter("target", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Usp_split", screen_nameParameter, combo_nameParameter, deliParameter, targetParameter);
        }
    }
}
