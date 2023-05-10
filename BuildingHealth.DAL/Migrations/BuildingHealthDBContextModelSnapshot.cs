﻿// <auto-generated />
using System;
using BuildingHealth.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildingHealth.DAL.Migrations
{
    [DbContext(typeof(BuildingHealthDBContext))]
    partial class BuildingHealthDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BuildingHealth.Core.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Architect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Architect", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Builder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("ArchitectId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ArchitectId");

                    b.ToTable("Builder", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.BuildingProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ArchitectId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HandoverDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("WorkStartedDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ArchitectId");

                    b.ToTable("BuildingProject", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuildingProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.MainCostructionState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompressionLevel")
                        .HasColumnType("int");

                    b.Property<int?>("DeformationLevel")
                        .HasColumnType("int");

                    b.Property<int?>("SensorsResponseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SensorsResponseId");

                    b.ToTable("MainCostructionState", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BuildingProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingProjectId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.SensorsResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuildingProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date");

                    b.Property<int?>("GroundAcidityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("GroundWaterLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingProjectId");

                    b.ToTable("SensorsResponse", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Admin", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.User", "IdNavigation")
                        .WithOne("Admin")
                        .HasForeignKey("BuildingHealth.Core.Models.Admin", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_Admin_User");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Architect", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.User", "IdNavigation")
                        .WithOne("Architect")
                        .HasForeignKey("BuildingHealth.Core.Models.Architect", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_Architect_User");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Builder", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.Architect", "Architect")
                        .WithMany("Builders")
                        .HasForeignKey("ArchitectId")
                        .HasConstraintName("FK_Builder_Architect");

                    b.HasOne("BuildingHealth.Core.Models.User", "IdNavigation")
                        .WithOne("Builder")
                        .HasForeignKey("BuildingHealth.Core.Models.Builder", "Id")
                        .IsRequired()
                        .HasConstraintName("FK_Builder_User");

                    b.Navigation("Architect");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.BuildingProject", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.Architect", "Architect")
                        .WithMany("BuildingProjects")
                        .HasForeignKey("ArchitectId")
                        .HasConstraintName("FK_BuildingProject_Architect");

                    b.Navigation("Architect");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Comment", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.BuildingProject", "BuildingProject")
                        .WithMany("Comments")
                        .HasForeignKey("BuildingProjectId")
                        .HasConstraintName("FK_Comment_BuildingProject");

                    b.HasOne("BuildingHealth.Core.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Comment_User");

                    b.Navigation("BuildingProject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.MainCostructionState", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.SensorsResponse", "SensorsResponse")
                        .WithMany("MainCostructionStates")
                        .HasForeignKey("SensorsResponseId")
                        .HasConstraintName("FK_MainCostructionState_SensorsResponse");

                    b.Navigation("SensorsResponse");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Notification", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.BuildingProject", "BuildingProject")
                        .WithMany()
                        .HasForeignKey("BuildingProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuildingProject");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.SensorsResponse", b =>
                {
                    b.HasOne("BuildingHealth.Core.Models.BuildingProject", "BuildingProject")
                        .WithMany("SensorsResponses")
                        .HasForeignKey("BuildingProjectId")
                        .HasConstraintName("FK_SensorsResponse_BuildingProject");

                    b.Navigation("BuildingProject");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.Architect", b =>
                {
                    b.Navigation("Builders");

                    b.Navigation("BuildingProjects");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.BuildingProject", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("SensorsResponses");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.SensorsResponse", b =>
                {
                    b.Navigation("MainCostructionStates");
                });

            modelBuilder.Entity("BuildingHealth.Core.Models.User", b =>
                {
                    b.Navigation("Admin");

                    b.Navigation("Architect");

                    b.Navigation("Builder");

                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
