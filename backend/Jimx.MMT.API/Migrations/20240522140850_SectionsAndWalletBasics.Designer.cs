﻿// <auto-generated />
using System;
using Jimx.MMT.API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Jimx.MMT.API.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240522140850_SectionsAndWalletBasics")]
    partial class SectionsAndWalletBasics
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Jimx.MMT.API.Context.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SectionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SharedAccountId")
                        .HasColumnType("integer");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int?>("WalletId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SharedAccountId");

                    b.HasIndex("WalletId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.SharedAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SharedAccount");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.SharedAccountToUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("SharedAccountId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SharedAccountId");

                    b.ToTable("SharedAccountToUser");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.Wallet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.Category", b =>
                {
                    b.HasOne("Jimx.MMT.API.Context.Section", "Section")
                        .WithMany("Categories")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.Section", b =>
                {
                    b.HasOne("Jimx.MMT.API.Context.SharedAccount", "SharedAccount")
                        .WithMany("Sections")
                        .HasForeignKey("SharedAccountId");

                    b.HasOne("Jimx.MMT.API.Context.Wallet", "Wallet")
                        .WithMany("Sections")
                        .HasForeignKey("WalletId");

                    b.Navigation("SharedAccount");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.SharedAccountToUser", b =>
                {
                    b.HasOne("Jimx.MMT.API.Context.SharedAccount", "SharedAccount")
                        .WithMany("SharedAccountToUsers")
                        .HasForeignKey("SharedAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SharedAccount");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.Section", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.SharedAccount", b =>
                {
                    b.Navigation("Sections");

                    b.Navigation("SharedAccountToUsers");
                });

            modelBuilder.Entity("Jimx.MMT.API.Context.Wallet", b =>
                {
                    b.Navigation("Sections");
                });
#pragma warning restore 612, 618
        }
    }
}