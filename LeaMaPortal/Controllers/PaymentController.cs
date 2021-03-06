﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.DBContext;
using System.Threading.Tasks;
using LeaMaPortal.Models;
using MySql.Data.MySqlClient;
using MvcPaging;
using LeaMaPortal.Helpers;

namespace LeaMaPortal.Controllers
{
    public class PaymentController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: Payments
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult List(string Search, int? page, int? defaultPageSize)
        {
            ViewData["Search"] = Search;
            int currentPageIndex = page.HasValue ? page.Value : 1;
            int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
            ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
            var PaymentDetails = db.tbl_paymenthd.Where(x => x.Delmark != "*");
            if (!string.IsNullOrWhiteSpace(Search))
            {
                PaymentDetails = PaymentDetails.Where(x => x.PaymentNo.ToString().Contains(Search));
            }
            var invoice = PaymentDetails.OrderBy(x => x.id).Select(x => new PaymentViewModel()
            {
                AdvAcCode = x.AdvAcCode,
                agreement_no = x.agreement_no,
                AmtInWords = x.AmtInWords,
                BankAcCode = x.BankAcCode,
                BankAcName = x.BankAcName,
                DDChequeDate = x.Cheqdate,
                DDChequeNo = x.DDChequeNo,
                Id = x.id,
                Narration = x.Narration,
                PaymentDate = x.PaymentDate,
                PaymentMode = x.PaymentMode,
                PaymentNo = x.PaymentNo,
                PaymentType = x.PaymentType,
                PDCstatus = x.pdcstatus,
                Property_id = x.Property_ID,
                Property_Name = x.Property_Name,
                Supplier_id = x.Supplier_id,
                Supplier_Name = x.Supplier_Name,
                TotalAmount = x.TotalAmount,
                Unit_ID = x.Unit_ID,
                unit_Name = x.unit_Name,
                PaymentDetailsViewModel = db.tbl_paymentdt.Where(y => y.Delmark != "*" && y.PaymentNo == x.PaymentNo).Select(z => new PaymentDetailsViewModel
                {
                    Id = z.id,
                    DebitAmount = z.Debitamt,
                    Description = z.Description,
                    Invno = z.InvoiceNo,
                    InvoiceAmount = z.InvoiceAmount,
                    InvoiceDate = z.InvoiceDate,
                    Invtype = z.Invtype,
                    PaidAmount = z.PaidAmount,
                    Remarks = z.Remarks
                }).ToList()
            }).ToPagedList(currentPageIndex, PageSize);
            return PartialView("../Payment/_List", invoice);
        }

        [HttpGet]
        public async Task<PartialViewResult> AddOrUpdate()
        {
            try
            {
                PaymentViewModel model = new PaymentViewModel();
                int paymentno = await db.tbl_paymenthd.Select(x => x.PaymentNo).DefaultIfEmpty(0).MaxAsync();
                model.PaymentNo = paymentno == 0 ? 1 : paymentno + 1;
                model.PaymentDate = DateTime.Now.Date;
                var paymentType_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','Reccategory',',',null)").ToList();
                ViewBag.PaymentType = new SelectList(paymentType_result, model.PaymentType);
                var paymentMode_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','RecpType',',',null)").ToList();
                ViewBag.PaymentMode = new SelectList(paymentMode_result, model.PaymentMode);
                var PDCstatus_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
                ViewBag.PDCstatus = new SelectList(PDCstatus_result, model.PDCstatus);
                var suppliers = await db.tbl_suppliermaster.Where(w => w.Delmark != "*").ToListAsync();
                ViewBag.Supplierid = new SelectList(suppliers, "Supplier_Id", "Supplier_Id");
                ViewBag.SupplierName = new SelectList(suppliers, "Supplier_Id", "Supplier_Name");

                var properties = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*").OrderBy(o => o.id);

                ViewBag.Propertyid = new SelectList(properties, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq");
                ViewBag.PropertyName = new SelectList(properties, "Property_ID_Tawtheeq", "Property_Name");

                var units = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Delmark != "*").OrderBy(o => o.id);

                ViewBag.UnitID = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq");
                ViewBag.unitName = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_Property_Name");

                
                ViewBag.agreement_no = new SelectList(db.tbl_agreement.Where(w => w.Delmark != "*").OrderBy(o => o.id).Distinct(), "Agreement_No", "Agreement_No");
                ViewBag.BankAcName = new SelectList(StaticHelper.GetStaticData(StaticHelper.ACCOUNT_NAME), "Name", "Name");
                ViewBag.BankAcCode = new SelectList(StaticHelper.GetStaticData(StaticHelper.ACCOUNT_NUMBER), "Name", "Name");

                var advancepaymentnumber = db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment");
                ViewBag.AdvAcCode = new SelectList(advancepaymentnumber);
                return PartialView("../Payment/_AddOrUpdate", model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(PaymentViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";

                    if (model.Id != 0)
                    {
                        PFlag = "UPDATE";
                    }
                    string invoice = null;
                    if (model.PaymentDetailsViewModel != null && model.PaymentType == "against invoice")
                    {
                        foreach (var item in model.PaymentDetailsViewModel)
                        {
                            if (string.IsNullOrWhiteSpace(invoice))
                            {
                                invoice = "(" + model.PaymentNo + ",'" + item.Invtype + "','" + item.Description +
                                    "','" + item.Invno + "','" + item.InvoiceDate + "','" + item.InvoiceAmount + "','" + item.PaidAmount + "','" + item.DebitAmount
                                    + "','" + item.Remarks + "')";
                            }
                            else
                            {
                                invoice += ",(" + model.PaymentNo + ",'" + item.Invtype + "','" + item.Description +
                                    "','" + item.Invno + "','" + item.InvoiceDate + "','" + item.InvoiceAmount + "','" + item.PaidAmount + "','" + item.DebitAmount
                                    + "','" + item.Remarks + "')";
                            }
                        }
                    }
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PPaymentNo", model.PaymentNo),
                                           new MySqlParameter("@PPaymentDate",model.PaymentDate),
                                            new MySqlParameter("@Pagreement_no",model.agreement_no),
                                            new MySqlParameter("@PProperty_ID", model.Property_id),
                                            new MySqlParameter("@PProperty_Name", model.Property_Name),
                                            new MySqlParameter("@PUnit_ID", model.Unit_ID),
                                            new MySqlParameter("@Punit_Name", model.unit_Name),
                                            new MySqlParameter("@PSupplier_id", model.Supplier_id),
                                            new MySqlParameter("@PSupplier_Name", model.Supplier_Name),
                                            new MySqlParameter("@PPaymentType", model.PaymentType),
                                            new MySqlParameter("@PPaymentMode", model.PaymentMode),
                                            new MySqlParameter("@PTotalAmount", model.TotalAmount),
                                            new MySqlParameter("@PAmtInWords", model.AmtInWords),
                                            new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                                            new MySqlParameter("@PCheqdate", model.DDChequeDate),
                                            new MySqlParameter("@Ppdcstatus", model.PDCstatus),
                                            new MySqlParameter("@PBankAcCode", model.BankAcCode),
                                            new MySqlParameter("@PBankAcName", model.BankAcName),
                                            new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                                            new MySqlParameter("@PNarration", model.Narration),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name),
                                           new MySqlParameter("@PPaymentdt", invoice)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Payment_All(@PFlag,@PPaymentNo,@PPaymentDate,@Pagreement_no,@PProperty_ID,@PProperty_Name,@PUnit_ID,@Punit_Name,@PSupplier_id,@PSupplier_Name,@PPaymentType,@PPaymentMode,@PTotalAmount,@PAmtInWords,@PDDChequeNo,@PCheqdate,@Ppdcstatus,@PBankAcCode,@PBankAcName,@PAdvAcCode,@PNarration,@PCreateduser,@PPaymentdt)", param).ToListAsync();
                    await db.SaveChangesAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> Edit(int Payment_No)
        {
            try
            {
                var x = await db.tbl_paymenthd.FirstOrDefaultAsync(f => f.PaymentNo == Payment_No);
                PaymentViewModel model = new PaymentViewModel()
                {
                    AdvAcCode = x.AdvAcCode,
                    agreement_no = x.agreement_no,
                    AmtInWords = x.AmtInWords,
                    BankAcCode = x.BankAcCode,
                    BankAcName = x.BankAcName,
                    DDChequeDate = x.Cheqdate,
                    DDChequeNo = x.DDChequeNo,
                    Id = x.id,
                    Narration = x.Narration,
                    PaymentDate = x.PaymentDate,
                    PaymentMode = x.PaymentMode,
                    PaymentNo = x.PaymentNo,
                    PaymentType = x.PaymentType,
                    PDCstatus = x.pdcstatus,
                    Property_id = x.Property_ID,
                    Property_Name = x.Property_Name,
                    Supplier_id = x.Supplier_id,
                    Supplier_Name = x.Supplier_Name,
                    TotalAmount = x.TotalAmount,
                    Unit_ID = x.Unit_ID,
                    unit_Name = x.unit_Name,
                    PaymentDetailsViewModel = db.tbl_paymentdt.Where(y => y.Delmark != "*" && y.PaymentNo == x.PaymentNo).Select(z => new PaymentDetailsViewModel
                    {
                        Id = z.id,
                        DebitAmount = z.Debitamt,
                        Description = z.Description,
                        Invno = z.InvoiceNo,
                        InvoiceAmount = z.InvoiceAmount,
                        InvoiceDate = z.InvoiceDate,
                        Invtype = z.Invtype,
                        PaidAmount = z.PaidAmount,
                        Remarks = z.Remarks
                    }).ToList()
                };

                var paymentType_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','Reccategory',',',null)").ToList();
                ViewBag.PaymentType = new SelectList(paymentType_result, model.PaymentType);
                var paymentMode_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','RecpType',',',null)").ToList();
                ViewBag.PaymentMode = new SelectList(paymentMode_result, model.PaymentMode);
                var PDCstatus_result = db.Database.SqlQuery<string>(@"call usp_split('Receipts','PDCstatus',',',null)").ToList();
                ViewBag.PDCstatus = new SelectList(PDCstatus_result, model.PDCstatus);
                var suppliers = await db.tbl_suppliermaster.Where(w => w.Delmark != "*").ToListAsync();
                ViewBag.Supplierid = new SelectList(suppliers, "Supplier_Id", "Supplier_Id",model.Supplier_id);
                ViewBag.SupplierName = new SelectList(suppliers, "Supplier_Id", "Supplier_Name",model.Supplier_id);
                var agreements = db.tbl_agreement.Where(w => w.Delmark != "*").OrderBy(o => o.id);
                ViewBag.agreement_no = new SelectList(agreements, "Agreement_No", "Agreement_No", model.agreement_no);
                ViewBag.BankAcName = new SelectList(StaticHelper.GetStaticData(StaticHelper.ACCOUNT_NAME), "Name", "Name", model.BankAcName);
                ViewBag.BankAcCode = new SelectList(StaticHelper.GetStaticData(StaticHelper.ACCOUNT_NUMBER), "Name", "Name", model.BankAcCode);
                if (model.agreement_no==null || model.agreement_no == 0)
                {
                    var properties = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Property" && w.Delmark != "*").OrderBy(o => o.id);

                    ViewBag.Propertyid = new SelectList(properties, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq", model.Property_id);
                    ViewBag.PropertyName = new SelectList(properties, "Property_ID_Tawtheeq", "Property_Name", model.Property_id);

                    var units = db.tbl_propertiesmaster.Where(w => w.Property_Flag == "Unit" && w.Delmark != "*").OrderBy(o => o.id);

                    ViewBag.UnitID = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq", model.Unit_ID);
                    ViewBag.unitName = new SelectList(units, "Unit_ID_Tawtheeq", "Unit_Property_Name", model.Unit_ID);
                }
                else
                {
                    var agreementDetails = await agreements.FirstOrDefaultAsync(y => y.Agreement_No == model.agreement_no);
                    List<UnitDropdown> unitsDropdown = new List<UnitDropdown>();
                    unitsDropdown.Add(new UnitDropdown()
                    {
                        Unitid = agreementDetails.Unit_ID_Tawtheeq,
                        unitName = agreementDetails.Unit_Property_Name
                    });
                    ViewBag.UnitID = new SelectList(unitsDropdown, "Unitid", "Unitid", model.Unit_ID);
                    ViewBag.unitName = new SelectList(unitsDropdown, "Unitid", "unitName", model.Unit_ID);

                    List<PropertyDropdown> propertyDropdown = new List<PropertyDropdown>();
                    propertyDropdown.Add(new PropertyDropdown()
                    {
                        Propertyid = agreementDetails.Property_ID_Tawtheeq,
                        PropertyName = agreementDetails.Properties_Name
                    });
                    ViewBag.Propertyid = new SelectList(propertyDropdown, "Propertyid", "Propertyid", model.Property_id);
                    ViewBag.PropertyName = new SelectList(propertyDropdown, "Propertyid", "PropertyName", model.Property_id);
                }
                var advancepaymentnumber = await db.Database.SqlQuery<int>("Select PaymentNo from view_advance_pending_payment").ToListAsync();
                ViewBag.AdvAcCode = new SelectList(advancepaymentnumber,model.AdvAcCode);
                return PartialView("../Payment/_AddorUpdate", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Payment_No)
        {
            try
            {
                MessageResult result = new MessageResult();
                var x = await db.tbl_paymenthd.FirstOrDefaultAsync(f => f.PaymentNo == Payment_No);
                PaymentViewModel model = new PaymentViewModel()
                {
                    AdvAcCode = x.AdvAcCode,
                    agreement_no = x.agreement_no,
                    AmtInWords = x.AmtInWords,
                    BankAcCode = x.BankAcCode,
                    BankAcName = x.BankAcName,
                    DDChequeDate = x.Cheqdate,
                    DDChequeNo = x.DDChequeNo,
                    Id = x.id,
                    Narration = x.Narration,
                    PaymentDate = x.PaymentDate,
                    PaymentMode = x.PaymentMode,
                    PaymentNo = x.PaymentNo,
                    PaymentType = x.PaymentType,
                    PDCstatus = x.pdcstatus,
                    Property_id = x.Property_ID,
                    Property_Name = x.Property_Name,
                    Supplier_id = x.Supplier_id,
                    Supplier_Name = x.Supplier_Name,
                    TotalAmount = x.TotalAmount,
                    Unit_ID = x.Unit_ID,
                    unit_Name = x.unit_Name,
                    PaymentDetailsViewModel = db.tbl_paymentdt.Where(y => y.Delmark != "*" && y.PaymentNo == x.PaymentNo).Select(z => new PaymentDetailsViewModel
                    {
                        Id = z.id,
                        DebitAmount = z.Debitamt,
                        Description = z.Description,
                        Invno = z.InvoiceNo,
                        InvoiceAmount = z.InvoiceAmount,
                        InvoiceDate = z.InvoiceDate,
                        Invtype = z.Invtype,
                        PaidAmount = z.PaidAmount,
                        Remarks = z.Remarks
                    }).ToList()
                };
                object[] param = {
                         new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PPaymentNo", model.PaymentNo),
                                           new MySqlParameter("@PPaymentDate",model.PaymentDate),
                                            new MySqlParameter("@Pagreement_no",model.agreement_no),
                                            new MySqlParameter("@PProperty_ID", model.Property_id),
                                            new MySqlParameter("@PProperty_Name", model.Property_Name),
                                            new MySqlParameter("@PUnit_ID", model.Unit_ID),
                                            new MySqlParameter("@Punit_Name", model.unit_Name),
                                            new MySqlParameter("@PSupplier_id", model.Supplier_id),
                                            new MySqlParameter("@PSupplier_Name", model.Supplier_Name),
                                            new MySqlParameter("@PPaymentType", model.PaymentType),
                                            new MySqlParameter("@PPaymentMode", model.PaymentMode),
                                            new MySqlParameter("@PTotalAmount", model.TotalAmount),
                                            new MySqlParameter("@PAmtInWords", model.AmtInWords),
                                            new MySqlParameter("@PDDChequeNo", model.DDChequeNo),
                                            new MySqlParameter("@PCheqdate", model.DDChequeDate),
                                            new MySqlParameter("@Ppdcstatus", model.PDCstatus),
                                            new MySqlParameter("@PBankAcCode", model.BankAcCode),
                                            new MySqlParameter("@PBankAcName", model.BankAcName),
                                            new MySqlParameter("@PAdvAcCode", model.AdvAcCode),
                                            new MySqlParameter("@PNarration", model.Narration),
                                            new MySqlParameter("@PPaymentdt", null),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var RE = await db.Database.SqlQuery<object>("CALL Usp_Payment_All(@PFlag,@PPaymentNo,@PPaymentDate,@Pagreement_no,@PProperty_ID,@PProperty_Name,@PUnit_ID,@Punit_Name,@PSupplier_id,@PSupplier_Name,@PPaymentType,@PPaymentMode,@PTotalAmount,@PAmtInWords,@PDDChequeNo,@PCheqdate,@Ppdcstatus,@PBankAcCode,@PBankAcName,@PAdvAcCode,@PNarration,@PCreateduser,@PPaymentdt)", param).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetUnitId(string PropertyId)
        {
            try
            {
                List<OptionModel> model = new List<OptionModel>();
                if (!String.IsNullOrEmpty(PropertyId))
                {
                    return Json(new
                    {
                        UnitId = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Ref_unit_Property_ID_Tawtheeq == PropertyId).OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"),
                        UnitName = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Ref_unit_Property_ID_Tawtheeq == PropertyId).OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_Property_Name"),
                        AgreementNo = new SelectList(db.tbl_agreement.Where(w => w.Delmark != "*" && w.Property_ID_Tawtheeq == PropertyId).OrderBy(o => o.property_id), "Agreement_No", "Agreement_No"),
                        PropertyId = PropertyId
                    });
                }
                else
                {
                    return Json(new
                    {
                        UnitId = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit").OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"),
                        UnitName = new SelectList(db.tbl_propertiesmaster.Where(w => w.Delmark != "*" && w.Property_Flag == "Unit").OrderBy(o => o.Ref_unit_Property_ID_Tawtheeq), "Unit_ID_Tawtheeq", "Unit_Property_Name"),
                        AgreementNo = new SelectList(db.tbl_agreement.Where(w => w.Delmark != "*").OrderBy(o => o.property_id), "Agreement_No", "Agreement_No"),
                        //PropertyId = db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Property_Name == PropertyName && w.Property_Flag == "Property").Property_ID_Tawtheeq,
                        //PropertyName = PropertyName
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetProperty(string UnitId)
        {
            try
            {
                List<OptionModel> model = new List<OptionModel>();
                if (!String.IsNullOrEmpty(UnitId))
                {
                    return Json(new
                    {
                        PropertyId = new SelectList(db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Unit_ID_Tawtheeq == UnitId).Ref_unit_Property_ID_Tawtheeq, "Ref_unit_Property_ID_Tawtheeq", "Ref_unit_Property_ID_Tawtheeq"),
                        PropertyName = new SelectList(db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Property_Flag == "Unit" && w.Unit_ID_Tawtheeq == UnitId).Ref_Unit_Property_Name, "Ref_unit_Property_ID_Tawtheeq", "Ref_Unit_Property_Name"),
                        AgreementNo = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Unit_ID_Tawtheeq == UnitId).Agreement_No.ToString(), "Agreement_No", "Agreement_No"),
                        UnitName = db.tbl_propertiesmaster.FirstOrDefault(w => w.Delmark != "*" && w.Unit_ID_Tawtheeq == UnitId && w.Property_Flag == "Unit").Unit_ID_Tawtheeq,
                        UnitId = UnitId
                    });
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetAgreementDetails(int? AgreementNo)
        {

            try
            {
                var invoiceDropdown = new InvoiceDropdown();
                var agreement = await db.tbl_agreement.FirstOrDefaultAsync(x => x.Agreement_No == AgreementNo);
                if (agreement != null)
                {
                    var property = new PropertyDropdown()
                    {
                        Propertyid = agreement.Property_ID_Tawtheeq,
                        PropertyName = agreement.Properties_Name
                    };
                    var unit = new UnitDropdown()
                    {
                        Unitid = agreement.Unit_ID_Tawtheeq,
                        unitName = agreement.Unit_Property_Name,
                    };

                    invoiceDropdown.Properties.Add(property);
                    invoiceDropdown.Units.Add(unit);
                }
                return Json(invoiceDropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }

            //try
            //{
            //    List<OptionModel> model = new List<OptionModel>();
            //    if (AgreementNo != 0 && db.tbl_agreement.Any(w => w.Delmark != "*" && w.Agreement_No == AgreementNo))
            //    {
            //        return Json(new
            //        {
            //            PropertyId = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Property_ID_Tawtheeq, "Property_ID_Tawtheeq", "Property_ID_Tawtheeq"),
            //            PropertyName = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Properties_Name, "Property_ID_Tawtheeq", "Properties_Name"),
            //            UnitName = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Unit_Property_Name, "Unit_ID_Tawtheeq", "Unit_Property_Name"),
            //            UnitId = new SelectList(db.tbl_agreement.FirstOrDefault(w => w.Delmark != "*" && w.Agreement_No == AgreementNo).Unit_ID_Tawtheeq, "Unit_ID_Tawtheeq", "Unit_ID_Tawtheeq"),
            //        });
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
