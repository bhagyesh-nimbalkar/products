namespace products.Models
{
    public class ProductDetails
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal MRP { get; set; }
        public int Qty { get; set; }
        public decimal Total { get; set; }
    }
}
