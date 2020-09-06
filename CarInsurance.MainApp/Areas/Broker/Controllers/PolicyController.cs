using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccess.DatabaseContext;
using CarInsurance.DataAccess.InfrastructureObjects.Interfaces;
using CarInsurance.DataAccess.Models;
using CarInsurance.DataAccess.ViewModels.Broker.CreatePolicy;
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
        private readonly CarDatabaseContext _dbContext;

        public PolicyController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, CarDatabaseContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._dbContext = context;
        }

        [HttpPost]
        public IActionResult Create(LimitAndQuestionViewModel model)
        {

            if (ModelState.IsValid)
            {
                TheftLimit theftLimit = _dbContext.TheftLimit.FindAsync(Guid.Parse(userManager.GetUserAsync(User).Result.Id)).Result;
                try
                {
                    theftLimit.Name = model.Limit.Name;
                    theftLimit.LimitValues = string.Join(",", model.Limit.LimitValues.ToArray());
                    _dbContext.SaveChanges();
                }
                catch (NullReferenceException) { }

                theftLimit = _dbContext.TheftLimit.Add(new TheftLimit
                {
                    ThefLimittBrokerId = Guid.Parse(userManager.GetUserAsync(User).Result.Id),
                    Name = model.Limit.Name,
                    //LimitValues = model.Limit.LimitValues.ToString()
                }).Entity;

                _dbContext.SaveChanges();

                //return RedirectToAction("index", "home", new { area = "Customer" });

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var userLogged = userManager.GetUserAsync(User).Result;

            TheftLimit theftLimit = null;
            try
            {
                theftLimit = _dbContext.TheftLimit.Where(
                     limit => limit.ThefLimittBrokerId.Equals(Guid.Parse(userManager.GetUserAsync(User).Result.Id)))
                    .FirstOrDefault();
            }
            catch (Exception) { }

            var modelObject = new LimitAndQuestionViewModel
            {
                Limit = new DataAccess.InfrastructureObjects.Interfaces.Limit
                {
                    LimitValues = new List<int>()
                },
                //Questions = new List<Question>()
            };

            if (theftLimit != null)
            {
                modelObject.Limit.Name = theftLimit.Name;

                try
                {
                    foreach (var limit in theftLimit.LimitValues.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        modelObject.Limit.LimitValues.Add(int.Parse(limit));
                    }

                }
                catch (NullReferenceException) { }
            }

            //modelObject.Limit.LimitValues.Add(1000);
            return View(modelObject);
        }

        [HttpPost]
        public IActionResult AddLimit(Limit limit)
        {
            TheftLimit theftLimit = null;
            try
            {
                theftLimit = _dbContext.TheftLimit.Where(
                 limit => limit.ThefLimittBrokerId.Equals(Guid.Parse(userManager.GetUserAsync(User).Result.Id)))
                .FirstOrDefault();
            }
            catch (Exception) { }

            if (theftLimit.LimitValues == null)
            {
                var limitList = new List<int>() { limit.LimitValue };
                theftLimit.LimitValues = limitList.ToString();
            }
            else
            {
                theftLimit.LimitValues += limit.LimitValue.ToString() + ",";
            }
            _dbContext.SaveChanges();

            int userInput = limit.LimitValue;

            return RedirectToAction("create", "policy", new { area = "Broker" });
        }


    }
}
