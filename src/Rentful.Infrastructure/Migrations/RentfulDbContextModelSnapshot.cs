﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rentful.Infrastructure.Persistence;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    [DbContext(typeof(RentfulDbContext))]
    partial class RentfulDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Rentful.Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("building_number");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("country");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)")
                        .HasColumnName("postal_code");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("street");

                    b.HasKey("Id");

                    b.ToTable("address", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ApartmentId")
                        .HasColumnType("integer")
                        .HasColumnName("apartment_id");

                    b.Property<string>("DateAdded")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("date_added");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId")
                        .IsUnique();

                    b.ToTable("announcements", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Apartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Area")
                        .HasColumnType("double precision")
                        .HasColumnName("area");

                    b.Property<bool>("HasBalcony")
                        .HasColumnType("boolean")
                        .HasColumnName("has_balcony");

                    b.Property<bool>("HasElevator")
                        .HasColumnType("boolean")
                        .HasColumnName("has_elevator");

                    b.Property<bool>("HasParkingSpace")
                        .HasColumnType("boolean")
                        .HasColumnName("has_parking_space");

                    b.Property<bool>("IsAnimalFriendly")
                        .HasColumnType("boolean")
                        .HasColumnName("is_animal_friendly");

                    b.Property<bool>("IsFurnished")
                        .HasColumnType("boolean")
                        .HasColumnName("is_furnished");

                    b.Property<int?>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.Property<short>("NumberOfRooms")
                        .HasColumnType("smallint")
                        .HasColumnName("number_of_rooms");

                    b.HasKey("Id");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("apartments", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric")
                        .HasColumnName("latitude");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric")
                        .HasColumnName("longitude");

                    b.Property<string>("Place")
                        .HasColumnType("text")
                        .HasColumnName("place");

                    b.HasKey("Id");

                    b.ToTable("locations", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressId")
                        .HasColumnType("integer")
                        .HasColumnName("address_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("password");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("telephone_number");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("users", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Announcement", b =>
                {
                    b.HasOne("Rentful.Domain.Entities.Apartment", "Apartment")
                        .WithOne()
                        .HasForeignKey("Rentful.Domain.Entities.Announcement", "ApartmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Apartment", b =>
                {
                    b.HasOne("Rentful.Domain.Entities.Location", "Location")
                        .WithOne()
                        .HasForeignKey("Rentful.Domain.Entities.Apartment", "LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.User", b =>
                {
                    b.HasOne("Rentful.Domain.Entities.Address", "Address")
                        .WithOne()
                        .HasForeignKey("Rentful.Domain.Entities.User", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
