using System;
using System.Collections.Generic;

namespace CarInsurance.NewDataAccess.DbModels
{
    public class Car
    {
        public Car()
        {
            CarRule = new List<CarRule>();
        }

        public int CarId { get; set; }
        public Guid CarBrokerRefId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public int EngineCC { get; set; }
        public string EuroStandard { get; set; }
        public Guid CarRuleCoverId { get; set; } //FK

        public ICollection<CarRule> CarRule { get; set; }
        public BrokerPolicyTemplate BrokerPolicyTemplate { get; set; }
    }
}