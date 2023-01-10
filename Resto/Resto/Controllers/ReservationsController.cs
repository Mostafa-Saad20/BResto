using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Resto.Models;

namespace Resto.Controllers
{
    public class ReservationsController : Controller
    {
        private AppDBContext db = new AppDBContext();

        // GET: Reservations
        public ActionResult Index()
        {
             return View(db.Reservations.ToList());
         
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
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

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (Session["CustomerId"] != null)
            {
                return View();
            }
            else return Redirect("~/Home/Login");
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                if (ValidateReservation(reservation)) 
                {
                    // save reservation
                    reservation.CustomerId = (int)Session["CustomerId"];
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                    return View("ConfirmReservation");
                }
            }

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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

        // ****** Validate Reservation *********
        private bool ValidateReservation(Reservation reservation) 
        {
            if (!Regex.IsMatch(reservation.Name, @"[a-zA-Z\s\-*]+"))
            {
                ViewBag.ErrorName = "Invalid Name!";
                return false;
            }
            else if (!Regex.IsMatch(reservation.Phone, @"[0-9]") || reservation.Phone.Length != 11)
            {
                ViewBag.ErrorNumber = "Invalid Phone Number!";
                return false;
            }
            else if (reservation.Date.CompareTo(DateTime.Today) < 0) {
                ViewBag.ErrorDate = "Invalid Date!";
                return false;
            }
            else if (reservation.Time.CompareTo(DateTime.Today.TimeOfDay) <= 0)
            {
                ViewBag.ErrorTime = "Invalid Time!";
                return false;
            }
            else return true;
        }
    }
}