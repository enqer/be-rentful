﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rentful.Infrastructure.Persistence;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    [DbContext(typeof(RentfulDbContext))]
    [Migration("20241119202126_LengthTitle")]
    partial class LengthTitle
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("AnnouncementId")
                        .HasColumnType("integer")
                        .HasColumnName("apartment_id");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_added");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId")
                        .IsUnique();

                    b.HasIndex("UserId");

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

                    b.Property<double?>("Deposit")
                        .HasColumnType("double precision")
                        .HasColumnName("deposit");

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

                    b.Property<int>("LocationId")
                        .HasColumnType("integer")
                        .HasColumnName("location_id");

                    b.Property<short>("NumberOfRooms")
                        .HasColumnType("smallint")
                        .HasColumnName("number_of_rooms");

                    b.Property<double>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<double?>("Rent")
                        .HasColumnType("double precision")
                        .HasColumnName("rent");

                    b.HasKey("Id");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("apartments", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AnnouncementId")
                        .HasColumnType("integer")
                        .HasColumnName("apartment_id");

                    b.Property<bool>("IsThumbnail")
                        .HasColumnType("boolean")
                        .HasColumnName("is_thumbnail");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("source");

                    b.HasKey("Id");

                    b.HasIndex("AnnouncementId");

                    b.ToTable("images", "rentful");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<bool>("IsPrecise")
                        .HasColumnType("boolean")
                        .HasColumnName("is_precise");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric")
                        .HasColumnName("latitude");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric")
                        .HasColumnName("longitude");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("province");

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
                        .HasForeignKey("Rentful.Domain.Entities.Announcement", "AnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Rentful.Domain.Entities.User", "User")
                        .WithMany("Announcements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Apartment", b =>
                {
                    b.HasOne("Rentful.Domain.Entities.Location", "Location")
                        .WithOne()
                        .HasForeignKey("Rentful.Domain.Entities.Apartment", "LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Images", b =>
                {
                    b.HasOne("Rentful.Domain.Entities.Apartment", "Apartment")
                        .WithMany("Images")
                        .HasForeignKey("AnnouncementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apartment");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.User", b =>
                {
                    b.HasOne("Rentful.Domain.Entities.Address", "Address")
                        .WithOne()
                        .HasForeignKey("Rentful.Domain.Entities.User", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.Apartment", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Rentful.Domain.Entities.User", b =>
                {
                    b.Navigation("Announcements");
                });
#pragma warning restore 612, 618
        }
    }
}
