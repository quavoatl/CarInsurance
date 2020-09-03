using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarInsurance.DataAccess.InfrastructureObjects.Interfaces;

namespace CarInsurance.DataAccess.ModelsPOCOs
{
    [NotMapped]
    public class PolicyTemplate
    {
        public List<AbstractCover> ListOfCovers = new List<AbstractCover>();
    }

}