﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Presentation.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230718130556_initialDB")]
    partial class initialDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.PriceItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Count")
                        .HasColumnType("integer")
                        .HasColumnName("Количество");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Наименование");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Номер запчасти");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("Цена");

                    b.Property<string>("SearchNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Номер для поиска");

                    b.Property<string>("SearchVendor")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Производитель для поиска");

                    b.Property<string>("Vendor")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Бренд");

                    b.HasKey("Id");

                    b.ToTable("PriceItems");
                });
#pragma warning restore 612, 618
        }
    }
}
