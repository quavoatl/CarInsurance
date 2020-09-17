using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.NewDataAccess.DbModels
{
    public class Cover
    {
        public Cover()
        {
            Limit = new List<Limit>();
            Question = new List<Question>();
        }

        public Guid CoverBrokerRefId { get; set; }
        public int CoverId { get; set; }
        public string Type { get; set; }
        public DateTime AdditionDate { get; set; }
        public Guid QuestionCoverId { get; set; } //acts as a foreign key to Question class
        public Guid LimitCoverId { get; set; } //acts as a foreign key to Limit class

        public ICollection<Limit> Limit { get; set; }
        public ICollection<Question> Question { get; set; }
        public BrokerPolicyTemplate BrokerPolicyTemplate { get; set; }
    }
}
