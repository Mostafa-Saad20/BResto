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
    public class AdminController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: /Login
        public ActionResult Login() 
        {
            return View();    
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                // check admin email & password ..
                var savedAdmin = db.Admins.Where(a => a.Email.Equals(admin.Email)).FirstOrDefault();
                
                if (savedAdmin != null) 
                {
                    if (savedAdmin.Password.Equals(admin.Password))
                    {
                        // save session 
                        Session["AdminId"] = admin.Id;
                        Session["AdminEmail"] = admin.Email;
                        Session["AdminRegister"] = true;
                        // redirect 
                        return RedirectToAction("Index");
                    }
                    else { ViewBag.AdminError = "Error Email or Password!"; }
                }
                else { ViewBag.AdminError = "Unauthorized Access!"; }
            }

            return View(admin);
        }

        // GET: Admin/Logout
        public ActionResult Logout()
        {
            // check admin registration
            if (Session["AdminRegister"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login");
        }

        // POST: Admin/LogoutConfirm
        [HttpPost]
        [ActionName("Logout")]
        public ActionResult LogoutConfirm()
        {
            // check admin registration
            if (Session["AdminRegister"] != null)
            {
                Session.Clear();
            }
            return RedirectToAction("Login");
        }


        // GET: Admin
        public ActionResult Index()
        {
            // check admin registration
            if (Session["AdminRegister"] != null)
            {
                // get some stats ..
                var regCustomers = db.Customers.ToList().Count();
                ViewBag.RegCustomers = regCustomers;

                var todayOrders = db.Orders.Where(o => o.Date == DateTime.Today.Date).ToList().Count();
                ViewBag.TodayOrders = todayOrders;

                var todayReservs = db.Reservations.Where(r => r.Date == DateTime.Today.Date).ToList().Count();
                ViewBag.TodayReservs = todayReservs;

                var totalOrders = db.Orders.ToList().Count();
                ViewBag.TotalOrders = totalOrders;
                
                var totalReserves = db.Reservations.ToList().Count();
                ViewBag.TotalReserves = totalReserves;

                var totalFeedback = db.Feedbacks.ToList().Count();
                ViewBag.TotalFeedback = totalFeedback;

                return View();
            }
            else return RedirectToAction("Login");
        }

        // GET: Admin/AllAdmins
        public ActionResult AllAdmins()
        {
            // check admin registration
            if (Session["AdminRegister"] != null)
            {
                // check admins count ..
                var admins = db.Admins.ToList();
                return View(admins);
            }
            else return RedirectToAction("Login");
        }

        // GET: Admin/AllReserves
        public ActionResult AllReserves()
        {
            // check admin registration
            if (Session["AdminRegister"] != null)
            {
                // check admins count ..
                var reservs = db.Reservations.ToList();
                return View(reservs);
            }
            else return RedirectToAction("Login");
        }

        // GET: Admin/Feedbacks
        public ActionResult Feedbacks()
        {
            // check admin registration
            if (Session["AdminRegister"] != null)
            {
                // check admins count ..
                var feedbacks = db.Feedbacks.ToList();
                return View(feedbacks);
            }
            else return RedirectToAction("Login");
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            if (Session["AdminRegister"] != null)
            {
                return View();
            }
            else return RedirectToAction("Login");
        }

        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Admin admin)
        {
            if (ModelState.IsValid)
            {
                // check if admin already exist
                var existedAdmin = db.Admins.Where(a => a.Email == admin.Email).FirstOrDefault();
                if (existedAdmin == null)
                {
                    
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    return RedirectToAction("AllAdmins");
                }
                else { ViewBag.AdminExists = "This Admin already exists!"; }
            }

            return View(admin);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else return RedirectToAction("Login");
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllAdmins");
            }
            return View(admin);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["AdminRegister"] != null)
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                Admin admin = db.Admins.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            else return RedirectToAction("Login");
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admins.Find(id);
            // when 
            if (!admin.Email.Equals(Session["AdminEmail"])) 
            { 
                db.Admins.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("AllAdmins");
            }
            return View();
        }

        // GET: Admin/Delete/5
        public ActionResult CancelReservation(int? id)
        {
            if (Session["AdminRegister"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }
            else return RedirectToAction("Login");
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("CancelReservation")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelReservationConfirm(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("AllReserves");
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
