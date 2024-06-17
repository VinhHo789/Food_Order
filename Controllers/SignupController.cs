using Lab05.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Lab05.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Lab05.Controllers
{
    // SignupController.cs
    public class SignupController : Controller
    {
        private readonly foodContext _dbContext;

        public SignupController(foodContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Account account, Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                // Save account to database
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();

                // Use the newly created account ID for Khachhang

                // Save Khachhang to database
                _dbContext.Khachhangs.Add(khachhang);
                _dbContext.SaveChanges();

                // Redirect to a success page or login page
                return RedirectToAction("Success");
            }

            // If ModelState is not valid, return to the signup page
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }
    }



}
