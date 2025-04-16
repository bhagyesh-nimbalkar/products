using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using products.Data;
using products.Models;

namespace products.Controllers
{
    [Route("Home")]
    public class OrderUpdateController:Controller
    {
        private readonly AppDbContext _context;

        public OrderUpdateController(AppDbContext context)
        {
            _context = context;
        }

        [Route("OrderUpdate/{id}")]
        public IActionResult Index(string id)
        {
            Guid orderId = Guid.Parse(id);
            Order orderDetails = _context.Orders.FirstOrDefault(p => p.OrderId == orderId);
            Customer customerDetails = _context.Customers.FirstOrDefault(p => p.CustomerId == orderDetails.CustomerId);
            List < OrderItem > orderitemDetails = _context.OrderItems.Where(p => p.OrderId == orderDetails.OrderId).ToList();

            List<Customer> customerlist = _context.Customers.ToList();
            List<Product> productlist = _context.Products.ToList();

            OrderDetails orderview = new OrderDetails
            {
                OrderId =orderId,
                CustomerName = customerDetails.CustomerName,
                PurchaseOrderNo = orderDetails.PurchaseOrderNo,
                OrderDate = orderDetails.PurchaseOrderDate,
                OrderItemList = orderitemDetails,
                customerlist = customerlist,
                productlist = productlist
            };
            return View(orderview);
        }

        [HttpPost("update")]
        public IActionResult UpdateOrderItem(string OrderItemId, string OrderId,int Qty,int MRP)
        {
            if (string.IsNullOrEmpty(OrderItemId))
            {
                return BadRequest("No products received.");
            }
            Guid Id = Guid.Parse(OrderItemId);
            OrderItem item = _context.OrderItems.Where(p => p.OrderItemId == Id).FirstOrDefault();
            item.Qty = Qty;
            item.Total = MRP * Qty;
            _context.OrderItems.Update(item);
            _context.SaveChanges();
            return RedirectToAction("OrderUpdate", "Home", new { id = OrderId });
        }

        [HttpPost("delete")]
        public IActionResult DeleteOrderItem(string OrderItemId,string OrderId)
        {
            if (string.IsNullOrEmpty(OrderItemId))
            {
                return BadRequest("No products received.");
            }
            Guid Id = Guid.Parse(OrderItemId);
            OrderItem item = _context.OrderItems.Where(p => p.OrderItemId == Id).FirstOrDefault();
            _context.OrderItems.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("OrderUpdate", "Home", new { id = OrderId });
        }

        [HttpPost("submit")]
        public IActionResult SubmitOrder(string ProductListJson,string OrderId)
        {
            if (string.IsNullOrEmpty(ProductListJson))
            {
                return BadRequest("No products received.");
            }

            var productList = JsonSerializer.Deserialize<List<ProductDetails>>(ProductListJson);


            if (productList == null) return BadRequest("No products received.");
            string CustomerId = productList[0].CustomerId;
            string PurchaseId = productList[0].PurchaseId;
            DateOnly orderDate = DateOnly.Parse(productList[0].OrderDate.ToString());

            Order existingOrder = _context.Orders.FirstOrDefault(p => p.OrderId == Guid.Parse(OrderId));

            if (existingOrder != null)
            {
                existingOrder.PurchaseOrderDate = orderDate;
                existingOrder.CustomerId = Guid.Parse(CustomerId);
                existingOrder.PurchaseOrderNo = PurchaseId;

                _context.Orders.Update(existingOrder);
            }
            _context.OrderItems.AddRange(productList.Select(p => new OrderItem
            {
                OrderId =Guid.Parse(OrderId),
                ProductId = Guid.Parse(p.ProductId),
                Qty = p.Qty,
                Total = p.Total,
                Status = "Active"
            }));
            _context.SaveChanges();
            //// Redirect or return success
            return RedirectToAction("Index", "Home");
        }
    }
}
