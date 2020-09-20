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



    }
}
