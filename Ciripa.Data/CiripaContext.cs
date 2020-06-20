﻿using Ciripa.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ciripa.Data
{
    public class CiripaContext : DbContext
    {
        public DbSet<Kid> Kids { get; set; }
        public DbSet<Presence> Presences { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public CiripaContext()
        {
        }

        public CiripaContext(DbContextOptions<CiripaContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=ciripa.db", b => b.MigrationsAssembly("Ciripa.Web"));
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DateConfiguration<Presence>());
            modelBuilder.ApplyConfiguration(new DateConfiguration<Invoice>());

            modelBuilder
                .Entity<Kid>()
                .HasMany(x => x.PresencesList);
            modelBuilder
                .Entity<Kid>()
                .OwnsOne(x => x.Parent1);
            modelBuilder
                .Entity<Kid>()
                .OwnsOne(x => x.Parent2);

            modelBuilder
                .Entity<Presence>()
                .HasOne(x => x.Kid);

            var settingsSeedData = new Settings(1, 6.0m, 7.0m, 200.0m);
            modelBuilder
                .Entity<Settings>()
                .HasData(settingsSeedData);

            modelBuilder
                .Entity<Invoice>()
                .HasOne(x => x.Kid);

            modelBuilder
                .Entity<Contract>();
        }
        
    }
}