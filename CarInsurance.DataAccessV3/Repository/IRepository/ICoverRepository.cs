using CarInsurance.DataAccessV3.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.IRepository
{
    public interface ICoverRepository : IRepository<Cover>
    {
        void Update(Cover cover);
    }
}
