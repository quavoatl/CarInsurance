using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.DbModels
{
    public class Question
    {
        public Question()
        {
            QuestionRule = new QuestionRule();
        }

        public int QuestionId { get; set; }
        public Guid QuestionCoverId { get; set; }
        public string CoverType { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid QuestionRuleCoverId { get; set; } //FK


        public QuestionRule QuestionRule { get; set; }
        public Cover Cover { get; set; }
    }
}
