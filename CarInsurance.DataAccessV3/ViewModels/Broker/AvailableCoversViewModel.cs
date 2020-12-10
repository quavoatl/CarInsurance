using CarInsurance.DataAccessV3.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.ViewModels.Broker
{
    public class AvailableCoversViewModel
    {
        public AvailableCoversViewModel()
        {
            ListOfAdditionalCoverDetails = new List<CoverViewModel>()
            {
                new CoverViewModel()
                {
                    Name = "Natural Hazards",
                    ImageURL = "https://garsidej.files.wordpress.com/2017/05/fire-7800934_orig.jpg"
                },

                new CoverViewModel()
                {
                    Name = "Accidents",
                    ImageURL = "https://2aszhi3llh0x466uws21w6cc-wpengine.netdna-ssl.com/wp-content/uploads/car-accidents.jpg"
                },

                new CoverViewModel()
                {
                    Name = "Theft",
                    ImageURL = "https://herthundbuss.com/wp-content/uploads/2019/04/Car_theft_Herthbuss_01_EN.jpg"
                }
            };
        }

        public ICollection<CoverViewModel> ListOfAdditionalCoverDetails { get; set; }
        public ICollection<Cover> ListOfCoversFromDb { get; set; }
    }

    public class CoverViewModel
    {
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public bool AlreadyCreated { get; set; }
    }
}

