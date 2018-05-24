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
    [Migration("20180524034527_'initial'")]
    partial class initial
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

                    b.Property<string>("Email");

                    b.Property<string>("FullName");

                    b.Property<string>("IPAddress");

                    b.Property<long>("ModifiedBy");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("AkunUser");
                });
#pragma warning restore 612, 618
        }
    }
}
