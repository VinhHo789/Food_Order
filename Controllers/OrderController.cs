using Lab05.Models;
using Lab05.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Lab05.Controllers
{
    public class OrderController : Controller
    {
        private readonly foodContext _context;

        double totalMoney = 0;
        int maHD = 0;


        public OrderController(foodContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            var userName = TempData["TenKH"]?.ToString();

            // Pass the username to the view
            ViewBag.UserName = userName;

            // Retrieve menu items from the database
            var menuItems = _context.Monans.Select(item => new OrderViewModel
            {
                MAMA = item.Mama,
                TENMA = item.Tenma,
                DONGGIA = item.Donggia,
                LOAIMA = item.Loaima,
                NOTE = "",
                MAK = "",
                SL = 0 // Default quantity is set to 0
            }).ToList();

            return View(menuItems);
        }

        [HttpPost]
        public IActionResult Order(List<OrderViewModel> orderItems)
        {
            var invoice = new Hoadon
            {
                
                // You need to generate a unique ID for the invoice
                Makh = (int)TempData["MaKH"], // Replace with the actual customer ID
                Tht = totalMoney, // You might need to calculate the total based on the items in CTHD
                Ngayhd = DateTime.Now
            };

            _context.Hoadons.Add(invoice);

            _context.SaveChanges();


            foreach (var item in orderItems)
            {
                if (item.SL > 0)
                {
                    totalMoney = totalMoney + (item.SL * item.DONGGIA);
                    // Create an entry in the CTHD table for each item with a quantity greater than 0
                    var invoiceDetail = new Cthd
                    {
                        Mahd = invoice.Mahd,// You need to generate a unique ID for the invoice
                        Mama = item.MAMA,
                        Mak = item.MAK,
                        SL = item.SL,
                        
                    };
                    

                    _context.Cthds.Add(invoiceDetail);
                }
            }

            // Create an entry in the HOADON table
            

            // Save changes to the database
            _context.SaveChanges();

            invoice.Tht = totalMoney;

            _context.SaveChanges();

            // Redirect to a confirmation page or home page
            return RedirectToAction("PlaceOrder");
        }
    }

}
