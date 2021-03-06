﻿// <auto-generated />
using System;
using CarInsurance.DataAccessV3.CarInsuranceDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarInsurance.DataAccessV3.Migrations
{
    [DbContext(typeof(CarInsuranceContextV3))]
    partial class CarInsuranceContextV3ModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40)
                        .IsUnicode(false);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsBroker")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.BrokerPolicyTemplate", b =>
                {
                    b.Property<Guid>("BrokerReferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarBrokerRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoverBrokerRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("date");

                    b.Property<bool>("TemplateReady")
                        .HasColumnType("bit");

                    b.HasKey("BrokerReferenceId");

                    b.ToTable("BrokerPolicyTemplate");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<Guid>("CarBrokerRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CarRuleCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("EngineCC")
                        .HasColumnType("smallint");

                    b.Property<string>("EuroStandard")
                        .IsRequired()
                        .HasColumnType("varchar(8)")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<DateTime>("Year")
                        .HasColumnType("date");

                    b.HasKey("CarId");

                    b.HasIndex("CarBrokerRefId");

                    b.ToTable("Car");

                    b.HasCheckConstraint("constraint_euroStandard", "`EuroStandard` in ('Euro1','Euro2','Euro3','Euro4','Euro5','Euro6')");

                    b.HasCheckConstraint("constraint_carBrand", "`Brand` in ('volkswagen','bmw','mercedes-benz','opel','dacia','renault','toyota','skoda')");

                    b.HasCheckConstraint("constraint_carEngine", "`EngineCC` > 1000 and `EngineCC` < 5000");

                    b.HasCheckConstraint("constraint_carMode", "`Model` in ('octavia','rapid','fabia','passat','golf','c200','e200','c220','e220','320d','325d','330d','530d','520d','525d','astra','corsa','megane','clio','logan','sandero','avensis','prius')");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.CarRule", b =>
                {
                    b.Property<int>("CarRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("CarRuleCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<double>("Multiplier")
                        .HasColumnType("float(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.HasKey("CarRuleId");

                    b.HasIndex("CarRuleCoverId");

                    b.ToTable("CarRule");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Cover", b =>
                {
                    b.Property<int>("CoverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AdditionDate")
                        .HasColumnType("date");

                    b.Property<Guid>("CoverBrokerRefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LimitCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("CoverId");

                    b.HasIndex("CoverBrokerRefId");

                    b.ToTable("Cover");

                    b.HasCheckConstraint("constraint_coverType", "`Type` in ('theft','accidents','naturalhazard')");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Limit", b =>
                {
                    b.Property<int>("LimitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverType")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<Guid>("LimitCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LimitRuleCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LimitValues")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.HasKey("LimitId");

                    b.HasIndex("LimitCoverId");

                    b.ToTable("Limit");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.LimitRule", b =>
                {
                    b.Property<int>("LimitRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("LimitRuleCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Multiplier")
                        .HasColumnType("float(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("RuleText")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("LimitRuleId");

                    b.HasIndex("LimitRuleCoverId");

                    b.ToTable("LimitRule");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoverType")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)")
                        .HasMaxLength(25)
                        .IsUnicode(false);

                    b.Property<Guid>("QuestionCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionRuleCoverId")
                        .HasColumnType("uniqueidentifier")
                        .IsUnicode(false);

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("QuestionId");

                    b.HasIndex("QuestionCoverId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.QuestionRule", b =>
                {
                    b.Property<int>("QuestionRuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Multiplier")
                        .HasColumnType("float(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<Guid>("QuestionRuleCoverId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RuleText")
                        .IsRequired()
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.HasKey("QuestionRuleId");

                    b.HasIndex("QuestionRuleCoverId")
                        .IsUnique();

                    b.ToTable("QuestionRule");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Car", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.BrokerPolicyTemplate", "BrokerPolicyTemplate")
                        .WithMany("Car")
                        .HasForeignKey("CarBrokerRefId")
                        .HasConstraintName("FK_CarType")
                        .HasPrincipalKey("CarBrokerRefId")
                        .IsRequired();
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.CarRule", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.Car", "Car")
                        .WithMany("CarRule")
                        .HasForeignKey("CarRuleCoverId")
                        .HasConstraintName("FK_CarRuleType")
                        .HasPrincipalKey("CarRuleCoverId")
                        .IsRequired();
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Cover", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.BrokerPolicyTemplate", "BrokerPolicyTemplate")
                        .WithMany("Cover")
                        .HasForeignKey("CoverBrokerRefId")
                        .HasConstraintName("FK_CoverType")
                        .HasPrincipalKey("CoverBrokerRefId")
                        .IsRequired();
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Limit", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.Cover", "Cover")
                        .WithMany("Limit")
                        .HasForeignKey("LimitCoverId")
                        .HasConstraintName("FK_LimitType")
                        .HasPrincipalKey("LimitCoverId")
                        .IsRequired();
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.LimitRule", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.Limit", "Limit")
                        .WithMany("LimitRule")
                        .HasForeignKey("LimitRuleCoverId")
                        .HasConstraintName("FK_LimitRuleType")
                        .HasPrincipalKey("LimitRuleCoverId")
                        .IsRequired();
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.Question", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.Cover", "Cover")
                        .WithMany("Question")
                        .HasForeignKey("QuestionCoverId")
                        .HasConstraintName("FK_QuestionType")
                        .HasPrincipalKey("QuestionCoverId")
                        .IsRequired();
                });

            modelBuilder.Entity("CarInsurance.DataAccessV3.DbModels.QuestionRule", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.Question", "Question")
                        .WithOne("QuestionRule")
                        .HasForeignKey("CarInsurance.DataAccessV3.DbModels.QuestionRule", "QuestionRuleCoverId")
                        .HasConstraintName("FK_QuestionRuleType")
                        .HasPrincipalKey("CarInsurance.DataAccessV3.DbModels.Question", "QuestionRuleCoverId")
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarInsurance.DataAccessV3.DbModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CarInsurance.DataAccessV3.DbModels.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
