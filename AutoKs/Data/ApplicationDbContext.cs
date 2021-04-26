using AutoKs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoKs.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Vehicle>()
               .HasMany(d => d.Ratings)
               .WithOne(d => d.Vehicle)
               .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(builder);
        }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<VhlDetails> VhlDetails { get; set; }
        public DbSet<MyFavourite> MyFavourite { get; set; }
        public DbSet<CarList> CarsList { get; set; }
    }
}
