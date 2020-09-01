using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarInsurance.DatabaseDemo.DbModels
{
    public abstract class AbstractCover
    {
        public int CoverId { get; set; }
        public string Limits { get; set; }
        public string Questions { get; set; }
        [NotMapped] public List<Limit> LimitsList { get; set; }
        [NotMapped] public List<Question> QuestionsList { get; set; }
    }
}
