using System.ComponentModel.DataAnnotations;

namespace products.Models
{
    public class Orders
    {
        [Key]
        public string OrderId { get; set; }

        public string CustomerId { get; set; }

        // Navigation property (optional but recommended)
        public ICollection<ProductEntry> ProductEntries { get; set; }
    }
}
