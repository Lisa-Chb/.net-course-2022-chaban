﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ModelsDb.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkWithEntity.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220922161424_fixModelsDb")]
    partial class fixModelsDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ModelsDb.Account", b =>
                {
                    b.Property<Guid>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<int>("Amount")
                        .HasColumnType("integer")
                        .HasColumnName("amount");

                    b.Property<Guid>("Clientid")
                        .HasColumnType("uuid")
                        .HasColumnName("client_id");

                    b.HasKey("AccountId");

                    b.HasIndex("Clientid");

                    b.ToTable("accounts");
                });

            modelBuilder.Entity("ModelsDb.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("client_Id");

                    b.Property<int>("BonusDiscount")
                        .HasColumnType("integer")
                        .HasColumnName("bonus_discount");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid")
                        .HasColumnName("client_Id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<int?>("NumberOfPassport")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_passport");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("SeriesOfPassport")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("series_of_passport");

                    b.HasKey("ClientId");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("ModelsDb.Currency", b =>
                {
                    b.Property<Guid>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("currency_Id");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_Id");

                    b.Property<int>("Code")
                        .HasColumnType("integer")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("CurrencyId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("currences");
                });

            modelBuilder.Entity("ModelsDb.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("employee_Id");

                    b.Property<int>("BonusDiscount")
                        .HasColumnType("integer")
                        .HasColumnName("bonus_discount");

                    b.Property<string>("Contract")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("contract");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<int?>("NumberOfPassport")
                        .HasColumnType("integer")
                        .HasColumnName("number_of_passport");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("phone");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.Property<int>("Salary")
                        .HasColumnType("integer")
                        .HasColumnName("salary");

                    b.Property<string>("SeriesOfPassport")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("series_of_passport");

                    b.HasKey("EmployeeId");

                    b.ToTable("employee");
                });

            modelBuilder.Entity("ModelsDb.Account", b =>
                {
                    b.HasOne("ModelsDb.Client", "Client")
                        .WithMany("Accounts")
                        .HasForeignKey("Clientid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ModelsDb.Currency", b =>
                {
                    b.HasOne("ModelsDb.Account", "Account")
                        .WithOne("Currency")
                        .HasForeignKey("ModelsDb.Currency", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ModelsDb.Account", b =>
                {
                    b.Navigation("Currency")
                        .IsRequired();
                });

            modelBuilder.Entity("ModelsDb.Client", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
