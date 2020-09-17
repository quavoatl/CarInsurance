using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using CarInsurance.DataAccessV3.DbModels;
using CarInsurance.DataAccessV3.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.DbModelsRepositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly CarInsuranceContextV3 _dbContext;

        public CarRepository(CarInsuranceContextV3 db) : base(db)
        {
            _dbContext = db;
        }

        public void Update(Car car)
        {
            var carFromDb = _dbContext.Car.FirstOrDefault(c => c.CarBrokerRefId.Equals(car.CarBrokerRefId));

            if (carFromDb != null)
            {
                carFromDb.Brand = car.Brand;
                carFromDb.Model = car.Model;
                carFromDb.EngineCC = car.EngineCC;
                carFromDb.EuroStandard = car.EuroStandard;
                carFromDb.Year = car.Year;

                _dbContext.SaveChanges();
            }
        }
    }
}
