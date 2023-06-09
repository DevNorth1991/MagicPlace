﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using magicPlace_webApi.DataStore;

#nullable disable

namespace magicPlace_webApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230422040627_agregadoDelModeloOccupant2")]
    partial class agregadoDelModeloOccupant2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("magicPlace_webApi.Models.Occupant", b =>
                {
                    b.Property<int>("IdCard")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreationOcccupant")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdateOcccupant")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameOccupant")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("IdCard");

                    b.HasIndex("RoomId");

                    b.ToTable("Occupants");
                });

            modelBuilder.Entity("magicPlace_webApi.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Fee")
                        .HasColumnType("float");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupants")
                        .HasColumnType("int");

                    b.Property<int>("SquareMeters")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreationDate = new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9951),
                            Detail = "",
                            Fee = 125.0,
                            ImageUrl = "",
                            Name = "habitacion magica",
                            Occupants = 4,
                            SquareMeters = 16,
                            UpdateTime = new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9961)
                        },
                        new
                        {
                            Id = 2,
                            CreationDate = new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9964),
                            Detail = "",
                            Fee = 1109.0,
                            ImageUrl = "",
                            Name = "habitacion excellent",
                            Occupants = 4,
                            SquareMeters = 16,
                            UpdateTime = new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9964)
                        },
                        new
                        {
                            Id = 3,
                            CreationDate = new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9966),
                            Detail = "",
                            Fee = 100.0,
                            ImageUrl = "",
                            Name = "habitacion premium ",
                            Occupants = 4,
                            SquareMeters = 16,
                            UpdateTime = new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9966)
                        });
                });

            modelBuilder.Entity("magicPlace_webApi.Models.Occupant", b =>
                {
                    b.HasOne("magicPlace_webApi.Models.Room", "ObjRoom")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObjRoom");
                });
#pragma warning restore 612, 618
        }
    }
}
