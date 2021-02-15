using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class LimitRule
    {
        public int LimitRuleId { get; set; }
        public Guid LimitRuleCoverId { get; set; }
        public string Name { get; set; }
        public string RuleText { get; set; }
        public float Multiplier { get; set; }

        public virtual Limit LimitRuleCover { get; set; }
    }
}
