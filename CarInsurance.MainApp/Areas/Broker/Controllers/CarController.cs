using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.ConstantsAndHelpers.CustomModelValidations;
using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository.IRepository;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarInsurance.MainApp.Areas.Broker.Controllers
{
    [Area("Broker")]
    [Authorize(Roles = "Broker")]
    public class CarController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly CarInsuranceContextV3 _dbContext;
        private readonly IBrokerService _brokerService;
        private readonly IUnitOfWork _unitOfWork;

        public CarController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                var brandModelMatching = new CarModelValidation();

                if (brandModelMatching.AllowedModelsOnBrand[car.Brand.ToLower()].Contains(car.Model.ToLower()))
                {
                    AppUser _loggedUser = userManager.GetUserAsync(User).Result;
                    car.CarBrokerRefId = Guid.Parse(_loggedUser.Id);
                    car.CarRuleCoverId = Guid.NewGuid();

                    _unitOfWork.CarRepository.Add(car);
                    _unitOfWork.Save();

                    return RedirectToAction("list", "car", new { area = "broker" });
                }
                else
                {
                    ViewBag.BrandModelMatchingError = $"There is no match between {car.Brand} and {car.Model} !!!";
                    return View(car); // the brand/model combination does not exist
                }

            }
            else return View(car); // some validation error occured, return model to the view to render the errors
        }


        [HttpGet]
        public IActionResult List()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            var listOfCars = _brokerService.GetCars(_loggedUser);

            return View(listOfCars);
        }

        [HttpPost]
        public IActionResult Edit(Car carModel)
        {
            if (ModelState.IsValid)
            {
                var brandModelMatching = new CarModelValidation();

                if (brandModelMatching.AllowedModelsOnBrand[carModel.Brand.ToLower()].Contains(carModel.Model.ToLower()))
                {
                    var carFromDb = _dbContext.Car.Find(carModel.CarId);
                    carFromDb.Brand = carModel.Brand;
                    carFromDb.EuroStandard = carModel.EuroStandard;
                    carFromDb.Model = carModel.Model;
                    carFromDb.EngineCC = carModel.EngineCC;
                    carFromDb.Year = carModel.Year;
                    _dbContext.SaveChanges();

                    return RedirectToAction("list", "car", new { area = "broker" });
                }
                else
                {
                    ViewBag.BrandModelMatchingError = $"There is no match between {carModel.Brand} and {carModel.Model} !!!";
                    return View(carModel); // the brand/model combination does not exist
                }

            }
            else return View(carModel); // some validation error occured, return model to the view to render the errors
        }

        [HttpGet]
        public IActionResult Edit(int carid)
        {
            var car = _dbContext.Car.Find(carid);

            return View(car);
        }
    }
}