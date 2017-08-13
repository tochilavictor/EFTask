using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace existingdb
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ShopModel2 dbc = new ShopModel2())
            {
                foreach (var item in dbc.Purchases)
                {
                    Console.WriteLine("Username with id " + item.PurchaseId + " bought on " + "{0:d}" + " this goods:", item.Date.Value);
                    foreach (var product in item.PurchaseProducts)
                    {
                        Console.WriteLine(product.Quantity.ToString() + " pieces of " + product.Product.Name + ", total price:" + "{0:c}", product.Quantity * product.Product.Price);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
    }
}
