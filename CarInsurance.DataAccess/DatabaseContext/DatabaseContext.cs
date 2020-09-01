using System;
using System.Collections.Generic;
using System.Text;
using CarInsurance.DataAccess.Models;
using CarInsurance.DataAccess.ModelsPOCOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarInsurance.DataAccess.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Broker> Broker { get; set; }
        public DbSet<BrokerDetails> BrokerDetails { get; set; }
        public DbSet<CoverTheft> CoverTheft { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BrokerDetails>().HasKey(details => details.BrokerId);
            modelBuilder.Entity<PolicyTemplate>().HasNoKey();
            modelBuilder.Entity<CoverTheft>().HasKey(cover => cover.CoverId);
            
        }
    }

   

}
