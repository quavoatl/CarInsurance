using System;

namespace CarInsurance.NewDataAccess.DbModels
{
    public class QuestionRule
    {
        public Guid QuestionRuleCoverId { get; set; }
        public string Name { get; set; }
        public string RuleText { get; set; }
        public double Multiplier { get; set; }

        public Question Question { get; set; }

    }
}