using System;
using System.Collections.Generic;
using System.Text;
using CarInsurance.DataAccessV2.DbModels;
using CarInsurance.DataAccessV2.Repositories.Cover;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarInsurance.DataAccessV2
{
    public static class ServiceCollectionExtensions
    {

        public static void RegisterYourLibrary(this IServiceCollection services, DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            services.AddScoped<ICoverRepository, CoverRepository>();
        }
    }
}
