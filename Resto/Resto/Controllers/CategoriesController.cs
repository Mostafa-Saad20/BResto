using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using PagedList;
using Resto.Models;

namespace Resto.Controllers
{
    public class CategoriesController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Categories
        public ActionResult Index(string searchCat, string sortOrder, int? page)
        {
            if (Session["AdminRegister"] != null)
            {
                // paging ..
                int pageSize = 4;
                int pageNumber = page ?? 1;

                IQueryable<Category> cats;
                // search ..
                if (searchCat != null)
                {
                    cats = db.Categories
                        .Where(c => c.Name.Contains(searchCat))
                        .OrderBy(c => c.Name);
                }
                // sorting ..
                else
                {
                    ViewBag.CurrentSort = sortOrder;
                    if (sortOrder == "name_desc")
                    {
                        cats = db.Categories.OrderByDescending(c => c.Name);
                    }
                    else { 
                        cats = db.Categories.OrderBy(c => c.Name);
                    }
                }
                // get sorted categories with paging ..
                return View(cats.ToPagedList(pageNumber, pageSize));
                
            }
            else return Redirect("~/Admin/Login");
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            if (Session["AdminRegister"] != null)
            {
                return View();
            }
            else return Redirect("~/Admin/Login");
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                // check category exist ..
                var savedCategory = db.Categories
                    .Where(c => c.Name == category.Name)
                    .FirstOrDefault();

                if (savedCategory == null)
                {
                    // save image in folder ..
                    var path = Path.Combine(Server.MapPath("~/Uploads"), Image.FileName);
                    Image.SaveAs(path);

                    // save data in DB
                    category.Image = Image.FileName;
                    db.Categories.Add(category);
                    db.SaveChanges();
                    // redirect 
                    return RedirectToAction("Index");
                }
                else { ViewBag.CategoryExist = "Category " + category.Name + " is already exists"; }
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            else return Redirect("~/Admin/Login");
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                bool saveFailed;
                do
                {
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        saveFailed = true;

                        // Update the values of the entity that failed to save from the store
                        ex.Entries.Single().Reload();
                    }

                } while (saveFailed);
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                return View(category);
            }
            else return Redirect("~/Admin/Login");
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
