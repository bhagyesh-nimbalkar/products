namespace products.Models
{
    public class SalesOrder
    {
        public Guid Orderid { get; set; }

        public string CustomerName { get; set; }

        public DateOnly OrderDate { get; set; }

        public decimal NetValue { get; set; }
    }
}
