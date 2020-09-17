using CarInsurance.NewDataAccess.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInsurance.NewDataAccess.CarInsuranceDbContext
{
    public class CarInsuranceContext : DbContext
    {
        public DbSet<BrokerPolicyTemplate> BrokerPolicyTemplate { get; set; }
        public DbSet<Car> Car { get; set; }
        public DbSet<CarRule> CarRule { get; set; }
        public DbSet<Limit> Limit { get; set; }
        public DbSet<LimitRule> LimitRule { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionRule> QuestionRule { get; set; }
        public DbSet<Cover> Cover { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=EN1410441\SQLEXPRESS;Database=CarInsuranceV2;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_CarType");

                entity.HasMany(d => d.Cover)
                   .WithOne(p => p.BrokerPolicyTemplate)
                   .HasPrincipalKey(d => d.CoverBrokerRefId)
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
                    .HasForeignKey(d => d.LimitCoverId)
                    .HasConstraintName("FK_LimitType");

                entity.HasMany(d => d.Question)
                 .WithOne(p => p.Cover)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasPrincipalKey(d => d.QuestionCoverId)
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
                                   .OnDelete(DeleteBehavior.ClientSetNull)
                                   .HasConstraintName("FK_LimitRuleType");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(c => new { c.QuestionCoverId });

                entity.Property(p => p.CoverType)
                .IsRequired(true)
                .HasMaxLength(25)
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
                entity.HasKey(c => new { c.QuestionRuleCoverId });

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
