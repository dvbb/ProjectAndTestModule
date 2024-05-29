﻿// <auto-generated />
using System;
using GameManagement.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameManagement.EntityFramework.Migrations
{
    [DbContext(typeof(GameManagementDbContext))]
    partial class GameManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameMenagement.Entities.Character", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("classes")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Nickname")
                        .IsUnique();

                    b.HasIndex("PlayerId");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4c0fad31-4c9a-4880-8f1e-c31f1c4ddc34"),
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5839),
                            Level = 99,
                            Nickname = "Code Man",
                            PlayerId = new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"),
                            classes = "Mage"
                        },
                        new
                        {
                            Id = new Guid("62ce51c8-5baa-4523-9396-25ad886593db"),
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5968),
                            Level = 99,
                            Nickname = "WZ",
                            PlayerId = new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"),
                            classes = "Warrior"
                        },
                        new
                        {
                            Id = new Guid("e7eed720-bc05-4fc9-9ded-cef58deb97ac"),
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5969),
                            Level = 29,
                            Nickname = "asaka",
                            PlayerId = new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"),
                            classes = "Druid"
                        },
                        new
                        {
                            Id = new Guid("22302531-0a2c-45ba-9e3f-030423d8ac8e"),
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5971),
                            Level = 5,
                            Nickname = "MyWon",
                            PlayerId = new Guid("f02a7e5a-0ea1-4c86-8106-cb6de77c4f22"),
                            classes = "Mage"
                        },
                        new
                        {
                            Id = new Guid("d7287182-6ce6-42ba-bf53-96d9d32f5304"),
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5979),
                            Level = 95,
                            Nickname = "TBD",
                            PlayerId = new Guid("f02a7e5a-0ea1-4c86-8106-cb6de77c4f22"),
                            classes = "Wizzard"
                        });
                });

            modelBuilder.Entity("GameMenagement.Entities.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Account")
                        .IsUnique();

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"),
                            Account = "mw2021",
                            AccountType = "Free",
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(4941)
                        },
                        new
                        {
                            Id = new Guid("f02a7e5a-0ea1-4c86-8106-cb6de77c4f22"),
                            Account = "dc2021",
                            AccountType = "Free",
                            DateCreated = new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(4959)
                        });
                });

            modelBuilder.Entity("GameMenagement.Entities.Character", b =>
                {
                    b.HasOne("GameMenagement.Entities.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("GameMenagement.Entities.Player", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}
