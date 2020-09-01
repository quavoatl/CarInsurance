using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInsurance.DataAccess.Models
{
    public class BrokerDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BrokerId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Postcode { get; set; }
    }
}