﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.ConstantsAndHelpers.CustomModelValidations;
using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository;
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
    public class PolicyController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly CarInsuranceContextV3 _dbContext;
        private readonly IBrokerService _brokerService;
        private readonly IUnitOfWork _unitOfWork;

        public PolicyController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
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

        [HttpPost]
        public IActionResult Create(LimitAndQuestionViewModel model)
        {


            //if (ModelState.IsValid)
            //{
            //    Limit theftLimit = _dbContext.Limit.FindAsync(Guid.Parse(userManager.GetUserAsync(User).Result.Id)).Result;
            //    try
            //    {
            //        theftLimit.Name = model.Limit.Name;
            //        theftLimit.LimitValues = string.Join(",", model.Limit.LimitValues.ToArray());
            //        _dbContext.SaveChanges();
            //    }
            //    catch (NullReferenceException) { }

            //    theftLimit = _dbContext.TheftLimit.Add(new TheftLimit
            //    {
            //        ThefLimittBrokerId = Guid.Parse(userManager.GetUserAsync(User).Result.Id),
            //        Name = model.Limit.Name,
            //        //LimitValues = model.Limit.LimitValues.ToString()
            //    }).Entity;

            //    _dbContext.SaveChanges();

            //return RedirectToAction("index", "home", new { area = "Customer" });
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            var hasTemplate = _brokerService.CheckBrokerHasPolicyTemplate(_loggedUser);

            if (!hasTemplate) _brokerService.CreateBrokerPolicyTemplate(_loggedUser);

            if (_brokerService.GetCars(_loggedUser).Count == 0)
            {
                return RedirectToAction("createcar", "policy", new { area = "broker" });
            }
            else
            {
                return RedirectToPage("createlimit", "policy", new { area = "broker" });
            }


            //var userLogged = userManager.GetUserAsync(User).Result;

            //TheftLimit theftLimit = null;
            //try
            //{
            //    theftLimit = _dbContext.TheftLimit.Where(
            //         limit => limit.ThefLimittBrokerId.Equals(Guid.Parse(userManager.GetUserAsync(User).Result.Id)))
            //        .FirstOrDefault();
            //}
            //catch (Exception) { }

            //var modelObject = new LimitAndQuestionViewModel
            //{
            //    Limit = new DataAccess.InfrastructureObjects.Interfaces.Limit
            //    {
            //        LimitValues = new List<int>()
            //    },
            //    //Questions = new List<Question>()
            //};

            //if (theftLimit != null)
            //{
            //    modelObject.Limit.Name = theftLimit.Name;

            //    try
            //    {
            //        foreach (var limit in theftLimit.LimitValues.Split(',', StringSplitOptions.RemoveEmptyEntries))
            //        {
            //            modelObject.Limit.LimitValues.Add(int.Parse(limit));
            //        }

            //    }
            //    catch (NullReferenceException) { }
            //}

            //modelObject.Limit.LimitValues.Add(1000);

        }

        [HttpPost]
        public IActionResult CreateLimit(Limit limit)
        {
            //TheftLimit theftLimit = null;
            //try
            //{
            //    theftLimit = _dbContext.TheftLimit.Where(
            //     limit => limit.ThefLimittBrokerId.Equals(Guid.Parse(userManager.GetUserAsync(User).Result.Id)))
            //    .FirstOrDefault();
            //}
            //catch (Exception) { }

            //if (theftLimit.LimitValues == null)
            //{
            //    var limitList = new List<int>() { limit.LimitValue };
            //    theftLimit.LimitValues = limitList.ToString();
            //}
            //else
            //{
            //    theftLimit.LimitValues += limit.LimitValue.ToString() + ",";
            //}
            //_dbContext.SaveChanges();

            //int userInput = limit.LimitValue;

            return RedirectToAction("create", "policy", new { area = "Broker" });
        }

        [HttpGet]
        public IActionResult CreateCar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCar(Car car)
        {
            if (ModelState.IsValid)
            {
                var brandModelMatching = new CarModelValidation();

                if (brandModelMatching.AllowedModelsOnBrand[car.Brand.ToLower()].Contains(car.Model.ToLower()))
                {
                    AppUser _loggedUser = userManager.GetUserAsync(User).Result;
                    car.CarBrokerRefId = Guid.Parse(_loggedUser.Id);

                    _unitOfWork.CarRepository.Add(car);
                    _unitOfWork.Save();

                    return RedirectToAction("listcars", "policy", new { area = "Broker" });
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
        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListCars()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            var listOfCars = _brokerService.GetCars(_loggedUser);

            return View(listOfCars);
        }

        [HttpPost]
        public IActionResult EditCar(Car carModel)
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

                    return RedirectToAction("listcars", "policy", new { area = "Broker" });
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
        public IActionResult EditCar(int carid)
        {
            var car = _dbContext.Car.Find(carid);

            return View(car);
        }
    }
}
