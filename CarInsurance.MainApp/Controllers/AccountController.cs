using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccess.AccountController.ViewModels;
using CarInsurance.DataAccess.DatabaseContext;
using CarInsurance.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarInsurance.MainApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CarDatabaseContext _dbContext;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, CarDatabaseContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._dbContext = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "account");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, IsBroker = model.IsBroker };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (user.IsBroker) await _userManager.AddToRoleAsync(user, "Broker");
                else await _userManager.AddToRoleAsync(user, "Customer");

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    if (user.IsBroker) return RedirectToAction("Index", "Home", new { area = "Broker" });
                    else return RedirectToAction("Index", "Home", new { area = "Customer" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool userIsBroker = false;
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                userIsBroker = _dbContext.Users.Where(user => user.Email.Equals(model.Email))
                    .Select(user => user.IsBroker)
                    .FirstOrDefault();

                if (result.Succeeded)
                {
                    if (userIsBroker) return RedirectToAction("index", "home", new { area = "Broker" });
                    else return RedirectToAction("index", "home", new { area = "Customer" });
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessBlocked()
        {
            return View();
        }
    }
}
