using Resto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Resto.Models;
using System.Data.Entity.Migrations;
using System.Net;

namespace Resto.Controllers
{
    public class HomeController : Controller
    {
        private AppDBContext db = new AppDBContext();

        public ActionResult Index() 
        {
            // return all categories ..
            var cats = db.Categories.ToList();
            return View(cats);
        }

        // GET: /CatDetails/[1]
        public ActionResult CatDetails(int? id)
        {
            if (id != null)
            {
                // for view
                ViewBag.CustomerId = Session["CustomerId"];
                // get category name for view
                ViewBag.CatName = db.Categories.Find(id).Name;
                // return food by category ID
                var food = db.Food.SqlQuery("SELECT * FROM Foods WHERE CategoryId = " + id);
                return View(food.ToList());
            }
            else { return HttpNotFound(); }
        }

        // GET: /AddToCart
        public ActionResult AddToCart(int foodId) 
        {
            if (Session["CustomerId"] != null)
            {
                int cstmrID = (int)Session["CustomerId"];
                // get food by Id ..
                var food = db.Food.Find(foodId);
                // add food to cart 
                CartItem cartItem = new CartItem
                {
                    Name = food.Name,
                    Price = food.Price,
                    Quantity = 1,
                    CustomerId = cstmrID,
                    FoodId = foodId
                };
                // check if item already exist ..
                var existedItem = db.CartItems
                    .SqlQuery("SELECT * FROM CartItems WHERE FoodId = " + foodId).FirstOrDefault();
                if (existedItem == null)
                {
                    db.CartItems.Add(cartItem);
                    db.SaveChanges();
                }
                // return nothing
                return new HttpStatusCodeResult(204);
            }
            else return RedirectToAction("Login");
        }

        // GET: Home/Reserve
        public ActionResult Reserve()
        {
            return View();
        }

        // GET: /Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // POST: /Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return View("ConfirmFeedback");
            }

            return View(feedback);
        }

        // GET: /MyProfile
        public ActionResult MyProfile()
        {
            if (Session["CustomerId"] != null)
            {
                var id = Session["CustomerId"];
                var customer = db.Customers.Find(id);
                return View(customer);
            }
            else return RedirectToAction("Login");
        }


        // ***** 🔒 Registration ***** //
        // ************************* //
        // GET: Customers/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                // validate customer
                if (ValidateRegister(customer))
                {
                    // encrypt password fisrt
                    string hashedPassword = EncryptPassword(customer.Password);
                    customer.Password = hashedPassword;

                    // save customer in DB ..
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    // save customer id for later 
                    Session["CustomerId"] = customer.Id;
                    Session["CustomerName"] = customer.Name;
                    return Redirect("~/Home/Index");
                }
            }
            return View(customer);
        }

        // GET: Customers/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Customers/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLogin customer)
        {
            if (ModelState.IsValid)
            {
                if (ValidateLogin(customer))
                {
                    // get saved customer name ..
                    var savedCustomer = db.Customers
                        .Where(c => c.Email.Equals(customer.Email)).FirstOrDefault();
                    var customerName = savedCustomer.Name;
                    // save customer id & name ..
                    Session["CustomerId"] = savedCustomer.Id;
                    Session["CustomerName"] = savedCustomer.Name;
                    return Redirect("~/Home/Index");
                }

            }
            return View(customer);
        }

        // GET: /Logout
        public ActionResult Logout()
        {
            return View();
        }

        // POST: /Logout
        [HttpPost]
        [ActionName("Logout")]
        public ActionResult LogoutConfirm()
        {
            Session.Clear();
            return Redirect("~/Home/Index");
        }



        // ******** ✅ Validaion section ****** //
        private string EncryptPassword(string password)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    password = Convert.ToBase64String(ms.ToArray());
                }
            }
            return password;

        }

        private string DecryptPassword(string password)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(password);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    password = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return password;
        }

        private bool ValidateRegister(Customer customer)
        {
            // get saved email ..
            var savedEmail = db.Customers
                .Where(c => c.Email.Equals(customer.Email)).FirstOrDefault();

            if (!Regex.IsMatch(customer.Password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,12}$"))
            {
                ViewBag.ErrorPass = "Password must be from [6 - 12] characters, " +
                    "and must contain at least One small character, " +
                    "One Capital character and One Digit number. ";
                return false;
            }
            else if (!Regex.IsMatch(customer.Name, @"[a-zA-Z\s\-*]+"))
            {
                ViewBag.ErrorName = "Invalid Customer Name!";
                return false;
            }
            else if (!Regex.IsMatch(customer.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                ViewBag.ErrorEmail = "Invalid Email Address!";
                return false;
            }
            else if (!Regex.IsMatch(customer.Phone, @"[0-9]{11}"))
            {
                ViewBag.ErrorNumber = "Invalid Phone Number!";
                return false;
            }
            // when client email is exist ..
            else if (savedEmail != null)
            {
                ViewBag.ErrorEmail = "This Email is already exist!";
                return false;
            }
            else return true;
        }

        private bool ValidateLogin(CustomerLogin customer)
        {
            // get saved email ..
            var savedCustomer = db.Customers
                .Where(c => c.Email.Equals(customer.Email)).FirstOrDefault();

            if (!Regex.IsMatch(customer.Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                ViewBag.ErrorLoginEmail = "Invalid Email Address!";
                return false;
            }
            // when client email is not exist ..
            else if (savedCustomer == null)
            {
                ViewBag.ErrorLoginEmail = "This Account is Not Found, Please Create new One";
                return false;
            }
            // when client exist, check password ..
            else if (savedCustomer != null)
            {
                // check hashed password ..
                var savedPassword = DecryptPassword(savedCustomer.Password);
                if (!savedPassword.Equals(customer.Password))
                {
                    ViewBag.ErrorLoginPass = "Error Email or Password!";
                    return false;
                }
                else return true;

            }
            else return true;
        }
    }
}