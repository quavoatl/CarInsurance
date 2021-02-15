using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarInsurance.DataAccessV2;
using CarInsurance.DataAccessV2.DbModels;
using CarInsurance.DataAccessV2.Repositories.Cover;
using Microsoft.EntityFrameworkCore;

namespace CarInsurance.Cover.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = "Server=.\\SQLExpress;Database=CarInsuranceV3;Trusted_Connection=True;";
            services.AddDbContext<CarInsuranceV3Context>(option => option.UseSqlServer(connectionString));

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            CarInsuranceV3Context appDbContext = serviceProvider.GetService<CarInsuranceV3Context>();

            services.RegisterYourLibrary(appDbContext);

            services.AddControllers();
            services.AddScoped<ICoverRepository, CoverRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
