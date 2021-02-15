using System;
using System.Collections.Generic;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public Guid QuestionCoverId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid QuestionRuleCoverId { get; set; }

        public virtual Cover QuestionCover { get; set; }
        public virtual QuestionRule QuestionRule { get; set; }
    }
}
