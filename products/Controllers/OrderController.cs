using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
            CustomerID customerIdModel = new CustomerID
            {
                CustomerNames = new[] { "DF09 - DR Distributer", "V097 - Vigneya Industries",
                    "T146 - Tropical Nortec", "U014 - Ukay Industries", "S436 - Shreeji IMPEX", "B249 - B. R. Enterprises"},
                PurchaseId = "ORD"+Guid.NewGuid().ToString().Substring(30)
            };

            ProductList productlist = new ProductList
            {
                ProductNames = new[] { 
                    new Product{ProductName="BRZ06 - Revelol 25H Tablets",MRP=83},
                    new Product{ProductName="GOYO3 - 100 Tablets",MRP=250},
                    new Product{ProductName="JMT01",MRP=67},
                    new Product{ProductName="APZ01 - Folitrax Tab 5",MRP=105},
                    new Product{ProductName="EHL01 - Solvin Nasal Spray",MRP=78},
                    new Product{ProductName="FND05 - Zerodol SP 100/15/325",MRP=122}
                }
            };

            var viewModel = new CustomerProductViewModel
            {
                Customer = customerIdModel,
                Products = productlist,
            };
            return View(viewModel);
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
            string OrderId = productList[0].OrderId;
            string CustomerId = productList[0].CustomerId;

            _context.Orders.Add(new Orders { OrderId = OrderId, CustomerId = CustomerId });
            _context.ProductEntries.AddRange(productList.Select(p => new ProductEntry
            {
                OrderId = OrderId,
                ProductName = p.ProductName,
                MRP = p.MRP,
                Qty = p.Qty,
                Total = p.Total,
                OrderDate=p.OrderDate
            }));
            _context.SaveChanges();
            // Redirect or return success
            return RedirectToAction("Index","Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}