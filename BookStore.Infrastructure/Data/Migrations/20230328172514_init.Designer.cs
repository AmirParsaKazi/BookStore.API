﻿// <auto-generated />
using System;
using BookStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230328172514_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookStore.Common.Models.Author", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookStore.Common.Models.AuthorBook", b =>
                {
                    b.Property<string>("BookId")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("AuthorId")
                        .HasColumnType("varchar(450)");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AuthorBooks");
                });

            modelBuilder.Entity("BookStore.Common.Models.Book", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("File")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<string>("LanguageId")
                        .IsRequired()
                        .HasColumnType("varchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStore.Common.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("ParentId")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookStore.Common.Models.CategoryBook", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("BookId")
                        .HasColumnType("varchar(450)");

                    b.HasKey("CategoryId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("CategoryBooks");
                });

            modelBuilder.Entity("BookStore.Common.Models.City", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ProvinceId")
                        .IsRequired()
                        .HasColumnType("varchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BookStore.Common.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CityId1")
                        .IsRequired()
                        .HasColumnType("varchar(450)");

                    b.Property<string>("CityId2")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("CityId1");

                    b.HasIndex("CityId2");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BookStore.Common.Models.Discount", b =>
                {
                    b.Property<string>("BookId")
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("Percent")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BookId");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("BookStore.Common.Models.Language", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("BookStore.Common.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("varchar(450)");

                    b.Property<string>("DispatchNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderStausId")
                        .IsRequired()
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalPurchasePrice")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderStausId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookStore.Common.Models.OrderBook", b =>
                {
                    b.Property<string>("BookId")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("OrderId")
                        .HasColumnType("varchar(450)");

                    b.HasKey("BookId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderBooks");
                });

            modelBuilder.Entity("BookStore.Common.Models.OrderStatus", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("BookStore.Common.Models.Province", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("BookStore.Common.Models.AuthorBook", b =>
                {
                    b.HasOne("BookStore.Common.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Common.Models.Book", "Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookStore.Common.Models.Book", b =>
                {
                    b.HasOne("BookStore.Common.Models.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("BookStore.Common.Models.Category", b =>
                {
                    b.HasOne("BookStore.Common.Models.Category", "ParentCategory")
                        .WithMany("Categories")
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("BookStore.Common.Models.CategoryBook", b =>
                {
                    b.HasOne("BookStore.Common.Models.Book", "Book")
                        .WithMany("Categories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Common.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookStore.Common.Models.City", b =>
                {
                    b.HasOne("BookStore.Common.Models.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Province");
                });

            modelBuilder.Entity("BookStore.Common.Models.Customer", b =>
                {
                    b.HasOne("BookStore.Common.Models.City", "City1")
                        .WithMany("Customers1")
                        .HasForeignKey("CityId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Common.Models.City", "City2")
                        .WithMany("Customers2")
                        .HasForeignKey("CityId2");

                    b.Navigation("City1");

                    b.Navigation("City2");
                });

            modelBuilder.Entity("BookStore.Common.Models.Discount", b =>
                {
                    b.HasOne("BookStore.Common.Models.Book", "Book")
                        .WithOne("Discount")
                        .HasForeignKey("BookStore.Common.Models.Discount", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookStore.Common.Models.Order", b =>
                {
                    b.HasOne("BookStore.Common.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Common.Models.OrderStatus", "Staus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStausId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Staus");
                });

            modelBuilder.Entity("BookStore.Common.Models.OrderBook", b =>
                {
                    b.HasOne("BookStore.Common.Models.Book", "Book")
                        .WithMany("Orders")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Common.Models.Order", "Order")
                        .WithMany("Books")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BookStore.Common.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStore.Common.Models.Book", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Categories");

                    b.Navigation("Discount")
                        .IsRequired();

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BookStore.Common.Models.Category", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Categories");
                });

            modelBuilder.Entity("BookStore.Common.Models.City", b =>
                {
                    b.Navigation("Customers1");

                    b.Navigation("Customers2");
                });

            modelBuilder.Entity("BookStore.Common.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BookStore.Common.Models.Language", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStore.Common.Models.Order", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookStore.Common.Models.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BookStore.Common.Models.Province", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
