using System;

namespace CarInsurance.NewDataAccess.DbModels
{
    public class LimitRule
    {
        public int LimitRuleId { get; set; }
        public Guid LimitRuleCoverId { get; set; }
        public string Name { get; set; }
        public string RuleText { get; set; }
        public double Multiplier { get; set; }

        public Limit Limit { get; set; }
    }
}