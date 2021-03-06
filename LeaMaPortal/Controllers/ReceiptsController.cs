﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using LeaMaPortal.Models;
using System.Threading.Tasks;
using LeaMaPortal.Helpers;
using MvcPaging;

namespace LeaMaPortal.Controllers
{
    public class ReceiptsController : Controller
    {
        private LeamaEntities db = new LeamaEntities();

        // GET: Receipts
        public PartialViewResult Index()
        {
            List<SelectListItem> ReceiptType = new List<SelectListItem>
                                     ();
            ReceiptType.Add(new SelectListItem
            {
                Text = "Advance",
                Value = "1"
            });
            ReceiptType.Add(new SelectListItem
            {
                Text = "Against Invoice",
                Value = "2",
                Selected = true
            });
            ReceiptType.Add(new SelectListItem
            {
                Text = "Others",
                Value = "3"
            });

            List<SelectListItem> ReceiptMode = new List<SelectListItem>
                                    ();
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Cash",
                Value = "1"
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Cheque",
                Value = "2",
                Selected = true
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Online",
                Value = "3"
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Cash",
                Value = "4"
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Pdc",
                Value = "5",
                Selected = true
            });
            ReceiptMode.Add(new SelectListItem
            {
                Text = "Advance Ajustment",
                Value = "3"
            });
            List<SelectListItem> PDCStatus = new List<SelectListItem>
                               ();
            PDCStatus.Add(new SelectListItem
            {
                Text = "Received",
                Value = "1"
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Cleared",
                Value = "2",
                Selected = true
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Bounced",
                Value = "3"
            });
            PDCStatus.Add(new SelectListItem
            {
                Text = "Cancelled",
                Value = "4"
            });
            ViewBag.ReceiptType = ReceiptType;
            ViewBag.ReceiptMode = ReceiptMode;
            //ViewBag.PDCStatus = PDCStatus;


            //var Pdc = db.Database.SqlQuery<object>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
            //ViewBag.PDCStatus = new SelectList(Pdc);
            ViewBag.PDCStatus = new SelectList("");

            ReceiptViewModel model = new ReceiptViewModel();

            var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
            model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;

            model.ReceiptDate = DateTime.Today;
            ViewBag.Reccategory = new SelectList(Common.Reccategory);
            ViewBag.ReceiptMode = new SelectList(Common.ReceiptMode);




            return PartialView("../Receipts/Index", model);
        }

        [HttpGet]
        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            var Rec = db.tbl_receipthd.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                Rec = Rec.Where(x => x.ReceiptNo.ToString().Contains(Search));
            }
            var invoice = Rec.OrderBy(x => x.id).Select(x => new ReceiptViewModel()
            {
                AdvAcCode = x.AdvAcCode,
                agreement_no = x.agreement_no,
                AmtInWords = x.AmtInWords,
                BankAcCode = x.BankAcCode,
                BankAcName = x.BankAcName,
                DDChequeDate = x.DDChequeDate,
                DDChequeNo = x.DDChequeNo,
                Id = x.id,
                Narration = x.Narration,
                Reccategory = x.Reccategory,
                ReceiptNo = x.ReceiptNo,
                RecpType = x.RecpType,
                ReceiptDate = x.ReceiptDate,
                PDCstatus = x.PDCstatus,
                Property_id = x.Property_id,
                Property_Name = x.Property_Name,
                TotalAmount = x.TotalAmount,
                unit_Name = x.unit_Name,
                ReceiptDetailsList = db.tbl_receiptdt.Where(y => y.Delmark != "*" && y.ReceiptNo == x.ReceiptNo).Select(z => new ReceiptDetailsViewModel
                {
                    Id = z.id,
                    CreditAmt = z.CreditAmt,
                    Description = z.Description,
                    Invno = z.Invno,
                    InvoiceAmount = z.InvoiceAmount,
                    InvoiceDate = z.InvoiceDate,
                    Invtype = z.Invtype,
                    ReceivedAmount = z.ReceivedAmount,
                    Balance = z.InvoiceAmount - (z.ReceivedAmount + z.CreditAmt),
                    Remarks = z.Remarks
                }).ToList()
            }).ToPagedList(currentPageIndex, PageSize);
            return PartialView("../Receipts/_List", invoice);
        }
                
        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            var rec = await db.tbl_receipthd.FirstOrDefaultAsync(f => f.id == id);
            var data = new ReceiptViewModel();
            data.AdvAcCode = rec.AdvAcCode;
            data.agreement_no = rec.agreement_no;
            data.AmtInWords = rec.AmtInWords;
            data.BankAcCode = rec.BankAcCode;
            data.BankAcName = rec.BankAcName;
            data.DDChequeDate = rec.DDChequeDate;
            data.DDChequeNo = rec.DDChequeNo;
            data.Id = rec.id;
            data.Narration = rec.Narration;
            data.PDCstatus = rec.PDCstatus;
            data.Property_id = rec.Property_id;
            data.Property_Name = rec.Property_Name;
            data.Reccategory = rec.Reccategory;
            data.ReceiptDate = rec.ReceiptDate;
            data.ReceiptNo = rec.ReceiptNo;
            data.RecpType = rec.RecpType;
            data.ReceiptDetailsList = db.tbl_receiptdt.Where(w => w.ReceiptNo == rec.ReceiptNo)
                .Select(s => new ReceiptDetailsViewModel
                {
                    CreditAmt = s.CreditAmt,
                    Balance = s.InvoiceAmount - (s.ReceivedAmount + s.CreditAmt),
                    Description = s.Description,
                    Id = s.id,
                    Invno = s.Invno,
                    InvoiceAmount = s.InvoiceAmount,
                    InvoiceDate = s.InvoiceDate,
                    Invtype = s.Invtype,
                    ReceivedAmount = s.ReceivedAmount,
                    Remarks = s.Remarks
                }).ToList();
            return PartialView("../Receipts/_AddorUpdate", data);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int? id)
        {
            MessageResult result = new MessageResult();
            var rec = await db.tbl_receipthd.FirstOrDefaultAsync(f => f.id == id);
            var data = new ReceiptViewModel();
            data.AdvAcCode = rec.AdvAcCode;
            data.agreement_no = rec.agreement_no;
            data.AmtInWords = rec.AmtInWords;
            data.BankAcCode = rec.BankAcCode;
            data.BankAcName = rec.BankAcName;
            data.DDChequeDate = rec.DDChequeDate;
            data.DDChequeNo = rec.DDChequeNo;
            data.Id = rec.id;
            data.Narration = rec.Narration;
            data.PDCstatus = rec.PDCstatus;
            data.Property_id = rec.Property_id;
            data.Property_Name = rec.Property_Name;
            data.Reccategory = rec.Reccategory;
            data.ReceiptDate = rec.ReceiptDate;
            data.ReceiptNo = rec.ReceiptNo;
            data.RecpType = rec.RecpType;
            data.ReceiptDetailsList = db.tbl_receiptdt.Where(w => w.ReceiptNo == rec.ReceiptNo)
                .Select(s => new ReceiptDetailsViewModel
                {
                    CreditAmt = s.CreditAmt,
                    Balance = s.InvoiceAmount - (s.ReceivedAmount + s.CreditAmt),
                    Description = s.Description,
                    Id = s.id,
                    Invno = s.Invno,
                    InvoiceAmount = s.InvoiceAmount,
                    InvoiceDate = s.InvoiceDate,
                    Invtype = s.Invtype,
                    ReceivedAmount = s.ReceivedAmount,
                    Remarks = s.Remarks
                }).ToList();
            object[] param = Helper.GetMySqlParameters<ReceiptViewModel>(data, "Delete", System.Web.HttpContext.Current.User.Identity.Name);
            var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Receipt_All(@PFlag,
, @PReccategory 
, @PRecpType 
, @PReceiptNo
, @PReceiptDate 
, @Pagreement_no
, @PProperty_id 
, @PProperty_Name
, @PUnit_ID 
, @Punit_Name 
, @PTenant_id 
, @PTenant_Name 
, @PTotalAmount 
, @PAmtInWords 
, @PDDChequeNo 
, @PPdcstatus 
, @PBankAcCode
, @PBankAcName
, @PAdvAcCode 
, @PDDChequeDate 
, @PNarration 
, @PCreateduser 
, @PReceiptdt )", param).ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }        

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            ReceiptViewModel model = new ReceiptViewModel();

            var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
            model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;

            model.ReceiptDate = DateTime.Today;
            ViewBag.Reccategory = new SelectList(Common.Reccategory);
            ViewBag.RecpType = new SelectList(Common.ReceiptMode);
            ViewBag.agreement_no = new SelectList(db.tbl_agreement.Where(x => x.Delmark != "*").OrderBy(x => x.Agreement_No).Select(x => x.Agreement_No));
            var advancepaymentnumber = db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment");
            ViewBag.AdvAcCode = new SelectList(advancepaymentnumber);
            var properties = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Property").OrderBy(x => x.Property_Id).Select(x => new { PropertyId = x.Property_Id, PropertyName = x.Property_Name, GroupedValue = x.Property_Id + ":" + x.Property_Name, PropertyIdTawtheeq = x.Property_ID_Tawtheeq });
            ViewBag.Property_id = new SelectList(properties, "PropertyIdTawtheeq", "PropertyIdTawtheeq");
            ViewBag.Property_Name = new SelectList(properties, "PropertyIdTawtheeq", "PropertyName");

            var units = db.tbl_propertiesmaster.Where(x => x.Delmark != "*" && x.Property_Flag == "Unit").OrderBy(x => x.Unit_ID_Tawtheeq).Select(x => new { UnitId = x.Unit_ID_Tawtheeq, UnitName = x.Unit_Property_Name, GroupedValue = x.Unit_ID_Tawtheeq + ":" + x.Unit_Property_Name });
            ViewBag.Unit_ID = new SelectList(units, "UnitId", "UnitId");
            ViewBag.unit_Name = new SelectList(units, "UnitId", "UnitName");

            var tenant = db.view_tenant.Select(x => new { TenantId = x.Tenant_id, TenantName = x.First_Name, Type = x.type, GroupedValue = x.Tenant_id + ":" + x.First_Name + ":" + x.type });
            ViewBag.Tenant_id = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantId", selectedValue: null);
            ViewBag.Tenant_Name = new SelectList(items: tenant, dataValueField: "TenantId", dataTextField: "TenantName", selectedValue: null);
            ViewBag.BankAcCode = new SelectList(Common.BankAccountNumber);
            ViewBag.BankAcName = new SelectList(Common.BankAccountName);
            ViewBag.PDCstatus = new SelectList(Common.Receipts_PDCStatus);
            ViewBag.InvoiceNumber = new SelectList(db.view_invoice_receipt_pending.OrderBy(o => o.invno).Distinct(), "invno", "invno");
            return PartialView("../Receipts/_AddorUpdate", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(ReceiptViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {

                string PFlag = Common.UPDATE;
                if (model.ReceiptNo == 0)
                {
                    PFlag = Common.INSERT;
                    var receipts = db.tbl_receipthd.OrderByDescending(x => x.id).FirstOrDefault();
                    model.ReceiptNo = receipts != null ? receipts.id + 1 : 1;
                }
                object[] param = Helper.GetMySqlParameters<ReceiptViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);
                var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Receipt_All(@PFlag,
, @PReccategory 
, @PRecpType 
, @PReceiptNo
, @PReceiptDate 
, @Pagreement_no
, @PProperty_id 
, @PProperty_Name
, @PUnit_ID 
, @Punit_Name 
, @PTenant_id 
, @PTenant_Name 
, @PTotalAmount 
, @PAmtInWords 
, @PDDChequeNo 
, @PPdcstatus 
, @PBankAcCode
, @PBankAcName
, @PAdvAcCode 
, @PDDChequeDate 
, @PNarration 
, @PCreateduser 
, @PReceiptdt )", param).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetReceiptNumber()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from view_advance_pending").ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementNoByPdc()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from view_pdc_pending").ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            //list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            list.pdc_Amount = new SelectList(result.Select(r => r.pdc_Amount));
            list.DDChequeNo = new SelectList(result.Select(r => r.DDChequeNo));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementNoByInvoice()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from view_invoice_receipt_pending order by incno").ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            //list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            //list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            list.totalamt = new SelectList(result.Select(r => r.totalamt));
            list.month = new SelectList(result.Select(r => r.month));
            list.year = new SelectList(result.Select(r => r.year));
            list.date = new SelectList(result.Select(r => r.date));
            list.incno = new SelectList(result.Select(r => r.incno));
            list.invtype = new SelectList(result.Select(r => r.invtype));
            list.InvoiceAmount = new SelectList(result.Select(r => r.InvoiceAmount));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementNoBySecurity()
        {
            List<ReceiptsDDLChangeViewModel> result = await db.Database.SqlQuery<ReceiptsDDLChangeViewModel>("select * from tbl_agreement where ifnull(Advance_Security_Amount,0)>0 and Agreement_No=1  and ifnull(status,'')='' and ifnull(delmark,'')<>'*';").ToListAsync();
            AdvancePendingSelectList list = new AdvancePendingSelectList();
            list.ReceiptNo = new SelectList(result.Select(r => r.ReceiptNo));
            list.Agreement_No = new SelectList(result.Select(r => r.agreement_no));
            list.Property_Id = new SelectList(result.Select(r => r.Property_id));
            list.Property_Name = new SelectList(result.Select(r => r.Property_Name));
            list.Tenant_Id = new SelectList(result.Select(r => r.Tenant_id));
            list.Tenant_Name = new SelectList(result.Select(r => r.Tenant_Name));
            list.Unit_Id = new SelectList(result.Select(r => r.Unit_ID));
            list.Unit_Name = new SelectList(result.Select(r => r.unit_Name));
            list.TotalAmount = new SelectList(result.Select(r => r.TotalAmount));
            return Json(list, JsonRequestBehavior.AllowGet);
            //return View();
        }
        [HttpGet]
        public async Task<string> AmountInWords(decimal amount)
        {
            string amountInWords = NumberToText.Convert(amount);
            //string amountInWords = amount.Humanize();
            return amountInWords;
        }
        [HttpPost]
        public async Task<JsonResult> GetInvoiceDetails(string InvoiceNumber)
        {
            try
            {
                var invoice = await db.view_invoice_receipt_pending.FirstOrDefaultAsync(f => f.invno == InvoiceNumber);
                return Json(new { invtype = invoice.invtype, day = invoice.date.Value.Date, month = invoice.date.Value.Month, year = invoice.date.Value.Year, InvoiceAmount = invoice.InvoiceAmount });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
