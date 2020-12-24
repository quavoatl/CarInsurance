using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.ConstantsAndHelpers.CustomModelValidations;
using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository.IRepository;
using CarInsurance.DataAccessV3.ViewModels.Broker;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarInsurance.MainApp.Areas.Broker.Controllers
{
    [Area("Broker")]
    [Authorize(Roles = "Broker")]
    public class CoverController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly CarInsuranceContextV3 _dbContext;
        private readonly IBrokerService _brokerService;
        private readonly IUnitOfWork _unitOfWork;

        public CoverController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
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
        public IActionResult Create(string coverType)
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            var coverAlreadyCreated = false;

            Cover coverRetrievedFromDb = null;
            Cover coverToCreate = null;

            foreach (var coverFromDb in _brokerService.GetCovers(_loggedUser))
            {
                if (coverFromDb.Type.Equals(coverType))
                {
                    coverAlreadyCreated = true;
                    coverToCreate = coverFromDb;
                }
            }

            if (!coverAlreadyCreated)
            {
                coverToCreate = new Cover()
                {
                    CoverBrokerRefId = Guid.Parse(_loggedUser.Id),
                    AdditionDate = DateTime.Now,
                    Type = coverType,
                    LimitCoverId = Guid.NewGuid(),
                    QuestionCoverId = Guid.NewGuid()
                };

                return View(coverToCreate);
            }

            else
            {
                return View(coverRetrievedFromDb);
            }

           
        }

        [HttpGet]
        public IActionResult Edit()
        {
            // send to the view the model retrieved from the user cover
            // ex: broker clicks on edit theft => load the page with info from the db with that cover

            return View();
        }

        [HttpPost]
        public IActionResult Create(Cover cover)
        {
            if (ModelState.IsValid)
            {
                // in the view add hidden fields so the model can be complete
                AppUser _loggedUser = userManager.GetUserAsync(User).Result;

                _unitOfWork.CoverRepository.Add(cover);
               
                int response = _unitOfWork.Save();
                if(response == 1)
                {
                    ViewBag.SavedCover = cover.Type;
                    ViewBag.CreationDone = true;
                }

                return View(cover);

            }
            else return View(cover); // some validation error occured, return model to the view to render the errors

        }

        [HttpGet]
        public IActionResult GetLimitQuestionPartial()
        {
            AppUser _loggedUser = userManager.GetUserAsync(User).Result;
            Cover coverRetrievedFromDb = null;

            foreach (var coverFromDb in _brokerService.GetCovers(_loggedUser))
            {
                if (coverFromDb.Type.Equals("Natural Hazards")) //USE SESSION I GUESS 
                {
                    coverRetrievedFromDb = coverFromDb;
                }
            }

            return PartialView("_LimitQuestionCreationPartial", coverRetrievedFromDb);
        }

    }
}
