using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using products.Models; // Import this for Orders class

public class ProductEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey("Order")]
    public string OrderId { get; set; }

    public DateOnly OrderDate { get; set; }
    public string ProductName { get; set; }
    public decimal MRP { get; set; }
    public int Qty { get; set; }
    public decimal Total { get; set; }

    // Navigation property
    public Orders Order { get; set; }
}
