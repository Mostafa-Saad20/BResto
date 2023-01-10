using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Resto.Models;

namespace Resto.Controllers
{
    public class OrdersController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            // when user registered
            if (Session["CustomerId"] != null)
            {
                // get Cart of the Current Customer
                var cstmrId = Session["CustomerId"];
                var cartItems = db.CartItems
                    .SqlQuery("SELECT * FROM CartItems WHERE CustomerId = " + cstmrId).ToList();

                if (cartItems != null)
                {
                    // get CartItems list ..
                    ViewBag.CartItems = cartItems.ToList();
                    
                    decimal? totalCart = 0;
                    foreach (var item in cartItems)
                    {
                        totalCart += item.Price * item.Quantity;
                    }

                    // get Total of Cart for view ..
                    ViewBag.TotalCart = totalCart;
                }
                return View();
            }
            else return Redirect("~/Home/Login");
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            // get Cart of the Current Customer
            var cstmrId = Session["CustomerId"];
            var cartItems = db.CartItems
                .SqlQuery("SELECT * FROM CartItems WHERE CustomerId = " + cstmrId).ToList();

            if (cartItems != null)
            {
                // get CartItems list ..
                ViewBag.CartItems = cartItems.ToList();

                decimal totalCart = 0;
                foreach (var item in cartItems)
                {
                    totalCart += item.Price * item.Quantity;
                }
                // get Total of Cart for view ..
                ViewBag.TotalCart = totalCart;

                // check order valid 
                if (ModelState.IsValid)
                {
                    if (ValidateOrderData(order))
                    {
                        // set Order data 
                        order.CustomerId = (int)Session["CustomerId"];
                        order.TotalPrice = totalCart;
                        order.Date   = DateTime.Today.Date;
                        order.Time   = DateTime.Now.TimeOfDay;
                        order.DeliveryAddress = order.Type.Equals("Pickup") ? "no address" : order.DeliveryAddress;
                        // save order to DB
                        db.Orders.Add(order);
                        db.SaveChanges();
                        return View("ConfirmOrder");
                    }
                }
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // ******* Order Validation ********
        // --------------------------------- //
        private bool ValidateOrderData(Order order)
        {
            if (!Regex.IsMatch(order.Name, @"[a-zA-Z\s\-*]+"))
            {
                ViewBag.ErrorName = "Invalid Name!";
                return false;
            }
            else if (!Regex.IsMatch(order.CustomerPhone, @"[0-9]") || order.CustomerPhone.Length != 11)
            {
                ViewBag.ErrorNumber = "Invalid Phone Number!";
                return false;
            }
            // on select delivery
            else if (order.Type.Equals("Delivery"))
            {
                if (order.DeliveryAddress.IsNullOrWhiteSpace())
                {
                    ViewBag.ErrorAddress = "Please, Enter Delivery Address";
                    return false;
                }
                else return true;
            }
            else return true;
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