namespace products.Models
{
    public class SalesOrder
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }

        public decimal Total { get; set; }
    }
}
