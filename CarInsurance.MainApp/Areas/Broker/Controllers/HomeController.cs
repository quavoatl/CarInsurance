using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccessV3.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarInsurance.MainApp.Areas.Broker.Controllers
{
    [Area("Broker")]
    [Authorize(Roles = "Broker")]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult ManagePolicyTemplate()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Broker")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
