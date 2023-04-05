using DrinksSale.DataAccess;
using DrinksSale.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinksSale.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index(string key)
        {
            if (key == "qwerty")
            {
                using (var db = new DatabaseContext())
                {
                    var model = new AdminModelView();

                    model.Coins = db.Coins.ToList();
                    model.Products = db.Products.ToList();

                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }

        [HttpPost]
        public IActionResult AddProduct(AdminModelView model)
        {
            using (var db = new DatabaseContext())
            {
                var newProduct = model.Product;

                db.Products.Add(newProduct);
                db.SaveChanges();

                return RedirectToAction("Index", new { key = "qwerty" });
            }
        }

        [HttpPost]
        public JsonResult EditCoin(int id, bool active)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var coin = db.Coins.FirstOrDefault(p => p.Id == id);

                    coin!.IsActive = active;

                    db.SaveChanges();
                }

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult EditProduct(int id, int? price, int? amount)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var product = db.Products.FirstOrDefault(p => p.Id == id);

                    product!.Price = price != null ? price.Value : product.Price;
                    product.Amount = amount != null ? amount.Value : product.Amount;

                    db.SaveChanges();

                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var product = db.Products.FirstOrDefault(p => p.Id == id);

                    db.Products.Remove(product!);
                    db.SaveChanges();

                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
