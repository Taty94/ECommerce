﻿// <auto-generated />
using System;
using Course.ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Course.ECommerce.Infrastructure.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20220627195709_AddProductBrandAndType")]
    partial class AddProductBrandAndType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Course.ECommerce.Domain.Entities.Catalogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Catalogue");
                });

            modelBuilder.Entity("Course.ECommerce.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Course.ECommerce.Domain.Entities.ProductBrand", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductBrand");
                });

            modelBuilder.Entity("Course.ECommerce.Domain.Entities.ProductType", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductType");
                });
#pragma warning restore 612, 618
        }
    }
}
