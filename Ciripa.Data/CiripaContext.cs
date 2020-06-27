using Ciripa.Data.Entities;
using Ciripa.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Ciripa.Data
{
    public class CiripaContext : DbContext
    {
        public DbSet<Kid> Kids { get; set; }
        public DbSet<Presence> Presences { get; set; }
        public DbSet<ExtraPresence> ExtraPresences { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<SimpleContract> Contracts { get; set; }
        public DbSet<SpecialContract> SpecialContracts { get; set; }

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
            modelBuilder.ApplyConfiguration(new DateConfiguration<ExtraPresence>());
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

            modelBuilder
                .Entity<ExtraPresence>()
                .HasOne(x => x.Kid);
            modelBuilder
                .Entity<ExtraPresence>()
                .HasOne(x => x.SpecialContract);


            var settingsSeedData = new Settings(1, 6.0m, 7.0m, 200.0m);
            modelBuilder
                .Entity<Settings>()
                .HasData(settingsSeedData);

            modelBuilder
                .Entity<Invoice>()
                .HasOne(x => x.Kid);

            modelBuilder.Entity<Contract>()
                .HasDiscriminator<int>("ContractType")
                .HasValue<SimpleContract>(1)
                .HasValue<SpecialContract>(2);

            modelBuilder
                .Entity<SimpleContract>()
                .HasData(ContractsSeedData());

            modelBuilder
                .Entity<SpecialContract>()
                .HasData(SpecialContractsSeedData());
        }

        private List<SimpleContract> ContractsSeedData()
        {
            return new List<SimpleContract>
            {
                new SimpleContract {
                    Id = 1,
                    Description = "Contratto 4 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 4m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 500
                },
                new SimpleContract {
                    Id = 2,
                    Description = "Contratto 4,5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 4.5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 525
                },
                new SimpleContract {
                    Id = 3,
                    Description = "Contratto 5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 550
                },
                new SimpleContract {
                    Id = 4,
                    Description = "Contratto 5,5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 5.5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 575
                },
                new SimpleContract {
                    Id = 5,
                    Description = "Contratto 6 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 6m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue =600
                },
                new SimpleContract {
                    Id = 6,
                    Description = "Contratto 6,5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 6.5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue =625
                },
                new SimpleContract {
                    Id = 7,
                    Description = "Contratto 7 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 7m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 650
                },
                new SimpleContract {
                    Id = 8,
                    Description = "Contratto 7,5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 7.5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 675
                },
                new SimpleContract {
                    Id = 9,
                    Description = "Contratto 8 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 8m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 700
                },
                new SimpleContract {
                    Id = 10,
                    Description = "Contratto 8,5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 8.5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 725
                },
                new SimpleContract {
                    Id = 11,
                    Description = "Contratto 9 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 9m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 750
                },
                new SimpleContract {
                    Id = 12,
                    Description = "Contratto 9,5 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 9.5m,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 775
                },
                new SimpleContract {
                    Id = 13,
                    Description = "Contratto 10 ore/dì",
                    MonthlyContract = false,
                    DailyHours = 10,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 800
                },
                new SimpleContract {
                    Id = 14,
                    Description = "Contratto orario",
                    MonthlyContract = true,
                    DailyHours = 0,
                    MonthlyHours = 43,
                    HourCost = 7m,
                    ExtraHourCost = 8m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 300
                }
            };
        }
        private List<SpecialContract> SpecialContractsSeedData()
        {
            return new List<SpecialContract>
            {
                new SpecialContract
                {
                    Id = 15,
                    Description = "Babysitting",
                    MonthlyContract = false,
                    WeeklyContract = false,
                    DailyHours = 0,
                    MonthlyHours = 0,
                    HourCost = 10.0m,
                    ExtraHourCost = 10.0m,
                    StartTime = new DateTime(2020, 1, 1, 0, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 22, 59, 59),
                    MinContractValue = 0
                },
                new SpecialContract
                {
                    Id = 16,
                    Description = "Colonie settimanali",
                    MonthlyContract = false,
                    WeeklyContract = true,
                    DailyHours = 0,
                    MonthlyHours = 0,
                    HourCost = 7.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 18, 0, 0),
                    MinContractValue = 150
                },
                new SpecialContract
                {
                    Id = 17,
                    Description = "Aiuto compiti",
                    MonthlyContract = false,
                    WeeklyContract = false,
                    DailyHours = 0,
                    MonthlyHours = 0,
                    HourCost = 8.0m,
                    ExtraHourCost = 8.0m,
                    StartTime = new DateTime(2020, 1, 1, 0, 0, 0),
                    EndTime = new DateTime(2020, 1, 1, 22, 59, 59),
                    MinContractValue = 0
                }
            };
        }
    }
}