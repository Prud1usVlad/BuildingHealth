using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BuildingHealth.Core.Models;

namespace BuildingHealth.DAL
{
    public partial class BuildingHealthDBContext : DbContext
    {
        public BuildingHealthDBContext()
        {
        }

        public BuildingHealthDBContext(DbContextOptions<BuildingHealthDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Architect> Architects { get; set; } = null!;
        public virtual DbSet<Builder> Builders { get; set; } = null!;
        public virtual DbSet<BuildingProject> BuildingProjects { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<MainCostructionState> MainCostructionStates { get; set; } = null!;
        public virtual DbSet<SensorsResponse> SensorsResponses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Admin)
                    .HasForeignKey<Admin>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_User");
            });

            modelBuilder.Entity<Architect>(entity =>
            {
                entity.ToTable("Architect");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Architect)
                    .HasForeignKey<Architect>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Architect_User");
            });

            modelBuilder.Entity<Builder>(entity =>
            {
                entity.ToTable("Builder");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Architect)
                    .WithMany(p => p.Builders)
                    .HasForeignKey(d => d.ArchitectId)
                    .HasConstraintName("FK_Builder_Architect");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Builder)
                    .HasForeignKey<Builder>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Builder_User");
            });

            modelBuilder.Entity<BuildingProject>(entity =>
            {
                entity.ToTable("BuildingProject");

                entity.Property(e => e.HandoverDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.WorkStartedDate).HasColumnType("date");

                entity.HasOne(d => d.Architect)
                    .WithMany(p => p.BuildingProjects)
                    .HasForeignKey(d => d.ArchitectId)
                    .HasConstraintName("FK_BuildingProject_Architect");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.BuildingProject)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BuildingProjectId)
                    .HasConstraintName("FK_Comment_BuildingProject");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<MainCostructionState>(entity =>
            {
                entity.ToTable("MainCostructionState");

                entity.HasOne(d => d.SensorsResponse)
                    .WithMany(p => p.MainCostructionStates)
                    .HasForeignKey(d => d.SensorsResponseId)
                    .HasConstraintName("FK_MainCostructionState_SensorsResponse");
            });

            modelBuilder.Entity<SensorsResponse>(entity =>
            {
                entity.ToTable("SensorsResponse");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.BuildingProject)
                    .WithMany(p => p.SensorsResponses)
                    .HasForeignKey(d => d.BuildingProjectId)
                    .HasConstraintName("FK_SensorsResponse_BuildingProject")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.SecondName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
