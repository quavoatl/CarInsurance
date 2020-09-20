using System;
using CarInsurance.ConstantsAndHelpers.CustomModelValidations;
using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository.IRepository;
using CarInsurance.DataAccessV3.ViewModels.Broker.PolicyController;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarInsurance.MainApp.Areas.Broker.Controllers
{
    [Area("Broker")]
    [Authorize(Roles = "Broker")]
    public class PolicyTemplateController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly CarInsuranceContextV3 _dbContext;
        private readonly IBrokerService _brokerService;
        private readonly IUnitOfWork _unitOfWork;

        public PolicyTemplateController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                                RoleManager<IdentityRole> roleManager, CarInsuranceContextV3 context,
                                IBrokerService brokerService, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._dbContext = context;
            this._brokerService = brokerService;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Create()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            var hasTemplate = _brokerService.CheckBrokerHasPolicyTemplate(_loggedUser);

            if (!hasTemplate) _brokerService.CreateBrokerPolicyTemplate(_loggedUser);

            if (_brokerService.GetCars(_loggedUser).Count == 0)
            {
                return RedirectToAction("createcar", "car", new { area = "broker" });
            }
            else
            {
                return RedirectToPage("createlimit", "limit", new { area = "broker" });
            }


        }
        
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

    }
}
