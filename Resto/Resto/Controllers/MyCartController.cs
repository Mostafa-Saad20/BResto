using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Resto.Models;

namespace Resto.Controllers
{
    public class MyCartController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: MyCart/Index
        public ActionResult Index()
        {
            if (Session["CustomerId"] != null)
            {
                // get Cart of the Current Customer
                var cstmrId = Session["CustomerId"];
                var cartItems = db.CartItems
                    .SqlQuery("SELECT * FROM CartItems WHERE CustomerId = " + cstmrId).ToList();
                
                decimal? totalCart = 0;
                foreach (var item in cartItems) {
                    totalCart += item.Price * item.Quantity;
                }
                
                // get Total of Cart for view ..
                ViewBag.TotalCart = totalCart;

                return View(cartItems.ToList());
            }
            else return Redirect("~/Home/Login");
        }

        // GET: MyCart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", cartItem.CustomerId);
            return View(cartItem);
        }

        // POST: MyCart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                if (cartItem.Quantity >= 1)
                { 
                   // update only Quantity
                    db.CartItems.Attach(cartItem);
                    db.Entry(cartItem).Property(c => c.Quantity).IsModified = true;
                    // keep the rest ..
                    db.Entry(cartItem).Property(c => c.CustomerId).IsModified = false;
                    db.Entry(cartItem).Property(c => c.Name).IsModified = false;
                    db.Entry(cartItem).Property(c => c.Price).IsModified = false;

                    // save subtotal ..
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", cartItem.CustomerId);
            return View(cartItem);
        }

        // GET: MyCart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // POST: MyCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
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
