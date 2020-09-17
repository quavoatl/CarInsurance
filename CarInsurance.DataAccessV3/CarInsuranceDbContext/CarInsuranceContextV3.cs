using CarInsurance.DataAccessV3.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.DataAccessV3.CarInsuranceDbContext
{
    public class CarInsuranceContextV3 : IdentityDbContext<AppUser>
    {
        public DbSet<BrokerPolicyTemplate> BrokerPolicyTemplate { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarRule> CarRule { get; set; }
        public DbSet<Limit> Limit { get; set; }
        public DbSet<LimitRule> LimitRule { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionRule> QuestionRule { get; set; }
        public DbSet<Cover> Cover { get; set; }
        public DbSet<AppUser> AppUser { get; set; }

        public CarInsuranceContextV3(DbContextOptions options) : base(options) 
        {
        }

        public CarInsuranceContextV3()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=EN1410441\SQLEXPRESS;Database=CarInsuranceV3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.Property(p => p.Address)
                .IsRequired(true)
                .HasMaxLength(40)
                .IsUnicode(false);

                entity.Property(p => p.IsBroker)
                .IsRequired(true)
                .HasColumnType("bit");
            });

            modelBuilder.Entity<BrokerPolicyTemplate>(entity =>
            {
                entity.HasKey(c => new { c.BrokerReferenceId });

                entity.Property(e => e.TemplateReady)
                .HasColumnType("bit")
                .IsRequired(true);

                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.HasMany(d => d.Car)
                      .WithOne(p => p.BrokerPolicyTemplate)
                      .HasPrincipalKey(d => d.CarBrokerRefId)
                      .HasForeignKey(p => p.CarBrokerRefId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_CarType");

                entity.HasMany(d => d.Cover)
                   .WithOne(p => p.BrokerPolicyTemplate)
                   .HasPrincipalKey(d => d.CoverBrokerRefId)
                   .HasForeignKey(p => p.CoverBrokerRefId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_CoverType");
            });

            modelBuilder.Entity<Cover>(entity =>
            {
                entity.HasKey(c => new { c.CoverId });

                entity.Property(e => e.Type)
                .IsRequired(true)
                .HasMaxLength(25)
                .IsUnicode(false);

                entity.Property(e => e.AdditionDate)
              .HasColumnType("date")
              .IsRequired(true);

                entity.HasMany(d => d.Limit)
                    .WithOne(p => p.Cover)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasPrincipalKey(x => x.LimitCoverId)
                    .HasForeignKey(p => p.LimitCoverId)
                    .HasForeignKey(d => d.LimitCoverId)
                    .HasConstraintName("FK_LimitType");

                entity.HasMany(d => d.Question)
                 .WithOne(p => p.Cover)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasPrincipalKey(d => d.QuestionCoverId)
                 .HasForeignKey(p => p.QuestionCoverId)
                 .HasConstraintName("FK_QuestionType");

                entity.HasCheckConstraint(
                         "constraint_coverType", "`Type` in ('theft','accidents','naturalhazard')");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(c => new { c.CarId });

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EngineCC)
                    .HasColumnType("smallint")
                    .IsRequired();

                entity.Property(e => e.Year)
                .HasColumnType("date")
                .IsRequired(true);

                entity.Property(e => e.EuroStandard)
                .IsRequired(true)
                .HasMaxLength(8)
                .IsUnicode(false);

                entity.HasMany(d => d.CarRule)
                   .WithOne(p => p.Car)
                   .HasPrincipalKey(d => d.CarRuleCoverId)
                   .HasForeignKey(p => p.CarRuleCoverId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_CarRuleType");

                entity.HasCheckConstraint(
                          "constraint_euroStandard", "`EuroStandard` in ('Euro1','Euro2','Euro3','Euro4','Euro5','Euro6')");

                entity.HasCheckConstraint(
                          "constraint_carBrand", "`Brand` in ('volkswagen','bmw','mercedes-benz','opel','dacia','renault','toyota','skoda')");

                entity.HasCheckConstraint(
                          "constraint_carEngine", "`EngineCC` > 1000 and `EngineCC` < 5000");

                entity.HasCheckConstraint(
                          "constraint_carMode",
                          "`Model` in ('octavia','rapid','fabia','passat','golf','c200','e200','c220','e220','320d','325d','330d','530d'," +
                          "'520d','525d','astra','corsa','megane','clio','logan','sandero','avensis','prius')");
            });

            modelBuilder.Entity<CarRule>(entity =>
            {
                entity.HasKey(c => new { c.CarRuleId });

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.Description)
               .IsRequired(true)
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.Multiplier)
                .HasColumnType("float(2)")
                .IsRequired(true);
            });

            modelBuilder.Entity<Limit>(entity =>
            {
                entity.HasKey(c => new { c.LimitId });

                entity.Property(p => p.CoverType)
                .IsRequired(true)
                .HasMaxLength(25)
                .IsUnicode(false);

                entity.Property(p => p.Name)
                                .IsRequired(true)
                                .HasMaxLength(25)
                                .IsUnicode(false);

                entity.Property(p => p.LimitValues)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(150);

                entity.HasMany(d => d.LimitRule)
                                   .WithOne(p => p.Limit)
                                   .HasPrincipalKey(d => d.LimitRuleCoverId)
                                   .HasForeignKey(p => p.LimitRuleCoverId)
                                   .OnDelete(DeleteBehavior.ClientSetNull)
                                   .HasConstraintName("FK_LimitRuleType");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(c => new { c.QuestionId });

                entity.Property(p => p.CoverType)
                .IsRequired(true)
                .HasMaxLength(25)
                .IsUnicode(false);

                entity.Property(p => p.QuestionRuleCoverId)
                    .HasColumnType("uniqueidentifier")
                    .IsRequired(true)
                    .IsUnicode(false);


                entity.Property(p => p.Name)
                                .IsRequired(true)
                                .HasMaxLength(25)
                                .IsUnicode(false);

                entity.Property(p => p.Text)
                .IsRequired(true)
                .IsUnicode(false)
                .HasMaxLength(150);

                entity.HasOne(d => d.QuestionRule)
                                   .WithOne(p => p.Question)
                                   .HasPrincipalKey<Question>(d=>d.QuestionRuleCoverId)
                                   .HasForeignKey<QuestionRule>(d => d.QuestionRuleCoverId)
                                   .OnDelete(DeleteBehavior.ClientSetNull)
                                   .HasConstraintName("FK_QuestionRuleType");
            });

            modelBuilder.Entity<LimitRule>(entity =>
            {
                entity.HasKey(c => new { c.LimitRuleId });

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.RuleText)
               .IsRequired(true)
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.Multiplier)
                .HasColumnType("float(2)")
                .IsRequired(true);
            });

            modelBuilder.Entity<QuestionRule>(entity =>
            {
                entity.HasKey(c => new { c.QuestionRuleId });

                entity.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(20)
                .IsUnicode(false);

                entity.Property(e => e.RuleText)
               .IsRequired(true)
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.Multiplier)
                .HasColumnType("float(2)")
                .IsRequired(true);
            });

        }
    }
}
