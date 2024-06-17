using Lab05.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Lab05.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Lab05.Controllers
{
    public class LoginController : Controller
    {
        public static int MaKHDangLogin = 0;
        private readonly foodContext _context;
        
        public LoginController(foodContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
            TempData["TenKH"] = "Bạn chưa đăng nhập, hệ thống sẽ không ghi nhận";
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {

            var account = _context.Accounts
            .Include(a => a.MakhNavigation) // Include related Khachhang data
            .FirstOrDefault(a => a.Username == username && a.Password == password);

            if (account != null)
            {

                TempData["UserName"] = account.Username;
                TempData["MaKH"] = account.MakhNavigation.Makh; // Assuming there's a Khachhang navigation property
                TempData["TenKH"] = $"{account.MakhNavigation.Ho} {account.MakhNavigation.Ten}"; // Adjust properties based on your Khachhang model
                Console.WriteLine($"{account.MakhNavigation.Ho} {account.MakhNavigation.Ten}");
                ViewBag.TenKH = $"{account.MakhNavigation.Ho} {account.MakhNavigation.Ten}";
                return RedirectToAction("PlaceOrder", "Order");
            }
            else
            {
                // Authentication failed, display error message
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }
        }

        public IActionResult Logout()
        {
            // Clear TempData on logout
            TempData.Clear();
            return RedirectToAction("Index", "Home"); // Redirect to the home page after logout
        }
    }


}
