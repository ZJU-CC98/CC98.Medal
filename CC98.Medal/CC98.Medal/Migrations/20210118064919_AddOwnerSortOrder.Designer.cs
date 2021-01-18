﻿// <auto-generated />
using System;
using CC98.Medal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CC98.Medal.Migrations
{
    [DbContext(typeof(CC98MedalDbContext))]
    [Migration("20210118064919_AddOwnerSortOrder")]
    partial class AddOwnerSortOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CC98.Medal.Data.Medal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BuySettingString")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BuySettings");

                    b.Property<bool>("CanApply")
                        .HasColumnType("bit");

                    b.Property<bool>("CanBuy")
                        .HasColumnType("bit");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HideOwners")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Medals");
                });

            modelBuilder.Entity("CC98.Medal.Data.MedalCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("MedalCategories");
                });

            modelBuilder.Entity("CC98.Medal.Data.MedalIssueRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MedalId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedalId");

                    b.ToTable("MedalIssueRecords");
                });

            modelBuilder.Entity("CC98.Medal.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserId")
                        .UseIdentityColumn();

                    b.Property<int>("Wealth")
                        .HasColumnType("int")
                        .HasColumnName("UserWealth");

                    b.HasKey("Id");

                    b.ToTable("Users", t => t.ExcludeFromMigrations());
                });

            modelBuilder.Entity("CC98.Medal.Data.UserMedalOwnership", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("MedalId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("ExpireTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("UserId", "MedalId");

                    b.HasIndex("MedalId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMedalOwnerships");
                });

            modelBuilder.Entity("CC98.Medal.Data.Medal", b =>
                {
                    b.HasOne("CC98.Medal.Data.MedalCategory", "Category")
                        .WithMany("Medals")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CC98.Medal.Data.MedalCategory", b =>
                {
                    b.HasOne("CC98.Medal.Data.MedalCategory", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("CC98.Medal.Data.MedalIssueRecord", b =>
                {
                    b.HasOne("CC98.Medal.Data.Medal", "Medal")
                        .WithMany()
                        .HasForeignKey("MedalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medal");
                });

            modelBuilder.Entity("CC98.Medal.Data.UserMedalOwnership", b =>
                {
                    b.HasOne("CC98.Medal.Data.Medal", "Medal")
                        .WithMany()
                        .HasForeignKey("MedalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medal");
                });

            modelBuilder.Entity("CC98.Medal.Data.MedalCategory", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Medals");
                });
#pragma warning restore 612, 618
        }
    }
}
