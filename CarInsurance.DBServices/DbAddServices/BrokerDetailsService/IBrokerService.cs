using CarInsurance.ConstantsAndHelpers.Enums;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DBServices.DbAddServices.BrokerDetailsService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DBServices.DbAddServices.BrokerDetailsService
{
    public interface IBrokerService
    {
        bool CreateBrokerPolicyTemplate(AppUser user);
        bool CheckIsBroker(AppUser user);
        bool CheckBrokerHasPolicyTemplate(AppUser user);
        BrokerPolicyTemplate RetrieveBrokerPolicyTemplate(AppUser user);
        ICollection<Cover> GetCovers(AppUser user);
        ICollection<Car> GetCars(AppUser user);
        Cover GetSpecificCover(CoverType coverType, AppUser user);

    }
}
