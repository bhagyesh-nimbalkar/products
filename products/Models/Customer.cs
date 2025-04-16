using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace products.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerId { get; set; }

   
        public string CustomerName { get; set; }

        public string Status { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
