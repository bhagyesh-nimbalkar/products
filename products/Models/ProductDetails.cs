namespace products.Models
{
    public class ProductDetails
    {
        public string CustomerId { get; set; }
        public string PurchaseId { get; set; }

        public string ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal MRP { get; set; }
        public decimal Qty { get; set; }
        public decimal Total { get; set; }
        public DateOnly OrderDate { get; set; }
    }
}
