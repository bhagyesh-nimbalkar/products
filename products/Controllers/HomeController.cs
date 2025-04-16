using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using products.Data;
using products.Models;

namespace products.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Order> orders = _context.Orders.ToList();
            List<SalesOrder> saleslist = [];
            foreach(var obj in orders)
            {
                string customerName = _context.Customers
                    .Where(c => c.CustomerId == obj.CustomerId)
                    .Select(c => c.CustomerName)
                    .FirstOrDefault();
                decimal TotalNetValue = _context.OrderItems
                    .Where(oi => oi.OrderId == obj.OrderId)
                    .Sum(oi => oi.Total);


                saleslist.Add(new SalesOrder
                {
                    Orderid = obj.OrderId,
                    CustomerName = customerName,
                    OrderDate = obj.PurchaseOrderDate,
                    NetValue = TotalNetValue
                });
            }
            return View(saleslist);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
