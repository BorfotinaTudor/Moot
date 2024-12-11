﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Moot.Data;

#nullable disable

namespace Moot.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20241211094418_noidate")]
    partial class noidate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Moot.Models.Agent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("AgentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("ID");

                    b.ToTable("Agent");
                });

            modelBuilder.Entity("Moot.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClientAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Moot.Models.Neighborhood", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Neighborhood");
                });

            modelBuilder.Entity("Moot.Models.Offer", b =>
                {
                    b.Property<int>("OfferID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OfferID"));

                    b.Property<int?>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OfferDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PropertyID")
                        .HasColumnType("int");

                    b.HasKey("OfferID");

                    b.HasIndex("ClientID");

                    b.HasIndex("PropertyID");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("Moot.Models.Owner", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Owner");
                });

            modelBuilder.Entity("Moot.Models.Property", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("NeighborhoodID")
                        .HasColumnType("int");

                    b.Property<int?>("OwnerID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PropertyType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("NeighborhoodID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("Moot.Models.PublishedProperty", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AgentID")
                        .HasColumnType("int");

                    b.Property<int>("PropertyID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AgentID");

                    b.HasIndex("PropertyID");

                    b.ToTable("PublishedProperty");
                });

            modelBuilder.Entity("Moot.Models.Offer", b =>
                {
                    b.HasOne("Moot.Models.Client", "Client")
                        .WithMany("Offers")
                        .HasForeignKey("ClientID");

                    b.HasOne("Moot.Models.Property", "Properties")
                        .WithMany("Offers")
                        .HasForeignKey("PropertyID");

                    b.Navigation("Client");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Moot.Models.Property", b =>
                {
                    b.HasOne("Moot.Models.Neighborhood", "Neighborhood")
                        .WithMany()
                        .HasForeignKey("NeighborhoodID");

                    b.HasOne("Moot.Models.Owner", "Owner")
                        .WithMany("Properties")
                        .HasForeignKey("OwnerID");

                    b.Navigation("Neighborhood");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Moot.Models.PublishedProperty", b =>
                {
                    b.HasOne("Moot.Models.Agent", "Agent")
                        .WithMany("PublishedProperties")
                        .HasForeignKey("AgentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Moot.Models.Property", "Property")
                        .WithMany("PublishedProperties")
                        .HasForeignKey("PropertyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agent");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("Moot.Models.Agent", b =>
                {
                    b.Navigation("PublishedProperties");
                });

            modelBuilder.Entity("Moot.Models.Client", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("Moot.Models.Owner", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("Moot.Models.Property", b =>
                {
                    b.Navigation("Offers");

                    b.Navigation("PublishedProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
