﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.Data;

#nullable disable

namespace Project.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240110164353_NazwaMigracji")]
    partial class NazwaMigracji
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Project.Models.AppUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<float?>("DailyCalorieGoal")
                        .HasColumnType("real");

                    b.Property<float?>("DailyCarbohydratesGoal")
                        .HasColumnType("real");

                    b.Property<float?>("DailyFatGoal")
                        .HasColumnType("real");

                    b.Property<float?>("DailyProteinGoal")
                        .HasColumnType("real");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Project.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MealId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Project.Models.MealProduct", b =>
                {
                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("QuantityInGrams")
                        .HasColumnType("real");

                    b.HasKey("MealId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("MealProducts");
                });

            modelBuilder.Entity("Project.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<float>("Calories")
                        .HasColumnType("real");

                    b.Property<float>("Carbohydrates")
                        .HasColumnType("real");

                    b.Property<float>("Fat")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Protein")
                        .HasColumnType("real");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Project.Models.UserMeal", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ConsumedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("UserMeals");
                });

            modelBuilder.Entity("Project.Models.MealProduct", b =>
                {
                    b.HasOne("Project.Models.Meal", "Meal")
                        .WithMany("MealProducts")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.Product", "Product")
                        .WithMany("MealProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Project.Models.UserMeal", b =>
                {
                    b.HasOne("Project.Models.Meal", "Meal")
                        .WithMany("UserMeals")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project.Models.AppUser", "AppUser")
                        .WithMany("UserMeals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("Project.Models.AppUser", b =>
                {
                    b.Navigation("UserMeals");
                });

            modelBuilder.Entity("Project.Models.Meal", b =>
                {
                    b.Navigation("MealProducts");

                    b.Navigation("UserMeals");
                });

            modelBuilder.Entity("Project.Models.Product", b =>
                {
                    b.Navigation("MealProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
