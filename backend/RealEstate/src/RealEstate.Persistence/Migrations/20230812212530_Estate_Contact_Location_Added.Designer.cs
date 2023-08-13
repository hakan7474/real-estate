﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RealEstate.Persistence.Context;

#nullable disable

namespace RealEstate.Persistence.Migrations
{
    [DbContext(typeof(RealEstateContext))]
    [Migration("20230812212530_Estate_Contact_Location_Added")]
    partial class Estate_Contact_Location_Added
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("Estate_SysCode_seq")
                .HasMin(1L)
                .HasMax(9223372036854775807L);

            modelBuilder.HasSequence("TypeDetail_SysCode_seq")
                .HasMin(1L)
                .HasMax(9223372036854775807L);

            modelBuilder.HasSequence("Types_SysCode_seq")
                .HasMin(1L)
                .HasMax(9223372036854775807L);

            modelBuilder.Entity("RealEstate.Domain.Entities.Estate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1)
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime?>("BuildingDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("EstateCode")
                        .HasColumnType("text");

                    b.Property<string>("EstateName")
                        .HasColumnType("text");

                    b.Property<Guid>("EstateTypeId")
                        .HasColumnType("uuid");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("integer");

                    b.Property<decimal?>("GrossArea")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<decimal?>("NetArea")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("RoomCount")
                        .HasColumnType("text");

                    b.Property<string>("SysCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValueSql("'EST-'::text || nextval('\"Estate_SysCode_seq\"')");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("EstateTypeId");

                    b.ToTable("Estate");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.TypeDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1)
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("ItemCode")
                        .HasColumnType("text");

                    b.Property<string>("ItemDescription")
                        .HasColumnType("text");

                    b.Property<string>("ItemName")
                        .HasColumnType("text");

                    b.Property<int>("OrderIndex")
                        .HasColumnType("integer");

                    b.Property<string>("SysCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValueSql("'TYP-'::text || nextval('\"TypeDetail_SysCode_seq\"')");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("TypeDetail");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Types", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(1)
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("SysCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValueSql("'TYP-'::text || nextval('\"Types_SysCode_seq\"')");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<string>("TypeCode")
                        .HasColumnType("text");

                    b.Property<string>("TypeDescription")
                        .HasColumnType("text");

                    b.Property<string>("TypeName")
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Estate", b =>
                {
                    b.HasOne("RealEstate.Domain.Entities.TypeDetail", "EstateType")
                        .WithMany()
                        .HasForeignKey("EstateTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("RealEstate.Domain.Entities.Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("EstateId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("Address")
                                .HasColumnType("text");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("Country")
                                .HasColumnType("text");

                            b1.Property<string>("District")
                                .HasColumnType("text");

                            b1.Property<string>("Location")
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .HasColumnType("text");

                            b1.HasKey("EstateId");

                            b1.ToTable("Estate");

                            b1.WithOwner()
                                .HasForeignKey("EstateId");
                        });

                    b.Navigation("Contact");

                    b.Navigation("EstateType");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.TypeDetail", b =>
                {
                    b.HasOne("RealEstate.Domain.Entities.Types", "Types")
                        .WithMany("TypeDetails")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Types");
                });

            modelBuilder.Entity("RealEstate.Domain.Entities.Types", b =>
                {
                    b.Navigation("TypeDetails");
                });
#pragma warning restore 612, 618
        }
    }
}