using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeFirstNewDb
{
    public class ProductPosition
    {
       
        [Key]
        [Column(Order = 1)]
        public int PurchaseId{ get; set; }
        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        public Purchase Purchase { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
