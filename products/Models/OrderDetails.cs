namespace products.Models
{
    public class OrderDetails:CustomerProductCombinedView
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string PurchaseOrderNo { get; set; }

        public DateOnly OrderDate { get; set; }

        public List<OrderItem> OrderItemList { get; set; }
    }
}
