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
    
    public partial class tbl_emailconfigurationmaster
    {
        public int ID { get; set; }
        public string MailServerName { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public Nullable<int> Port { get; set; }
        public Nullable<int> InSSl { get; set; }
        public string AttachmentDownloadPath { get; set; }
        public string AliasName { get; set; }
        public string OutFromId { get; set; }
        public string Mode { get; set; }
        public string OutMailServerName { get; set; }
        public string OutEmailId { get; set; }
        public string OutPassword { get; set; }
        public Nullable<int> OutPortNo { get; set; }
        public Nullable<int> OutSSl { get; set; }
        public string InMailID { get; set; }
        public string ServerTimeZone { get; set; }
        public string IncNo { get; set; }
        public Nullable<int> ServerType { get; set; }
        public Nullable<int> Accyear { get; set; }
        public Nullable<System.DateTime> Createddatetime { get; set; }
        public string Createduser { get; set; }
        public string Delmark { get; set; }
    }
}