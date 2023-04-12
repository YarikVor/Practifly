﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PractiFly.DbContextUtility.Context;

#nullable disable

namespace PractiFly.DbContextUtility.Migrations
{
    [DbContext(typeof(ErrorContext))]
    [Migration("20230412162401_ErrorMigration")]
    partial class ErrorMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PractiFly.DbContextUtility.Context.ErrorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ErrorType")
                        .HasColumnType("integer");

                    b.Property<string>("ExceptionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("GeneratedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ErrorEntities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ErrorType = 0,
                            ExceptionName = "Unknown error",
                            GeneratedAt = new DateTime(2023, 4, 12, 19, 24, 0, 930, DateTimeKind.Local).AddTicks(5317),
                            Message = "Unknown error",
                            StackTrace = "Unknown error"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
