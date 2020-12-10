using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.ViewModels.Broker;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
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
        private readonly IBrokerService _brokerService;

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IBrokerService brokerService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._brokerService = brokerService;
        }

        [HttpGet]
        public IActionResult ManagePolicyTemplate()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            var brokerTemplate = _brokerService.RetrieveBrokerPolicyTemplate(_loggedUser);
            return View(brokerTemplate);
        }

        [HttpGet]
        public IActionResult AvailableCovers()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;

            var listOfCoversFromDB = _brokerService.GetCovers(_loggedUser);
            var viewObject = new AvailableCoversViewModel()
            {
                ListOfCoversFromDb = listOfCoversFromDB
            };
           
            return View(viewObject);
        }

        [HttpGet]
        [Authorize(Roles = "Broker")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
