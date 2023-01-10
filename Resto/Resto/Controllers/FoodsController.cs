using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Resto.Models;
using PagedList;

namespace Resto.Controllers
{
    public class FoodsController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: /Foods
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            if (Session["AdminRegister"] != null)
            {
                // paging ..
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                
                IQueryable<Food> foodList;
                // search 
                if (searchString != null)
                {
                    foodList = db.Food
                        .Where(f => f.Name.Contains(searchString))
                        .OrderBy(f => f.Name)
                        .Include(f => f.Category);
                }
                // sorting ..
                else{
                    ViewBag.CurrentSort = sortOrder;
                    ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    ViewBag.PriceSortParam = string.IsNullOrEmpty(sortOrder) ? "price_asc" : "price_desc";

                    switch (sortOrder) 
                    {
                        case "name_desc":
                            foodList = db.Food.OrderByDescending(f => f.Name).Include(f => f.Category);
                            break;
                        case "price_asc":
                            foodList = db.Food.OrderBy(f => f.Price).Include(f => f.Category);
                            break;
                        case "price_desc":
                            foodList = db.Food.OrderByDescending(f => f.Price).Include(f => f.Category);
                            break;
                        default:
                            foodList = db.Food.OrderBy(f => f.Name).Include(f => f.Category);
                            break;
                    }
                }
                // get food list with pages ..
                return View(foodList.ToPagedList(pageNumber, pageSize));
                
            }
            else return Redirect("~/Admin/Login");
        }

        // GET: /Foods/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Food food = db.Food.Find(id);
                if (food == null)
                {
                    return HttpNotFound();
                }
                return View(food);
            }
            else return Redirect("~/Admin/Login");
        }

        // GET: Foods/Create
        public ActionResult Create()
        {
            if (Session["AdminRegister"] != null)
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
                return View();
            }
            else return Redirect("~/Admin/Login");
        }

        // POST: Foods/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Food food)
        {
            if (ModelState.IsValid)
            {
                db.Food.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
            return View(food);
        }

        // GET: Foods/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Food food = db.Food.Find(id);
                if (food == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
                return View(food);
            }
            else return Redirect("~/Admin/Login");
        }

        // POST: Foods/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", food.CategoryId);
            return View(food);
        }

        // GET: Foods/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Food food = db.Food.Find(id);
                if (food == null)
                {
                    return HttpNotFound();
                }
                return View(food);
            }
            else return Redirect("~/Admin/Login");
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Food food = db.Food.Find(id);
            db.Food.Remove(food);
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
