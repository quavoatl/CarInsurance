using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccess.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarInsurance.DataAccess.Models;

namespace CarInsurance.CarInsuranceMVCMain.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _dbContext;

        public HomeController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index(int id = 1)
        {
            var broker = _dbContext.Broker.FirstOrDefault(b => b.Id.Equals(id));
            return View(broker);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
