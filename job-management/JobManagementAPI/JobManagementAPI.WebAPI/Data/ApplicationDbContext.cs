﻿using JobManagementAPI.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobManagementAPI.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // RefreshToken configuration
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Token).IsRequired();
                entity.Property(e => e.ExpiryDate).IsRequired();
                entity.Property(e => e.IsRevoked).IsRequired().HasDefaultValue(false);
                
                entity.HasOne(e => e.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Job configuration
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Department).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Location).IsRequired().HasMaxLength(100);
                entity.Property(e => e.JobType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.CreatedBy).IsRequired().HasMaxLength(50);
                
                // Add index on Department for faster lookups
                entity.HasIndex(e => e.Department);
                // Add index on IsActive for faster lookups
                entity.HasIndex(e => e.IsActive);
            });
        }
    }
}