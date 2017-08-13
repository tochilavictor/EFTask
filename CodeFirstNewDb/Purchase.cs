using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace CodeFirstNewDb
{
    public class Purchase
    {
        public Purchase()
        {
            Products = new HashSet<ProductPosition>();
        }
        public int PurchaseId { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime Date { get; set; }
       public virtual ICollection<ProductPosition> Products { get; set; }
    }
}
