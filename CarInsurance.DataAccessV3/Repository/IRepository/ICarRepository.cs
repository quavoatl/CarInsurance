using CarInsurance.DataAccessV3.DbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        void Update(Car car);
    }
}
