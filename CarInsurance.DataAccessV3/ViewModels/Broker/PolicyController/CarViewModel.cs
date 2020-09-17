using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarInsurance.DataAccessV3.ViewModels.Broker.PolicyController
{
    public class CarViewModel
    {
        [Display(Name = "Car Brand", Description = "Volkswagen, Toyota, Skoda...")]
        public string Brand { get; set; }
        public string Model { get; set; }

        [DataType(DataType.Date)]
        public DateTime Year { get; set; }
        public int EngineCC { get; set; }

        public string EuroStandard { get; set; }
    }
}
