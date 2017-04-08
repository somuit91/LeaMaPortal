﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal
{
    public static class Common
    {
        public static List<string> Title = new List<string>() {"MR.", "MRS.","MS." };
        public static List<string> Role = new List<string>() { "Admin", "Management", "Caretaker" };
        public const string DefaultTitle= "Mr.";
        //public static List<string> City = new List<string>() { "Abudhabi", "Sharja" };
        public static List<string> Profession = new List<string>() { "Engineer", "Teacher", "Shop keeper","Doctor","Farmer" };

        public const string INSERT = "INSERT", UPDATE = "UPDATE", DELETE = "DELETE", SELECT= "SELECT",View = "View";
        public const int DefaultMaster = 9;
        public const string TenantIndividualDocumentContainer = "Documents/TenantIndividual/";
        public static List<FormMaster> FormMasterList = new List<FormMaster>()
        {
           //new FormMaster() {Id=1,FormName="" },
           new FormMaster() {Id=2,MenuName="User Rights" },
           //new FormMaster() {Id=3,FormName="" },
           new FormMaster() {Id=4,MenuName="Approval" },
           new FormMaster() {Id=5,MenuName="Country Master" },
           new FormMaster() {Id=6,MenuName="Region Master" },
           new FormMaster() {Id=7,MenuName="Caretaker Master" },
           new FormMaster() {Id=8,MenuName="Property Type Master" },
           new FormMaster() {Id=9,MenuName="Property Master" },
           new FormMaster() {Id=10,MenuName="Tenant Master - Company" },
           new FormMaster() {Id=11,MenuName="Tenant Master - Individual" },
           new FormMaster() {Id=12,MenuName="Checklist Master" },
           new FormMaster() {Id=13,MenuName="Facility Master" },
           new FormMaster() {Id=14,MenuName="Utility Master" },
           new FormMaster() {Id=15,MenuName="Supplier Master" },
           new FormMaster() {Id=16,MenuName="Slab Master" },
           new FormMaster() {Id=17,MenuName="Meter Master" },
           new FormMaster() {Id=18,MenuName="Email Template" },
           //new FormMaster() {Id=19,FormName="" },
           //new FormMaster() {Id=20,FormName="" }

        };
        //tenant company
        public static List<string> TenantType = new List<string>() { "Government", "Person", "Company" };
        public static List<string> Emirate = new List<string>() { "Default" };
        public static List<string> ComapanyActivity = new List<string>() { "Activity1" };
        public static List<string> Issuance_authority = new List<string>() { "List-1" };
        public const string TenantCompanyDocumentContainer = "Documents/TenantCompany/";
        public static List<string> Nationality = new List<string>() { "UAE", "Non-UAE"};
        public static string DefaultNationality="UAE";
        public static string DefaultMaridalStatus = "Family";
    }
    public class FormMaster
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
    }
}