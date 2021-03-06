﻿using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using MvcPaging;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class PropertyMasterController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        //private string user = "rmv";
        // GET: PropertyMaster
        public async Task<ActionResult> Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, PageSize);
                //TenantCompanyViewModel model = new TenantCompanyViewModel();
                var query = db.tbl_propertiesmaster.Where(x => x.Delmark != "*");
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    query = query.Where(x => x.Property_Name.Contains(Search));
                }
                var list = await query.OrderBy(x => x.id).ToListAsync();
                return PartialView("../Master/PropertyMaster/_List", list.Select(x => Map(x)).ToPagedList(currentPageIndex, PageSize));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            PropertyViewModel model = new PropertyViewModel();
            ViewBag.TitleDisplay = new SelectList(Common.Title, Common.DefaultTitle);
            //var region = .Select(x => x.Region_Name);

            model.Property_Usage_unitList = new SelectList("", "", "");
            model.Property_Type_unitList = new SelectList("", "", "");
            model.Caretaker_IDList = new SelectList("", "", "");

            var propertyTypeData = db.tbl_propertytypemaster.Where(x => x.Type_Flag == "Property").Select(x => new PropertyTypeModel { PropertyCategory = x.Type_name, Usage_name = x.Usage_name }).ToList();
            ViewBag.Property_Usage = new SelectList(propertyTypeData.Select(x => x.Usage_name));
            ViewBag.Property_Type = new SelectList(propertyTypeData.Select(x => x.PropertyCategory));


            propertyTypeData = db.tbl_propertytypemaster.Where(x => x.Type_Flag == "Unit").Select(x => new PropertyTypeModel { PropertyCategory = x.Type_name, Usage_name = x.Usage_name }).ToList();
            ViewBag.Property_Usage_unit = new SelectList(propertyTypeData.Select(x => x.Usage_name));
            ViewBag.Property_Type_unit = new SelectList(propertyTypeData.Select(x => x.PropertyCategory));

            var propertyMaster = db.tbl_propertiesmaster.Where(x => x.Noofunits > 0 &&  x.Status != "Avail" && x.Delmark != "*" &&  x.Company_occupied_Flag != 1).ToList();
            ViewBag.Ref_unit_Property_ID_Tawtheeq = new SelectList(propertyMaster.Select(x => new { PropertyIdTawtheeq = x.Property_ID_Tawtheeq, PropertyId = x.Property_Id }), "PropertyId", "PropertyIdTawtheeq");
            
            ViewBag.Ref_Unit_Property_ID = new SelectList(propertyMaster.Select(x => x.Property_Id));
            ViewBag.Ref_Unit_Property_Name = new SelectList(propertyMaster.Select(x => new { PropertyName = x.Property_Name, PropertyId = x.Property_Id }), "PropertyId", "PropertyName");


            ViewBag.Region_Name = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
            ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
            ViewBag.Caretaker_ID = new SelectList(db.tbl_caretaker.Where(x => x.Delmark != "*").OrderBy(x => x.Id), "Caretaker_id", "Caretaker_id");
            //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
            ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
            ViewBag.Profession = new SelectList(Common.Profession);
            ViewBag.PropertyId = db.tbl_propertiesmaster.OrderByDescending(x => x.Property_Id).FirstOrDefault()?.Property_Id + 1;
            ViewBag.Utility_Name = new SelectList(db.tbl_utilitiesmaster.Where(x => x.Delmark != "*").OrderBy(x => x.Utility_id), "Utility_id", "Utility_Name");
            ViewBag.Utility_ID  = new SelectList(db.tbl_utilitiesmaster.Where(x => x.Delmark != "*").OrderBy(x => x.Utility_id), "Utility_Name", "Utility_id");

            ViewBag.Facility_Name = new SelectList(db.tbl_facilitymaster.Where(x => x.Delmark != "*").OrderBy(x => x.Facility_id), "Facility_id", "Facility_Name");
            ViewBag.Facility_id = new SelectList(db.tbl_facilitymaster.Where(x => x.Delmark != "*").OrderBy(x => x.Facility_id), "Facility_Name","Facility_id");
            return PartialView("../Master/PropertyMaster/_AddOrUpdate", model);
        }
        // GET: PropertyMaster/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult List()
        {
            var list = db.tbl_propertiesmaster.ToList();
            return View(list);
        }
        // GET: PropertyMaster/Create
        public ActionResult Create()
        {
            try
            {
                PropertyViewModel model = new PropertyViewModel();
                //model.Title = Common.DefaultTitle;
                ViewBag.TitleDisplay = new SelectList(Common.Title, Common.DefaultTitle);
                //var region = .Select(x => x.Region_Name);
                ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name");
                //var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
                ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name");
                ViewBag.Profession = new SelectList(Common.Profession);
                ViewBag.PropertyId = db.tbl_propertiesmaster.OrderByDescending(x => x.Property_Id).FirstOrDefault()?.Property_Id + 1;
                
                return PartialView("../Master/TenantIndividual/_AddOrUpdate", model);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        // POST: PropertyMaster/Create
        [HttpPost]
        public async Task<ActionResult> Create(PropertyViewModel model)
        {
            try
            {
                string PFlag = Common.UPDATE;
                if (model.Property_Id == 0)
                {
                    PFlag = Common.INSERT;
                    var propertyMaster = await db.tbl_propertiesmaster.OrderByDescending(r => r.Property_Id).FirstOrDefaultAsync();
                    model.Property_Id = propertyMaster != null ? propertyMaster.Property_Id + 1 : 1;
                }
                string facility = null, utility = null;
                foreach(var propertiesdt in model.PropertiesdtList)
                {
                    if (string.IsNullOrWhiteSpace(facility))
                    {
                        facility = "(" + model.Property_Id + ",'" + model.Property_ID_Tawtheeq + "','" + model.Unit_ID_Tawtheeq + "','" + propertiesdt.Facility_id + "','" + propertiesdt.Facility_Name + "','" + propertiesdt.Numbers_available + "'," + DateTime.Now.Year + ")";
                    }
                    else
                    {
                        facility = facility + ",(" + model.Property_Id + ",'" + model.Property_ID_Tawtheeq + "','" + model.Unit_ID_Tawtheeq + "','" + propertiesdt.Facility_id + "','" + propertiesdt.Facility_Name + "','" + propertiesdt.Numbers_available + "'," + DateTime.Now.Year + ")";
                    }
                }
                model.propertiesdt = facility;
                foreach (var propertiesdt1 in model.Propertiesdt1List)
                {
                    if (string.IsNullOrWhiteSpace(utility))
                    {
                        utility = "(" + model.Property_Id + ",'" + model.Property_ID_Tawtheeq + "','" + model.Unit_ID_Tawtheeq + "','" + propertiesdt1.Utility_id + "','" + propertiesdt1.Utility_Name + "'," + DateTime.Now.Year + ")";
                    }
                    else
                    {
                        utility = utility + ",(" + model.Property_Id + ",'" + model.Property_ID_Tawtheeq + "','" + model.Unit_ID_Tawtheeq + "','" + propertiesdt1.Utility_id + "','" + propertiesdt1.Utility_Name + "'," + DateTime.Now.Year + ")";
                    }
                }
                model.propertiesdt1 = utility;
                object[] param = Helper.GetMySqlParameters<PropertyViewModel>(model, PFlag, System.Web.HttpContext.Current.User.Identity.Name);

                var _result = await db.Database.SqlQuery<object>(@"call leama.Usp_Properties_All(@PFlag, @PProperty_Flag, @PProperty_ID_Tawtheeq, @PProperty_Id, @PProperty_Name, @PCompound, @PZone, @Psector, @Pplotno, @Pownedbyregistrant, @PProperty_Usage, @PProperty_Type, @PCommercial_villa, @PStreet_Name, @PAddress1, @PAddress2, @PAddress3, @PRegion_Name, @PCountry, @PCity, @PState, @PExternalrefno, @PNoofoffloors, @PNoofunits, @PBuiltarea, @PPlotarea, @PLeasablearea, @Pcommonarea, @Pcompletion_Date, @PAEDvalue, @PPurchased_date, @PValued_Date, @PStatus, @PVacant_Start_Date, @PCaretaker_Name, @PCaretaker_ID, @PRental_Rate_Month, @PComments, @PRef_unit_Property_ID_Tawtheeq, @PRef_Unit_Property_ID, @PRef_Unit_Property_Name, @PUnit_ID_Tawtheeq, @PUnit_Property_Name, @PExternalrefno_unit, @PAEDvalue_unit, @PPurchased_date_unit, @PValued_Date_unit, @PStatus_unit, @PVacant_Start_Date_Unit, @PRental_Rate_Month_unit, @PFloorno, @PFloorlevel, @PProperty_Usage_unit, @PProperty_Type_unit, @PTotal_Area, @PUnit_Common_Area, @PCommon_Area, @PParkingno, @PUnitcomments, @PCreateduser, @PCompany_occupied_Flag, @Ppropertiesdt, @Ppropertiesdt1)", param).ToListAsync();

                return RedirectToAction("../Master/Index", new { selected = 9 });

            }
            catch (Exception e)
            {
                throw;
            }
        }

        // GET: PropertyMaster/Edit/5
        public async Task<ActionResult> Edit(int Property_Id)
        {
            try
            {
                var properties = await db.tbl_propertiesmaster.FindAsync(Property_Id);
                //TenantIndividualViewModel model = Map(tenant);
                //ViewBag.TitleDisplay = new SelectList(Common.Title, tenant.Title);
                ////var region = .Select(x => x.Region_Name);
                //ViewBag.City = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name", tenant.City);
                ////var country = db.tbl_country.Where(x => x.Delmark != "*").Select(x => x.Country_name);
                //ViewBag.Nationality = new SelectList(db.tbl_country.Where(x => x.Delmark != "*").OrderBy(x => x.Country_name), "Country_name", "Country_name", tenant.Nationality);
                //ViewBag.Profession = new SelectList(Common.Profession, tenant.Profession);
                //ViewBag.Tenant_Id = tenantId;
                //model.TenantDocumentList = await db.tbl_tenant_individual_doc.Where(x => x.Tenant_Id == tenantId)
                //                            .Select(x => new tbl_tenant_individual_docVM()
                //                            {
                //                                id = x.id,
                //                                Tenant_Id = x.Tenant_Id,
                //                                Doc_name = x.Doc_name,
                //                                Doc_Path = x.Doc_Path
                //                            }).ToListAsync();
                PropertyViewModel model = Map(properties);
                ViewBag.Caretaker_ID = new SelectList(db.tbl_caretaker.Where(x => x.Delmark != "*").OrderBy(x => x.Id), "Caretaker_id", "Caretaker_id", model.Caretaker_ID);
                ViewBag.PropertyId = model.Property_Id;

                model.PropertiesdtList = db.tbl_propertiesdt.Where(x => x.Property_Id == model.Property_Id).Select(x => new Propertiesdt()
                {
                    Facility_id = x.Facility_id,
                    Facility_Name = x.Facility_Name,
                    Numbers_available = x.Numbers_available,
                    Property_Id = x.Property_Id.HasValue ? x.Property_Id.Value : 0,
                    Property_ID_Tawtheeq = x.Property_ID_Tawtheeq,
                    Unit_ID_Tawtheeq = x.Unit_ID_Tawtheeq
                }).ToList();

                model.Propertiesdt1List = db.tbl_propertiesdt1.Where(x => x.Property_Id == model.Property_Id).Select(x => new Propertiesdt1()
                {
                    Property_Id = x.Property_Id.HasValue ? x.Property_Id.Value : 0,
                    Property_ID_Tawtheeq = x.Property_ID_Tawtheeq,
                    Unit_ID_Tawtheeq = x.Unit_ID_Tawtheeq,
                    Utility_id = x.Utility_id,
                    Utility_Name = x.Utility_Name
                }).ToList();

                model.Property_Usage_unitList = new SelectList("", "", "");
                model.Property_Type_unitList = new SelectList("", "", "");
                model.Caretaker_IDList = new SelectList("", "", "");

                var propertyTypeData = db.tbl_propertytypemaster.Where(x => x.Type_Flag == "Property").Select(x => new PropertyTypeModel { PropertyCategory = x.Type_name, Usage_name = x.Usage_name }).ToList();
                ViewBag.Property_Usage = new SelectList(propertyTypeData.Select(x => x.Usage_name), model.Property_Usage);
                ViewBag.Property_Type = new SelectList(propertyTypeData.Select(x => x.PropertyCategory),model.Property_Type);


                propertyTypeData = db.tbl_propertytypemaster.Where(x => x.Type_Flag == "Unit").Select(x => new PropertyTypeModel { PropertyCategory = x.Type_name, Usage_name = x.Usage_name }).ToList();
                ViewBag.Property_Usage_unit = new SelectList(propertyTypeData.Select(x => x.Usage_name), model.Property_Usage_unit);
                ViewBag.Property_Type_unit = new SelectList(propertyTypeData.Select(x => x.PropertyCategory), model.Property_Type_unit);

                var propertyMaster = db.tbl_propertiesmaster.Where(x => x.Noofunits > 0 && x.Status != "Avail" && x.Delmark != "*" && x.Company_occupied_Flag != 1).ToList();
                ViewBag.Ref_unit_Property_ID_Tawtheeq = new SelectList(propertyMaster.Select(x => new { PropertyIdTawtheeq = x.Property_ID_Tawtheeq, PropertyId = x.Property_Id }), "PropertyId", "PropertyIdTawtheeq", model.Ref_Unit_Property_ID);
                ViewBag.Ref_Unit_Property_ID = new SelectList(propertyMaster.Select(x => x.Property_Id), model.Ref_Unit_Property_ID);
                ViewBag.Ref_Unit_Property_Name = new SelectList(propertyMaster.Select(x => new { PropertyName = x.Property_Name, PropertyId = x.Property_Id }), "PropertyId", "PropertyName", model.Ref_Unit_Property_ID);

                ViewBag.Region_Name = new SelectList(db.tbl_region.Where(x => x.Delmark != "*").OrderBy(x => x.Region_Name), "Region_Name", "Region_Name", model.Region_Name);

                ViewBag.Utility_Name = new SelectList(db.tbl_utilitiesmaster.Where(x => x.Delmark != "*").OrderBy(x => x.Utility_id), "Utility_id", "Utility_Name");
                ViewBag.Utility_ID = new SelectList(db.tbl_utilitiesmaster.Where(x => x.Delmark != "*").OrderBy(x => x.Utility_id), "Utility_Name", "Utility_id");

                ViewBag.Facility_Name = new SelectList(db.tbl_facilitymaster.Where(x => x.Delmark != "*").OrderBy(x => x.Facility_id), "Facility_id", "Facility_Name");
                ViewBag.Facility_id = new SelectList(db.tbl_facilitymaster.Where(x => x.Delmark != "*").OrderBy(x => x.Facility_id), "Facility_Name", "Facility_id");

                return PartialView("../Master/PropertyMaster/_AddOrUpdate", model);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        // POST: PropertyMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PropertyMaster/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: PropertyMaster/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int Property_Id)
        {
            try
            {
                MessageResult result = new MessageResult();
                try
                {
                    var model = await db.tbl_propertiesmaster.FirstOrDefaultAsync(r => r.Property_Id == Property_Id);
                    //PropertyViewModel modelData = new PropertyViewModel();
                    //modelData.Property_Id = model.Property_Id;
                    object[] param = Helper.GetMySqlParameters<PropertyViewModel>(Map(model), Common.DELETE, System.Web.HttpContext.Current.User.Identity.Name);

                    var _result = await db.Database.SqlQuery<object>(@"CALL Usp_Properties_All(@PFlag,
@PProperty_Flag ,
@PProperty_ID_Tawtheeq ,
@PProperty_Id  ,
@PProperty_Name , 
@PCompound, 
@PZone , 
@Psector, 
@Pplotno , 
@Pownedbyregistrant  , 
@PProperty_Usage , 
@PProperty_Type , 
@PCommercial_villa  , 
@PStreet_Name , 
@PAddress1,
@PAddress2,
@PAddress3,
@PRegion_Name,
@PCountry,
@PCity,
@PState
@PExternalrefno , 
@PNoofoffloors  , 
@PNoofunits  , 
@PBuiltarea  ,
@PPlotarea  , 
@PLeasablearea  , 
@Pcommonarea  , 
@Pcompletion_Date  , 
@PAEDvalue  ,
@PPurchased_date  , 
@PValued_Date  , 
@PStatus ,
@PVacant_Start_Date  , 
@PCaretaker_Name , 
@PCaretaker_ID  , 
@PRental_Rate_Month  ,
@PComments  ,
@PRef_unit_Property_ID_Tawtheeq , 
@PRef_Unit_Property_ID,
@PRef_Unit_Property_Name, 
@PUnit_ID_Tawtheeq, 
@PUnit_Property_Name , 
@PExternalrefno_unit ,
@PAEDvalue_unit  , 
@PPurchased_date_unit  , 
@PValued_Date_unit  , 
@PStatus_unit ,
@PVacant_Start_Date_Unit  ,
@PRental_Rate_Month_unit  , 
@PFloorno,
@PFloorlevel , 
@PProperty_Usage_unit , 
@PProperty_Type_unit , 
@PTotal_Area  , 
@PUnit_Common_Area  , 
@PCommon_Area  , 
@PParkingno  , 
@PUnitcomments,
@PCreateduser , 
@PCompany_occupied_Flag  , 
@Ppropertiesdt,
@Ppropertiesdt1

                                    )", param).ToListAsync();

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch(Exception ex)
                {
                    return Json(new MessageResult() { Errors = "Internal server error" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception e)
            {
                return View();
            }
        }

        private PropertyViewModel Map(tbl_propertiesmaster propertyMaster)
        {
            PropertyViewModel model = new PropertyViewModel()
            {
                Address1 = propertyMaster.Address1,
                Address2 = propertyMaster.Address2,
                Address3 = propertyMaster.Address3,
                AEDvalue = propertyMaster.AEDvalue,
                AEDvalue_unit = propertyMaster.AEDvalue_unit,
                Builtarea = propertyMaster.Builtarea,
                Caretaker_ID = propertyMaster.Caretaker_ID,
                Caretaker_Name = propertyMaster.Caretaker_Name,
                City = propertyMaster.City,
                Comments = propertyMaster.Comments,
                Commercial_villa = propertyMaster.Commercial_villa,
                commonarea = propertyMaster.commonarea,
                Company_occupied_Flag = propertyMaster.Company_occupied_Flag,
                completion_Date = propertyMaster.completion_Date,
                Compound = propertyMaster.Compound,
                Country = propertyMaster.Country,
                Createddatetime = propertyMaster.Createddatetime,
                Createduser = propertyMaster.Createduser,
                Externalrefno = propertyMaster.Externalrefno,
                Externalrefno_unit = propertyMaster.Externalrefno_unit,
                Floorlevel = propertyMaster.Floorlevel,
                Floorno = propertyMaster.Floorno,
                Leasablearea = propertyMaster.Leasablearea,
                Noofoffloors = propertyMaster.Noofoffloors,
                Noofunits = propertyMaster.Noofunits,
                ownedbyregistrant = propertyMaster.ownedbyregistrant,
                Parkingno = propertyMaster.Parkingno,
                Plotarea = propertyMaster.Plotarea,
                plotno = propertyMaster.plotno,
                Property_Flag = propertyMaster.Property_Flag,
                Property_Id = propertyMaster.Property_Id,
                Property_ID_Tawtheeq = propertyMaster.Property_ID_Tawtheeq,
                Property_Name = propertyMaster.Property_Name,
                Property_Type = propertyMaster.Property_Type,
                Property_Type_unit = propertyMaster.Property_Type_unit,
                Property_Usage = propertyMaster.Property_Usage,
                Property_Usage_unit = propertyMaster.Property_Usage_unit,
                Ref_Unit_Property_ID = propertyMaster.Ref_Unit_Property_ID,
                Purchased_date = propertyMaster.Purchased_date,
                Purchased_date_unit = propertyMaster.Purchased_date_unit,
                Ref_unit_Property_ID_Tawtheeq = propertyMaster.Ref_unit_Property_ID_Tawtheeq,
                Ref_Unit_Property_Name = propertyMaster.Ref_Unit_Property_Name,
                Region_Name = propertyMaster.Region_Name,
                Rental_Rate_Month = propertyMaster.Rental_Rate_Month,
                Rental_Rate_Month_unit = propertyMaster.Rental_Rate_Month_unit,
                sector = propertyMaster.sector,
                State = propertyMaster.State,
                Status = propertyMaster.Status,
                Status_unit = propertyMaster.Status_unit,
                Street_Name = propertyMaster.Street_Name,
                Total_Area = propertyMaster.Total_Area,
                Unitcomments = propertyMaster.Unitcomments,
                Common_Area = propertyMaster.Common_Area,
                Unit_Common_Area = propertyMaster.Unit_Common_Area,
                Unit_ID_Tawtheeq = propertyMaster.Unit_ID_Tawtheeq,
                Unit_Property_Name = propertyMaster.Unit_Property_Name,
                Vacant_Start_Date = propertyMaster.Vacant_Start_Date,
                Vacant_Start_Date_Unit = propertyMaster.Vacant_Start_Date_Unit,
                Valued_Date = propertyMaster.Valued_Date,
                Valued_Date_unit = propertyMaster.Valued_Date_unit,
                Zone = propertyMaster.Zone
            };

            return model;
        }
    }
}

//public static class PropertyCopy
//{
//    public static void Copy<TDest, TSource>(TDest destination, TSource source)
//        where TSource : class
//        where TDest : class
//    {
//        var destProperties = destination.GetType().GetProperties()
//            .Where(x => !x.CustomAttributes.Any(y => y.AttributeType.Name == PropertyCopyIgnoreAttribute.Name) && x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);
//        var sourceProperties = source.GetType().GetProperties()
//            .Where(x => !x.CustomAttributes.Any(y => y.AttributeType.Name == PropertyCopyIgnoreAttribute.Name) && x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);
//        var copyProperties = sourceProperties.Join(destProperties, x => x.Name, y => y.Name, (x, y) => x);
//        foreach (var sourceProperty in copyProperties)
//        {
//            var prop = destProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
//            prop.SetValue(destination, sourceProperty.GetValue(source));
//        }
//    }
//}
