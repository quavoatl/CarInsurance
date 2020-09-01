using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarInsurance.DatabaseDemo.DbModels
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