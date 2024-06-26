﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniquomeApp.EfCore;

#nullable disable

namespace UniquomeApp.EfCore.Migrations
{
    [DbContext(typeof(UniquomeDbContext))]
    [Migration("20220830003644_001.002")]
    partial class _001002
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniquomeApp.Domain.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Country")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Institution")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Position")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("UniquomeApp.Domain.NewsletterRegistration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Instant>("AcceptedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Instant?>("RemovedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("NewsletterRegistration");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Organism", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Organism");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Peptide", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("FirstLocation")
                        .HasColumnType("integer");

                    b.Property<long>("InUniquomeProteinId")
                        .HasColumnType("bigint");

                    b.Property<string>("Sequence")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.HasIndex("InUniquomeProteinId");

                    b.ToTable("Peptide");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Protein", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Gene")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<long>("InProteomeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<short>("ProteinExistence")
                        .HasColumnType("smallint");

                    b.Property<int>("ProteinStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Sequence")
                        .IsRequired()
                        .HasMaxLength(100000)
                        .HasColumnType("character varying(100000)");

                    b.Property<int>("SequenceLength")
                        .HasColumnType("integer");

                    b.Property<short>("SequenceVersion")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("InProteomeId");

                    b.ToTable("Protein");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Proteome", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Proteome");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Uniquome", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<Instant>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Uniquome");
                });

            modelBuilder.Entity("UniquomeApp.Domain.UniquomeProtein", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("ForProteinId")
                        .HasColumnType("bigint");

                    b.Property<long>("ForUniquomeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ForProteinId");

                    b.HasIndex("ForUniquomeId");

                    b.ToTable("UniquomeProtein");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Peptide", b =>
                {
                    b.HasOne("UniquomeApp.Domain.UniquomeProtein", "InUniquomeProtein")
                        .WithMany("Peptides")
                        .HasForeignKey("InUniquomeProteinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InUniquomeProtein");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Protein", b =>
                {
                    b.HasOne("UniquomeApp.Domain.Proteome", "InProteome")
                        .WithMany("Proteins")
                        .HasForeignKey("InProteomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InProteome");
                });

            modelBuilder.Entity("UniquomeApp.Domain.UniquomeProtein", b =>
                {
                    b.HasOne("UniquomeApp.Domain.Protein", "ForProtein")
                        .WithMany()
                        .HasForeignKey("ForProteinId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UniquomeApp.Domain.Uniquome", "ForUniquome")
                        .WithMany("Proteins")
                        .HasForeignKey("ForUniquomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ForProtein");

                    b.Navigation("ForUniquome");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Proteome", b =>
                {
                    b.Navigation("Proteins");
                });

            modelBuilder.Entity("UniquomeApp.Domain.Uniquome", b =>
                {
                    b.Navigation("Proteins");
                });

            modelBuilder.Entity("UniquomeApp.Domain.UniquomeProtein", b =>
                {
                    b.Navigation("Peptides");
                });
#pragma warning restore 612, 618
        }
    }
}
