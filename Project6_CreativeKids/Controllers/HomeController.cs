using Project6_CreativeKids.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project6_CreativeKids.Controllers
{
    public class HomeController : Controller
    {
        private KidsStoreEntities db = new KidsStoreEntities();
        protected void Alert(string message, string title = "Alert", string type = "warning")
        {
            // Create the SweetAlert script
            var script = $"<script>swal('{title}', '{message}', '{type}');</script>";
            TempData["Alert"] = script;
        }
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,username,email,password")] User_Info user_Info, string userPassword)
        {
            Alert("Your form has been submitted successfully!", "Success", "success");
            if (ModelState.IsValid)
            {
                if (userPassword != user_Info.password)
                {
                    ModelState.AddModelError("", "Passwords do NOT match");
                    return View();


                }
                var isExist = IsEmailExist(user_Info.email);
                if (isExist)
                {
                    ModelState.AddModelError("", "Email already Exist");
                    return View();
                }

                db.User_Info.Add(user_Info);
                    db.SaveChanges();
                

                return RedirectToAction("Index");
            }

            return View(user_Info);
        }
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            
                var v = db.User_Info.Where(a => a.email == emailID).FirstOrDefault();
                return v != null;
            
        }
        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User_Info user_Info)
        {
            if (ModelState.IsValid)
            {
                var obj = db.User_Info.Where(a => a.email.Equals(user_Info.email) && a.password.Equals(user_Info.password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.ID.ToString();
                    Session["UserName"] = obj.username.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }

            }

            return View(user_Info);
        }
        public ActionResult Shop(int? id)
        {
            if (id == null)
            {
                var det2 = (from d in db.Products
                           select d).ToList();
                return View(det2);
            }
            else
            {
                var det = (from d in db.Products
                           where d.cat_ID == id
                           select d).ToList();
                return View(det);
            }

        }
        //[HttpPost]
        //public ActionResult Shop(int? id)
        //{
        //    var det = (from d in db.Products
        //               where d.cat_ID == id
        //               select d).ToList();
        //    return View(det);
        //}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult Cart(int?id)
        {
            
           
         

            var allItemsInCart = db.Carts.Where(x => x.user_ID == id).ToList();
           
           

            return View(allItemsInCart);

            
        }

        public ActionResult Add(int id)
        {
            addToCart(id);
            return RedirectToAction("Index");

        }
        private void addToCart(int pId)
        {
            
            Product product = db.Products.FirstOrDefault(p => p.ID == pId);
            if (product != null)
            {
                var uId = Convert.ToInt32(Session["UserID"]);
               
                Cart cart = db.Carts.FirstOrDefault(c => c.user_ID == uId && c.product_ID == pId);
                if (cart != null)
                {
                  
                    cart.quantity++;
                }
                else
                {

                    cart = new Cart
                    {
                        product_ID = product.ID,
                        productName = product.name,
                        price = product.price,
                        user_ID = uId,
                        quantity = 1
                    };

                    db.Carts.Add(cart);
                }

               
                db.SaveChanges();
            }
        }

        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Info user_Info = db.User_Info.Find(id);
            if (user_Info == null)
            {
                return HttpNotFound();
            }
            return View(user_Info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile([Bind(Include = "ID,username,email")] User_Info user_Info, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //User_Info myuser = db.User_Info.Find(Session["UserID"]);
                //if (user_Info.username != null)
                //{
                //    myuser.username = user_Info.username;
                //}
                //if (user_Info.email != null)
                //{
                //    myuser.email = user_Info.email;
                //}

               
                db.Entry(user_Info).State = EntityState.Modified;
                var uploadPath = Server.MapPath("~/Content/Uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                if (upload != null && upload.ContentLength > 0)
                {
                    var listLogoFileName = Path.GetFileName(upload.FileName);
                    var listLogoPath = Path.Combine(uploadPath, listLogoFileName);
                    upload.SaveAs(listLogoPath);
                    user_Info.image = listLogoFileName;
                }
               

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_Info);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(User_Info user_Info,string newPass, string confPass,string Pass) 
        {

            if (ModelState.IsValid)
            {
                if (user_Info.password == Pass)
                {
                    if (confPass == newPass)
                    {user_Info.password = confPass;
                        db.Entry(user_Info).State = EntityState.Modified;

                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }

                }

                
            }
            return View("Profile");
        }
        [HttpPost]
        public ActionResult RemoveFromCart(int productId)
        {
            removeFromCart(productId);
            return RedirectToAction("Index");
        }
        public void removeFromCart(int productId)
        {
            var cartItem = db.Carts.FirstOrDefault(c => c.product_ID == productId);
            if (cartItem != null)
            {
                db.Carts.Remove(cartItem);
                db.SaveChanges();
            }
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int productID, int quantity)
        {
            // Assuming you have a method to update the quantity in the cart
            UpdateCart(productID, quantity);

            // Redirect back to the cart page or wherever you want
            return RedirectToAction("Index");
        }

        private void UpdateCart(int? productID, int quantity)
        {
            // Logic to update the cart quantity
            var cart = GetCartFromSessionOrDatabase();
            var item = cart.FirstOrDefault(c => c.product_ID == productID);

            if (item != null)
            {
                item.quantity = quantity;
            }

            // Save the cart back to session or database
            SaveCart(cart);
        }
        private List<Cart> GetCartFromDatabase(int userId)
        {
            // Example code, adjust according to your data access layer
          
                return db.Carts.Where(c => c.user_ID == userId).ToList();
           
        }
        private void SaveCart(List<Cart> cart)
        {
            foreach (var item in cart)
            {
                
                var existingItem = db.Carts.FirstOrDefault(c => c.product_ID == item.product_ID);
                if (existingItem != null)
                {
                   
                    existingItem.quantity = item.quantity;
                }
                else
                {
                   
                    db.Carts.Add(item);
                }
            }
           
            db.SaveChanges();
        }

        private List<Cart> GetCartFromSessionOrDatabase()
        {
            int userId = Convert.ToInt32(Session["UserID"]); // Method to retrieve the current user's ID
            return GetCartFromDatabase(userId);
        }







        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            // Clear the authentication cookie
            FormsAuthentication.SignOut();

            // Optionally, you can clear the session if you're using it
            Session.Clear();
            Session.Abandon();

            // Redirect to the home page or login page
            return RedirectToAction("Index", "Home"); // or RedirectToAction("Login", "Account");
        }


    }
}