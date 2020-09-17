using System;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class LimitRule
    {
        public int LimitRuleId { get; set; }
        public Guid LimitRuleCoverId { get; set; }
        public string Name { get; set; }
        public string RuleText { get; set; }
        public float Multiplier { get; set; }

        public Limit Limit { get; set; }
    }
}