using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBrokerPolicyTemplateRepository BrokerPolicyTemplateRepository { get; }
        ICarRepository CarRepository { get; }
        ICoverRepository CoverRepository { get; }
        int Save();
    }
}
