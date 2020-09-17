using System;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class CarRule
    {
        public int CarRuleId { get; set; }
        public Guid CarRuleCoverId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Multiplier { get; set; }

        public Car Car { get; set; }
    }
}