﻿// <auto-generated />
using System;
using APITemplate.DataAccess.Concrete.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APITemplate.DataAccess.Migrations
{
    [DbContext(typeof(APITemplateContext))]
    partial class APITemplateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APITemplate.Entity.Poco.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedTime = new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(693),
                            IsActive = true,
                            Name = "Admin",
                            UpdatedTime = new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(694)
                        },
                        new
                        {
                            Id = 2,
                            AddedTime = new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(696),
                            IsActive = true,
                            Name = "Çalışan",
                            UpdatedTime = new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(697)
                        });
                });

            modelBuilder.Entity("APITemplate.Entity.Poco.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedTime = new DateTime(2024, 8, 28, 13, 41, 19, 268, DateTimeKind.Local).AddTicks(9747),
                            Email = "admin@gmail.com",
                            Image = "string",
                            IsActive = true,
                            LastName = "Admin",
                            Name = "Admin",
                            Password = "123",
                            UpdatedTime = new DateTime(2024, 8, 28, 13, 41, 19, 268, DateTimeKind.Local).AddTicks(9760)
                        });
                });

            modelBuilder.Entity("APITemplate.Entity.Poco.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedTime = new DateTime(2024, 8, 28, 13, 41, 19, 269, DateTimeKind.Local).AddTicks(3344),
                            IsActive = true,
                            RoleId = 1,
                            UpdatedTime = new DateTime(2024, 8, 28, 13, 41, 19, 269, DateTimeKind.Local).AddTicks(3348),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("APITemplate.Entity.Poco.UserRole", b =>
                {
                    b.HasOne("APITemplate.Entity.Poco.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("APITemplate.Entity.Poco.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("APITemplate.Entity.Poco.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("APITemplate.Entity.Poco.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
