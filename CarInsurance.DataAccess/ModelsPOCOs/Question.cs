using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccess.ModelsPOCOs
{
    public class Question
    {
        public Question(string name, List<string> qValues)
        {
            this.Name = name;
            this.QuestionValues = qValues;
        }
        public string Name { get; set; }
        public List<string> QuestionValues { get; set; }
    }
}
