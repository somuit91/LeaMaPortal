﻿using LeaMaPortal.Models;
using LeaMaPortal.DBContext;
using MvcPaging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class UserCreationController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: UserCreation
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                var users = db.tbl_userrights.Where(x => x.Delmark != "*");
                if (!string.IsNullOrWhiteSpace(Search))
                {
                    users = users.Where(x => x.Name.Contains(Search));
                }
                return PartialView("../Master/UserCreation/_List", users.OrderBy(x => x.id).Select(x => new UserCreationViewModel()
                {
                    AddConfig = x.AddConfig,
                    Category = x.Category,
                    Cnfpwd = x.Cnfpwd,
                    Createduser = x.Createduser,
                    DeleteConfig = x.DeleteConfig,
                    EditConfig = x.EditConfig,
                    Email = x.Email,
                    id = x.id,
                    MenuConfig = x.MenuConfig,
                    Name = x.Name,
                    Phoneno = x.Phoneno,
                    Pwd = x.Pwd,
                    Userid = x.Userid
                }).ToPagedList(currentPageIndex, PageSize));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            ViewBag.Category = new SelectList(Common.Role);
            ViewBag.MenuRights = db.tbl_formmaster.OrderBy(x => x.Id).Select(x => new MenuRights()
            {
                Id = x.Id,
                MenuName = x.MenuName
            });
            return PartialView("../Master/UserCreation/_AddOrUpdate", new UserCreationViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddOrUpdate(UserCreationViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = null;
                    if (model.id == 0)
                    {
                        PFlag = "INSERT";
                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    object[] parameters = {
                         new MySqlParameter("@PFlag", PFlag),
                         new MySqlParameter("@Pid", model.id),
                         new MySqlParameter("@PName",model.Name),
                         new MySqlParameter("@PUserid", model.Userid),
                         new MySqlParameter("@PPwd", model.Pwd),
                         new MySqlParameter("@PCnfPwd", model.Cnfpwd),
                         new MySqlParameter("@PCategory", model.Category),
                         new MySqlParameter("@PEmail", model.Email),
                         new MySqlParameter("@PPhoneno", model.Phoneno),
                         new MySqlParameter("@PAddConfig", model.AddConfig),
                         new MySqlParameter("@PEditConfig", model.EditConfig),
                         new MySqlParameter("@PDeleteConfig", model.DeleteConfig),
                         new MySqlParameter("@PMenuConfig", model.MenuConfig),
                         new MySqlParameter("@PActive", model.Active),
                         new MySqlParameter("@PCreatedUser",System.Web.HttpContext.Current.User.Identity.Name)
                    };

                    var userCreation = await db.Database.SqlQuery<object>("call leama.Usp_Userrights_All(@PFlag, @Pid, @PName, @PUserid, @PPwd, @PCnfpwd, @PCategory, @PEmail, @PPhoneno, @PAddConfig, @PEditConfig, @PDeleteConfig, @PMenuConfig, @PActive, @PCreateduser)", parameters).ToListAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public PartialViewResult Edit(int? id)
        {
            try
            {
                var userRights = db.tbl_userrights.FirstOrDefault(x => x.Delmark != "*" && x.id == id);
                if (userRights == null)
                {
                    return PartialView("../Master/UserCreation/_AddOrUpdate", new UserCreationViewModel());
                    //return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                ViewBag.Category = new SelectList(Common.Role, userRights.Category);
                var menuRights = userRights.MenuConfig.Split(',').Select(int.Parse).ToArray();
                //1,2,5,6,7,8,9
                ViewBag.MenuRights = db.tbl_formmaster.OrderBy(x=>x.Id).ToList().Select(x => new MenuRights()
                {
                    Id = x.Id,
                    MenuName = x.MenuName,
                    IsChecked = menuRights.Contains(x.Id)
                });
                UserCreationViewModel model = new UserCreationViewModel()
                {
                    AddConfig = userRights.AddConfig,
                    Category = userRights.Category,
                    Cnfpwd = userRights.Cnfpwd,
                    Createduser = userRights.Createduser,
                    DeleteConfig = userRights.DeleteConfig,
                    EditConfig = userRights.EditConfig,
                    Email = userRights.Email,
                    id = userRights.id,
                    MenuConfig = userRights.MenuConfig,
                    Name = userRights.Name,
                    Phoneno = userRights.Phoneno,
                    Pwd = userRights.Pwd,
                    Userid = userRights.Userid
                };
                return PartialView("../Master/UserCreation/_AddOrUpdate", model);
            }
            catch
            {
                return PartialView("../Master/UserCreation/_AddOrUpdate", new UserCreationViewModel());
            }
        }

        public async Task<ActionResult> Delete(int? id)
        {
            MessageResult result = new MessageResult();
            if (id == null)
            {
                return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
            }
            object[] parameters = {
                         new MySqlParameter("@PFlag", "DELETE"),
                         new MySqlParameter("@Pid", id),
                         new MySqlParameter("@PName",""),
                         new MySqlParameter("@PUserid", ""),
                         new MySqlParameter("@PPwd", ""),
                         new MySqlParameter("@PCnfPwd", ""),
                         new MySqlParameter("@PCategory", ""),
                         new MySqlParameter("@PEmail", ""),
                         new MySqlParameter("@PPhoneno", ""),
                         new MySqlParameter("@PAddConfig", 0),
                         new MySqlParameter("@PEditConfig", 0),
                         new MySqlParameter("@PDeleteConfig", 0),
                         new MySqlParameter("@PMenuConfig", ""),
                         new MySqlParameter("@PActive", 0),
                         new MySqlParameter("@PCreatedUser",System.Web.HttpContext.Current.User.Identity.Name)
                    };

            var userCreation = await db.Database.SqlQuery<object>("call leama.Usp_Userrights_All(@PFlag, @Pid, @PName, @PUserid, @PPwd, @PCnfpwd, @PCategory, @PEmail, @PPhoneno, @PAddConfig, @PEditConfig, @PDeleteConfig, @PMenuConfig, @PActive, @PCreateduser)", parameters).ToListAsync();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        // GET: UserCreation/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: UserCreation/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: UserCreation/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserCreation/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: UserCreation/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserCreation/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: UserCreation/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
