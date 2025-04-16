using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace products.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; } // Navigation property

        public string PurchaseOrderNo { get; set; }

        public DateOnly PurchaseOrderDate { get; set; }

        public string Status { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
