using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeFirstExistingDb
{
    public static class ShopService
    {
        #region Product CRUD    
        public static void AddProduct(Product item)
        {
            if (!ValidateProduct(item)) throw new ArgumentException("Invalid product fields");
            using(ShopModel db = new ShopModel())
            {
                db.Products.Add(item);
                db.SaveChanges();
            }
        }
        public static void RemoveProduct(Product item)
        {
            if (item == null) throw new ArgumentException("element doesn't exists");
            RemoveProduct(item.ProductId);
        }
        public static void RemoveProduct(int itemid)
        {
            using (ShopModel db = new ShopModel())
            {
                Product tmp = FindProduct(itemid, db);
                List<PurchaseProduct> pptoremove = new List<PurchaseProduct>();
                foreach (var item in tmp.PurchaseProducts)
                {
                    pptoremove.Add(item);
                }
                foreach (var item in pptoremove)
                {
                    db.PurchaseProducts.Remove(item);
                }
                db.Products.Remove(tmp);
                db.SaveChanges();
            }
        }
        public static void UpdateProduct(Product itemtoupdate, Product itemupdated)
        {
            using (ShopModel db = new ShopModel())
            {
                Product tmp = FindProduct(itemtoupdate.ProductId, db);
                tmp.Name = itemupdated.Name;
                tmp.Price = itemupdated.Price;
                tmp.Image = itemtoupdate.Image;
                tmp.PurchaseProducts.Clear();
                foreach (var productPosition in itemupdated.PurchaseProducts)
                {
                    tmp.PurchaseProducts.Add(productPosition);
                }
                db.SaveChanges();
            }
        }
        public static void UpdateProduct(Product item, string newname = null, decimal? newprice = null, byte[] newimage = null, int? newcategory = null)
        {
            if (item == null) throw new ArgumentException("element doesn't exists");
            UpdateProduct(item.ProductId,newname,newprice,newimage,newcategory);
        }
        public static void UpdateProduct(int itemid, string newname=null,decimal? newprice=null,byte[] newimage = null,int? newcategory = null)
        {
            using (ShopModel db = new ShopModel())
            {
                Product tmp = FindProduct(itemid, db);
                if(newname!=null) tmp.Name = newname;
                if (newprice != null) tmp.Price = newprice;
                if (newimage != null) tmp.Image = newimage;
                if (newcategory != null) tmp.Category = newcategory;
                db.SaveChanges();
            }
        }
        #endregion
        #region Purchase CRUD
        public static void AddPurchase(Purchase purchase)
        {
            if (!ValidatePurchase(purchase)) throw new ArgumentException("invalid purchase fields");
            using (ShopModel db = new ShopModel())
            {
                db.Purchases.Add(purchase);
                foreach (var item in purchase.PurchaseProducts)
                {
                    AddProductPosition(item,db);
                }
                db.SaveChanges();
            }
        }
        public static void RemovePurchase(Product item)
        {
            if (item == null) throw new ArgumentException("element doesn't exists");
            RemovePurchase(item.ProductId);
        }
        public static void RemovePurchase(int itemid)
        {
            using (ShopModel db = new ShopModel())
            {
                Purchase tmp = FindPurchase(itemid, db);
                List<PurchaseProduct> pptoremove = new List<PurchaseProduct>();
                foreach (var item in tmp.PurchaseProducts)
                {
                    pptoremove.Add(item);
                }
                foreach (var item in pptoremove)
                {
                    db.PurchaseProducts.Remove(item);
                }
                db.Purchases.Remove(tmp);
                db.SaveChanges();
            }
        }
        public static void UpdatePurchase(Purchase itemtoupdate, Purchase itemupdated)
        {
            using (ShopModel db = new ShopModel())
            {
                Purchase tmp = FindPurchase(itemtoupdate.PurchaseId, db);
                Console.WriteLine(tmp.UserId);
                tmp.UserId = itemupdated.UserId;
                tmp.Date = itemupdated.Date;
                Console.WriteLine(tmp.UserId);
                tmp.PurchaseProducts.Clear();
                foreach (var productPosition in itemupdated.PurchaseProducts)
                {
                    tmp.PurchaseProducts.Add(productPosition);
                }
                db.SaveChanges();
            }
        }
        #endregion
        public static void SavePurchasesToXml(string filename)
        {

            using (ShopModel db = new ShopModel())
            {
                XElement Purchases = new XElement("Purchases", from c in db.Purchases.ToList()
                                                               select new XElement("Purchase",
                                                               new XElement("PurchaseID", c.PurchaseId),
                                                               new XElement("UserID", c.UserId),
                                                               new XElement("Products", from n in c.PurchaseProducts
                                                                                        select new XElement("Product",
                                                                                        new XElement("ProductId", n.ProductId),
                                                                                        new XElement("Quantity", n.Quantity)))));
                XDocument tmp = new XDocument(Purchases);
                tmp.Save(filename);
            }
        }
        private static void AddProductPosition(PurchaseProduct p,ShopModel db)
        {
            if (p == null) return;
            if (p.PurchaseId != 0) throw new ArgumentException("you can't update other purchases using this method");
            if (FindProduct(p.ProductId, db) == null) throw new ArgumentException("Unknown product in purchase");
            if (p.Quantity == null) p.Quantity = 1;
            db.PurchaseProducts.Add(p);
        }
        private static Product FindProduct(int id,ShopModel db)
        {
            Product tmp = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            if (tmp == null) throw new ArgumentException("element doesn't exists");
            return tmp;
        }
        private static Purchase FindPurchase(int id, ShopModel db)
        {
            Purchase tmp = db.Purchases.Where(x => x.PurchaseId == id).FirstOrDefault();
            if (tmp == null) throw new ArgumentException("element doesn't exists");
            return tmp;
        }
        private static bool ValidateProduct(Product p)
        {
            if (ReferenceEquals(p, null)) return false;
            else if (p.Name == null || p.Price == null || p.Category == null) return false;
            return true;
        }
        private static bool ValidatePurchase(Purchase p)
        {
            if (ReferenceEquals(p, null)) return false;
            else if (p.UserId < 1 || p.PurchaseProducts == null || p.PurchaseProducts.Count == 0) return false;
            return true;
        }
    }
}
