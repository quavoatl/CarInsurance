using CarInsurance.ConstantsAndHelpers.CustomModelValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class Car
    {
        public Car()
        {
            CarRule = new List<CarRule>();
        }

        public int CarId { get; set; }
        public Guid CarBrokerRefId { get; set; }

        [CarBrandValidation]
        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime Year { get; set; }

        public int EngineCC { get; set; }

        public string EuroStandard { get; set; }

        public Guid CarRuleCoverId { get; set; } //FK //should be set when creating a car.

        public ICollection<CarRule> CarRule { get; set; }
        public BrokerPolicyTemplate BrokerPolicyTemplate { get; set; }
    }
}