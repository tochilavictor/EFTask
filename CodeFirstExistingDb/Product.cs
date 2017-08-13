namespace CodeFirstExistingDb
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            PurchaseProducts = new HashSet<PurchaseProduct>();
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public int? Category { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public byte[] Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; }
    }
}
