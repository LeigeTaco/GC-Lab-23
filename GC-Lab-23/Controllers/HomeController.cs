using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GC_Lab_23.Models;

namespace GC_Lab_23.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult NewItem()
        {

            return View();
        }

        public ActionResult SaveNewItem(Item newItem)
        {
            GCWebORM ORM = new GCWebORM();

            ORM.Items.Add(newItem);
            ORM.SaveChanges();

            return View();
        }

        public ActionResult DeleteItem(int itemID)
        {
            GCWebORM ORM = new GCWebORM();
            Item itemToDelete = ORM.Items.Find(itemID);
            ORM.Items.Remove(itemToDelete);
            try
            {
                ORM.SaveChanges();
            }
            catch (Exception e)
            {
                ViewBag.ServerException = e;
                return View("Error");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Error()
        {

            return View();
        }

        public ActionResult UpdateItem(int itemID)
        {
            GCWebORM ORM = new GCWebORM();
            Item itemToEdit = ORM.Items.Find(itemID);
            ViewBag.ItemToEdit = itemToEdit;

            return View();
        }

        public ActionResult SaveUpdatedItem(Item updatedItem)
        {
            GCWebORM ORM = new GCWebORM();
            Item oldItem = ORM.Items.Find(updatedItem.ItemID);

            oldItem.Name = updatedItem.Name;
            oldItem.Description = updatedItem.Description;
            oldItem.Stock = updatedItem.Stock;
            oldItem.Price = updatedItem.Price;

            ORM.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {

            return View();
        }

        public ActionResult AddNewUser(string Username, string Password, string Email, string Phone)
        {
            GCWebORM ORM = new GCWebORM();
            bool isAdmin = false;
            Guid UserID = Guid.NewGuid();
            ORM.Users.Add(new User(UserID, Username, Password, Email, Phone, isAdmin));
            ORM.SaveChanges();

            return View("Index");
        }
    }
}