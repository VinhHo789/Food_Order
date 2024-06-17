using Lab05.Models;
using Lab05.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Lab05.Controllers
{
    // CustomerController.cs
    public class CustomerController : Controller
    {
        private readonly foodContext _dbContext;

        public CustomerController(foodContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {

            // Fetch or create your model
            var customers = _dbContext.Khachhangs
                .Select(c => new CustomerViewModel
                {
                    MaKH = c.Makh,
                    TenKH = $"{c.Ho} {c.Ten}"
                })
                .ToList();

            return View("Index", customers);
        }


        [HttpGet]
        public IActionResult GetCustomerOrders(int id)
        {
            var customer = _dbContext.Khachhangs.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            var orders = _dbContext.Hoadons
                .Where(h => h.Makh == customer.Makh)
                .AsEnumerable()  // Materialize the query before projection
                .Select((h, index) => new CustomerOrders
                {
                    STT = index + 1,
                    MaHD = h.Mahd,
                    NgayHD = h.Ngayhd
                })
                .ToList();

            var customerViewModel = new CustomerViewModel
            {
                MaKH = customer.Makh,
                TenKH = $"{customer.Ho} {customer.Ten}",
                DanhSachDonHang = orders
            };

            return PartialView("CustomerDetails", customerViewModel);
        }

        [HttpGet]
        [Route("Customer/GetOrderDetails/{maHD}")]
        public IActionResult GetOrderDetails(int maHD)
        {
            var hoadon = _dbContext.Hoadons
                .Include(h => h.Cthds)
                .ThenInclude(c => c.MamaNavigation)
                .FirstOrDefault(h => h.Mahd == maHD);



            if (hoadon == null )
            {
                return NotFound();
            }

            // Map Hoadon details to OrderDetailsViewModel
            var orderDetailsViewModels = hoadon.Cthds.Select((c, index) => new OrderDetailsViewModel
            {
                STT = index + 1,
                TenMa = c.MamaNavigation.Tenma,
                SL = c.SL,
                Donggia = c.MamaNavigation.Donggia,
                ThanhTien = c.SL * c.MamaNavigation.Donggia
            }).ToList();

           

            // Assuming you have PartialViewResult for Hoadon details

            // Assuming you have PartialViewResult for Order details
            return PartialView("OrderDetail", orderDetailsViewModels);
        }


        [HttpGet]
        [Route("Customer/GetCustomerDetails/{maHD}")]
        public IActionResult GetCustomerOrderDetails(int maHD)
        {
            var hoadon = _dbContext.Hoadons
                .Include(h => h.Cthds)
                .ThenInclude(c => c.MamaNavigation)
                .FirstOrDefault(h => h.Mahd == maHD);

            var customer = _dbContext.Khachhangs
                .FirstOrDefault(c => c.Makh == hoadon.Makh);

            if (hoadon == null || customer == null)
            {
                return NotFound();
            }

            // Map Hoadon details to OrderDetailsViewModel
            

            var customerDetailsViewModel = new CustomerDetailsViewModel
            {
                MaKH = customer.Makh,
                TenKH = $"{customer.Ho} {customer.Ten}",
                DiaChi = customer.Diachi,
                Sdt = customer.Sdt,
                MaHD = hoadon.Mahd,
                NgayHD = hoadon.Ngayhd,
            };

            // Assuming you have PartialViewResult for Hoadon details
            return PartialView("OrderInfo", customerDetailsViewModel);


        }




        [HttpPost]
        [Route("Customer/DeleteOrder/{maHD}")]
        public IActionResult DeleteOrder(int maHD)
        {
            // Assuming you have a method to delete Hoadon and Cthd records based on maHD
            // Implement the deletion logic as needed in your application

            // For example:
            var hoadon = _dbContext.Hoadons.Find(maHD);
            if (hoadon != null)
            {
                // Delete associated Cthd records
                var cthds = _dbContext.Cthds.Where(c => c.Mahd == maHD);
                _dbContext.Cthds.RemoveRange(cthds);

                _dbContext.SaveChanges();

                // Delete Hoadon record
                _dbContext.Hoadons.Remove(hoadon);

                _dbContext.SaveChanges();


            }

            return Ok(); // Return a success status
        }


    }

}
