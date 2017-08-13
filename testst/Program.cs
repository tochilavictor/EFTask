using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstExistingDb;

namespace testst
{
    class Program
    {
        static void Main(string[] args)
        {
            ShopService.SavePurchasesToXml("D:\\1.xml");
            //Purchase p2 = new Purchase()
            //{
            //    Date = DateTime.Now,
            //    UserId = 322,
            //    PurchaseProducts = new PurchaseProduct[] { new PurchaseProduct { ProductId = 9, Quantity = 12 } }
            //};
            //Purchase p1 = new Purchase { PurchaseId = 6 };
            //ShopService.UpdatePurchase(p1, p2);
            //Product tmp = new Product { Name = "broiler",Price=0,Category=1 };
            //Purchase p1 = new Purchase { Date = DateTime.Now, UserId = 22,
            //    PurchaseProducts = new PurchaseProduct[] { new PurchaseProduct { ProductId = 10 } , new PurchaseProduct {ProductId = 2,Quantity=2 } } };
            //ShopService.AddPurchase(p1);
            using (CodeFirstExistingDb.ShopModel dbc = new CodeFirstExistingDb.ShopModel())
            {
                //DbFirst.Product temp = new DbFirst.Product() { Category = DbFirst.ProductCategory.groceries, ProductId = 3 };
                //List<DbFirst.Product> a = dbc.Products.ToList<DbFirst.Product>();
                //if (a.Contains(temp)) Console.WriteLine("yes");
                //else Console.WriteLine("no");
                foreach (var item in dbc.Purchases)
                {
                    Console.WriteLine("Username with id " + item.UserId + " bought on " + "{0:d}" + " this goods:", item.Date.Value);
                    foreach (var product in item.PurchaseProducts)
                    {
                        Console.WriteLine(product.Quantity.ToString() + " " + product.Product.Category
                            + " pieces of " + product.Product.Name + ", total price:" + "{0:c}", product.Quantity * product.Product.Price);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
