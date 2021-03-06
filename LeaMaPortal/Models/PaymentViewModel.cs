﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [DisplayName("Payment Number")]
        public int PaymentNo { get; set; }
        [DisplayName("Payment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PaymentDate { get; set; }
        [DisplayName("PaymentType")]
        public string PaymentType { get; set; }
        [DisplayName("Payment Mode")]
        public string PaymentMode { get; set; }
        [DisplayName("Supplier ID")]
        public int? Supplier_id { get; set; }
        [DisplayName("Supplier Name")]
        public string Supplier_Name { get; set; }
        [DisplayName("Agreeement Number")]
        public int? agreement_no { get; set; }
        [DisplayName("Property ID")]
        public string Property_id { get; set; }
        [DisplayName("Property Name")]
        public string Property_Name { get; set; }
        [DisplayName("Unit ID")]
        public string Unit_ID { get; set; }
        [DisplayName("Unit Name")]
        public string unit_Name { get; set; }
        [DisplayName("Total Amount")]
        public float? TotalAmount { get; set; }
        [DisplayName("Amount in words")]
        public string AmtInWords { get; set; }
        [DisplayName("PDC Status")]
        public string PDCstatus { get; set; }
        [DisplayName("DD/Cheque Number")]
        public string DDChequeNo { get; set; }
        [DisplayName("DD/Cheque Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DDChequeDate { get; set; }
        [DisplayName("Bank Account Number")]
        public string BankAcCode { get; set; }
        [DisplayName("Bank Account Name")]
        public string BankAcName { get; set; }
        [DisplayName("Narration")]
        public string Narration { get; set; }
        [DisplayName("Advance Payment Number")]
        public string AdvAcCode { get; set; }
        public List<PaymentDetailsViewModel> PaymentDetailsViewModel { get; set; }

    }
    public class PaymentDetailsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Invoice Type")]
        public string Invtype { get; set; }
        [DisplayName("Invoice Number")]
        public string Invno { get; set; }
        [DisplayName("Invoice Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InvoiceDate { get; set; }
        [DisplayName("Invoice Description")]
        public string Description { get; set; }
        [DisplayName("Invoice Amount")]
        public float? InvoiceAmount { get; set; }
        [DisplayName("Debit Amount")]
        public float? DebitAmount { get; set; }
        [DisplayName("Paid Amount")]
        public float? PaidAmount { get; set; }
        [DisplayName("Balance Amount")]
        public float? Balance { get; set; }
        [DisplayName("Remarks")]
        public string Remarks { get; set; }


    }

}