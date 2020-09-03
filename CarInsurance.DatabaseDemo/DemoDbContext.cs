using System;
using System.Collections.Generic;
using System.Text;
using CarInsurance.DatabaseDemo.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInsurance.DatabaseDemo
{
    public class DemoDbContext : IdentityDbContext
    {
        public DbSet<Broker> Broker { get; set; }
        public DbSet<BrokerDetails> BrokerDetails { get; set; }
        public DbSet<CoverTheft> CoverTheft { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=EN1410441\SQLEXPRESS;Database=CarInsuranceDemo;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoverTheft>().HasKey(cover => cover.CoverId);
            
        }
    }

   

}
