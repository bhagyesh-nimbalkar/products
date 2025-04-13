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
           List<Orders> orders = _context.Orders.ToList();
            List<SalesOrder> saleOrder = [];
            foreach(var obj in orders)
            {
                List<ProductEntry> list = _context.ProductEntries.Where(p => p.OrderId == obj.OrderId).ToList();
                DateOnly Date = list[0].OrderDate;
                decimal NetVal = list.Sum(x => x.Total);
                saleOrder.Add(new SalesOrder
                {
                    OrderId = obj.OrderId,
                    CustomerId = obj.CustomerId,
                    Total = NetVal,
                    OrderDate = Date,
                });
            }
            return View(saleOrder);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
