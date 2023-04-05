using DrinksSale.DataAccess;
using DrinksSale.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrinksSale.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new DatabaseContext())
            {
                var model = new UserModelView();

                model.Coins = db.Coins.ToList();
                model.Products = db.Products.ToList();

                var condition = db.Condition.FirstOrDefault();
                if (condition == null)
                {
                    var newCondition = new ConditionModel()
                    {
                        Money = 0,
                        Coin1 = 0,
                        Coin2 = 0,
                        Coin5 = 0,
                        Coin10 = 0
                    };

                    db.Condition.Add(newCondition);
                    db.SaveChanges();

                    condition = newCondition;
                }

                model.Condition = condition;

                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Condition(int money)
        {
            using(var db = new DatabaseContext())
            {
                var condition = db.Condition.FirstOrDefault();

                condition!.Money += money;

                switch (money)
                {
                    case 1: condition.Coin1++; break;
                    case 2: condition.Coin2++; break;
                    case 5: condition.Coin5++; break;
                    case 10: condition.Coin10++; break;
                    default: break;
                }

                db.SaveChanges();
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Buy(int id, int money)
        {
            using (var db = new DatabaseContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);

                if (money < product!.Price)
                {
                    return Json("Недостаточно средств для покупки напитка");
                }

                if (product.Amount == 0)
                {
                    return Json("Напиток закончился");
                }

                if (product != null && product.Amount > 0)
                {
                    var condition = db.Condition.FirstOrDefault();
                    var newSale = new SaleModel()
                    {
                        ConditionId = condition!.Id,
                        ProductId = product.Id,
                        Amount = 1,
                        Revenue = product.Price,
                        CreateDate = DateTime.Now
                    };

                    db.Sales.Add(newSale); //Добавляем продажу
                    product.Amount--; //Уменьшаем количество товара
                    condition.Money -= product.Price; //Текущие деньги в автомате уменьшаются

                    db.SaveChanges();
                }

            }
            return Json("");
        }

        [HttpPost]
        public JsonResult Cashback(int money)
        {
            return Json(backCoins(money));
        }

        private Dictionary<int, int> backCoins(int money)
        {
            var result = new Dictionary<int, int>();

            using (var db = new DatabaseContext())
            {
                var condition = db.Condition.FirstOrDefault();

                Dictionary<int, int> currentCount= new Dictionary<int, int>();

                currentCount.Add(10, condition!.Coin10);
                currentCount.Add(5, condition.Coin5);
                currentCount.Add(2, condition.Coin2);
                currentCount.Add(1, condition.Coin1);

                var balance = money;
                var count = 0;
                var newBalance = 0;

                var dens = new List<int>{ 10, 5, 2, 1 };

                foreach (var item in dens)
                {
                    newBalance = calc(balance, item, currentCount[item]);
                    count = (balance - newBalance) / item;
                    result.Add(item, count);
                    balance = newBalance;
                }

                condition!.Coin10 -= result[10];
                condition!.Coin5 -= result[5];
                condition!.Coin2 -= result[2];
                condition!.Coin1 -= result[1];

                condition.Money -= 10 * result[10];
                condition.Money -= 5 * result[5];
                condition.Money -= 2 * result[2];
                condition.Money -= 1 * result[1];

                db.SaveChanges();
            }

            return result;
        }

        private int calc(int balance, int denomination, int count)
        {
            var needCount = balance / denomination;
            if (count >= needCount)
            {
                balance -= needCount * denomination;
            }
            else
            {
                balance -= count * denomination;
            }
            return balance;
        }
    }
}
