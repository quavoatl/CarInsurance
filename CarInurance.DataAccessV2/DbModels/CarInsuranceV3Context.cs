using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CarInsurance.DataAccessV2.DbModels
{
    public partial class CarInsuranceV3Context : DbContext
    {
        public CarInsuranceV3Context()
        {
        }

        public CarInsuranceV3Context(DbContextOptions<CarInsuranceV3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<BrokerPolicyTemplate> BrokerPolicyTemplates { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<CarRule> CarRules { get; set; }
        public virtual DbSet<Cover> Covers { get; set; }
        public virtual DbSet<Limit> Limits { get; set; }
        public virtual DbSet<LimitRule> LimitRules { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionRule> QuestionRules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=CarInsuranceV3;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BrokerPolicyTemplate>(entity =>
            {
                entity.HasKey(e => e.BrokerReferenceId);

                entity.ToTable("BrokerPolicyTemplate");

                entity.HasIndex(e => e.CarBrokerRefId, "AK_BrokerPolicyTemplate_CarBrokerRefId")
                    .IsUnique();

                entity.HasIndex(e => e.CoverBrokerRefId, "AK_BrokerPolicyTemplate_CoverBrokerRefId")
                    .IsUnique();

                entity.Property(e => e.BrokerReferenceId).ValueGeneratedNever();

                entity.Property(e => e.CreationDate).HasColumnType("date");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.HasIndex(e => e.CarRuleCoverId, "AK_Car_CarRuleCoverId")
                    .IsUnique();

                entity.HasIndex(e => e.CarBrokerRefId, "IX_Car_CarBrokerRefId");

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EngineCc).HasColumnName("EngineCC");

                entity.Property(e => e.EuroStandard)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Year).HasColumnType("date");

                entity.HasOne(d => d.CarBrokerRef)
                    .WithMany(p => p.Cars)
                    .HasPrincipalKey(p => p.CarBrokerRefId)
                    .HasForeignKey(d => d.CarBrokerRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarType");
            });

            modelBuilder.Entity<CarRule>(entity =>
            {
                entity.ToTable("CarRule");

                entity.HasIndex(e => e.CarRuleCoverId, "IX_CarRule_CarRuleCoverId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CarRuleCover)
                    .WithMany(p => p.CarRules)
                    .HasPrincipalKey(p => p.CarRuleCoverId)
                    .HasForeignKey(d => d.CarRuleCoverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CarRuleType");
            });

            modelBuilder.Entity<Cover>(entity =>
            {
                entity.ToTable("Cover");

                entity.HasIndex(e => e.LimitCoverId, "AK_Cover_LimitCoverId")
                    .IsUnique();

                entity.HasIndex(e => e.QuestionCoverId, "AK_Cover_QuestionCoverId")
                    .IsUnique();

                entity.HasIndex(e => e.CoverBrokerRefId, "IX_Cover_CoverBrokerRefId");

                entity.Property(e => e.AdditionDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.CoverBrokerRef)
                    .WithMany(p => p.Covers)
                    .HasPrincipalKey(p => p.CoverBrokerRefId)
                    .HasForeignKey(d => d.CoverBrokerRefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CoverType");
            });

            modelBuilder.Entity<Limit>(entity =>
            {
                entity.ToTable("Limit");

                entity.HasIndex(e => e.LimitRuleCoverId, "AK_Limit_LimitRuleCoverId")
                    .IsUnique();

                entity.HasIndex(e => e.LimitCoverId, "IX_Limit_LimitCoverId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.LimitCover)
                    .WithMany(p => p.Limits)
                    .HasPrincipalKey(p => p.LimitCoverId)
                    .HasForeignKey(d => d.LimitCoverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LimitType");
            });

            modelBuilder.Entity<LimitRule>(entity =>
            {
                entity.ToTable("LimitRule");

                entity.HasIndex(e => e.LimitRuleCoverId, "IX_LimitRule_LimitRuleCoverId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RuleText)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.LimitRuleCover)
                    .WithMany(p => p.LimitRules)
                    .HasPrincipalKey(p => p.LimitRuleCoverId)
                    .HasForeignKey(d => d.LimitRuleCoverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LimitRuleType");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.HasIndex(e => e.QuestionRuleCoverId, "AK_Question_QuestionRuleCoverId")
                    .IsUnique();

                entity.HasIndex(e => e.QuestionCoverId, "IX_Question_QuestionCoverId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.QuestionCover)
                    .WithMany(p => p.Questions)
                    .HasPrincipalKey(p => p.QuestionCoverId)
                    .HasForeignKey(d => d.QuestionCoverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionType");
            });

            modelBuilder.Entity<QuestionRule>(entity =>
            {
                entity.ToTable("QuestionRule");

                entity.HasIndex(e => e.QuestionRuleCoverId, "IX_QuestionRule_QuestionRuleCoverId")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RuleText)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.QuestionRuleCover)
                    .WithOne(p => p.QuestionRule)
                    .HasPrincipalKey<Question>(p => p.QuestionRuleCoverId)
                    .HasForeignKey<QuestionRule>(d => d.QuestionRuleCoverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionRuleType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
