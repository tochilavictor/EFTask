using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeFirstNewDb
{
    public enum ProductCategory
    {
        groceries = 1,
        sportgoods = 2,
        kitchengoods = 3,
        goodsforanimals = 4,
        goodsforbabies = 5,
        others = 6
    }
    public class Product
    {
        public Product()
        {
            Purchases = new HashSet<ProductPosition>();
        }
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Price { get; set; }
        public byte[] Image { get; set; }
        public virtual ICollection<ProductPosition> Purchases { get; set; }
    }
}
