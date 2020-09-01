using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInsurance.DatabaseDemo.DbModels
{
    [NotMapped]
    public class PolicyTemplate
    {
        [NotMapped]
        public List<AbstractCover> ListOfCovers = new List<AbstractCover>();
    }

}