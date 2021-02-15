using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class Car
    {
        public Car()
        {
            CarRules = new HashSet<CarRule>();
        }

        public int CarId { get; set; }
        public Guid CarBrokerRefId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public short EngineCc { get; set; }
        public string EuroStandard { get; set; }
        public Guid CarRuleCoverId { get; set; }

        public virtual BrokerPolicyTemplate CarBrokerRef { get; set; }
        public virtual ICollection<CarRule> CarRules { get; set; }
    }
}
