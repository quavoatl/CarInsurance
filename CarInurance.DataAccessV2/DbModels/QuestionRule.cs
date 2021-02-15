using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class QuestionRule
    {
        public int QuestionRuleId { get; set; }
        public Guid QuestionRuleCoverId { get; set; }
        public string Name { get; set; }
        public string RuleText { get; set; }
        public float Multiplier { get; set; }

        public virtual Question QuestionRuleCover { get; set; }
    }
}
