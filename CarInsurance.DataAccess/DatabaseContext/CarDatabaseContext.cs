using System;
using System.Collections.Generic;
using System.Text;
using CarInsurance.DataAccess.InfrastructureObjects.Interfaces;
using CarInsurance.DataAccess.Models;
using CarInsurance.DataAccess.ModelsPOCOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInsurance.DataAccess.DatabaseContext
{
    public class CarDatabaseContext : IdentityDbContext<AppUser>
    {
       // public DbSet<CoverTheft> CoverTheft { get; set; }
        public DbSet<TheftLimit> TheftLimit { get; set; }
        //public DbSet<TheftQuestion> TheftQuestion { get; set; }
        public CarDatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<CoverTheft>();
          
        }
    }



}
