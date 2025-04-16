using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using products.Data;
using products.Models;

namespace products.Controllers
{
    public class OrderController: Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Customer> customerlist = _context.Customers.ToList();
            List<Product> productlist = _context.Products.ToList();

            CustomerProductCombinedView combinedView = new CustomerProductCombinedView
            {
                customerlist = customerlist,
                productlist = productlist
            };
            return View(combinedView);
        }

        [HttpPost]
        public IActionResult SubmitOrder(string ProductListJson)
        {
            if (string.IsNullOrEmpty(ProductListJson))
            {
                return BadRequest("No products received.");
            }

            var productList = JsonSerializer.Deserialize<List<ProductDetails>>(ProductListJson);


            if (productList==null) return BadRequest("No products received.");
            string CustomerId = productList[0].CustomerId;
            string PurchaseId = productList[0].PurchaseId;
            DateOnly orderDate = DateOnly.Parse(productList[0].OrderDate.ToString());

            EntityEntry<Order> result = _context.Orders.Add(new Order
            {
                CustomerId = Guid.Parse(CustomerId),
                PurchaseOrderNo = PurchaseId,
                PurchaseOrderDate = orderDate,
                Status="Active",
            });

            _context.OrderItems.AddRange(productList.Select(p => new OrderItem
            {
                OrderId = result.Entity.OrderId,
                ProductId = Guid.Parse(p.ProductId),
                Qty = p.Qty,
                Total = p.Total,
                Status="Active"
            }));
            _context.SaveChanges();
            //// Redirect or return success
            return RedirectToAction("Index","Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}