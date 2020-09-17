using System;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class QuestionRule
    {
        public int QuestionRuleId { get; set; }
        public Guid QuestionRuleCoverId { get; set; }
        public string Name { get; set; }
        public string RuleText { get; set; }
        public double Multiplier { get; set; }

        public Question Question { get; set; }

    }
}