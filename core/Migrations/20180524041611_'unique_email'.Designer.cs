﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using core.Models;

namespace core.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20180524041611_'unique_email'")]
    partial class unique_email
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-preview2-30571")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("core.Models.AkunUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(200);

                    b.Property<string>("FullName")
                        .HasMaxLength(350);

                    b.Property<string>("IPAddress");

                    b.Property<long>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Password")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("AkunUser");
                });
#pragma warning restore 612, 618
        }
    }
}
