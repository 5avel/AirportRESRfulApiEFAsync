using AirportRESRfulApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace AirportRESRfulApi.DAL
{
    public class AirportContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Departure> Departures { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<PlaneType> PlaneTypes { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Stewardess> Stewardesses { get; set; }

        public AirportContext( DbContextOptions<AirportContext> options, IConfiguration configuration = null) : base(options)
        {
            this.configuration = configuration;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (configuration != null)
            {
                optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("AirportMSSQLlocaldb"));
            }
            else
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AirportDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    FlightNumber = "QW11",
                    DeparturePoint = "London",
                    DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                    DestinationPoint = "Ukraine",
                    ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5),
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "QW12",
                    DeparturePoint = "Ukraine",
                    DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                    DestinationPoint = "London",
                    ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5),
                }, new Flight
                {
                    Id = 3,
                    FlightNumber = "QW13",
                    DeparturePoint = "London",
                    DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00"),
                    DestinationPoint = "Dnipro",
                    ArrivalTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") + TimeSpan.FromHours(5),
                });

            builder.Entity<Ticket>().HasData(
                        new Ticket { Id = 1,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 1, Price = 200 },
                        new Ticket { Id = 2,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 2, Price = 200 },
                        new Ticket { Id = 3,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 3, Price = 200 },
                        new Ticket { Id = 4,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 4, Price = 200 },
                        new Ticket { Id = 5,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 5, Price = 200 },
                        new Ticket { Id = 6,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 6, Price = 200 },
                        new Ticket { Id = 7,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 7, Price = 200 },
                        new Ticket { Id = 8,  FlightId = 1, FlightNumber = "QW11", IsSold = false, PlaseNumber = 8, Price = 200 },
                        new Ticket { Id = 9,  FlightId = 2, FlightNumber = "KJ76", IsSold = false, PlaseNumber = 1, Price = 200 },
                        new Ticket { Id = 10, FlightId = 2, FlightNumber = "KJ76", IsSold = false, PlaseNumber = 2, Price = 200 },
                        new Ticket { Id = 11, FlightId = 2, FlightNumber = "KJ76", IsSold = false, PlaseNumber = 3, Price = 200 },
                        new Ticket { Id = 12, FlightId = 2, FlightNumber = "KJ76", IsSold = false, PlaseNumber = 4, Price = 200 },
                        new Ticket { Id = 13, FlightId = 2, FlightNumber = "KJ76", IsSold = false, PlaseNumber = 5, Price = 200 },
                        new Ticket { Id = 14, FlightId = 2, FlightNumber = "KJ76", IsSold = false, PlaseNumber = 6, Price = 200 },
                        new Ticket { Id = 15, FlightId = 3, FlightNumber = "ER86", IsSold = false, PlaseNumber = 1, Price = 200 },
                        new Ticket { Id = 16, FlightId = 3, FlightNumber = "ER86", IsSold = false, PlaseNumber = 2, Price = 200 },
                        new Ticket { Id = 17, FlightId = 3, FlightNumber = "ER86", IsSold = false, PlaseNumber = 3, Price = 200 },
                        new Ticket { Id = 18, FlightId = 3, FlightNumber = "ER86", IsSold = false, PlaseNumber = 4, Price = 200 },
                        new Ticket { Id = 19, FlightId = 3, FlightNumber = "ER86", IsSold = false, PlaseNumber = 5, Price = 200 },
                        new Ticket { Id = 20, FlightId = 3, FlightNumber = "ER86", IsSold = false, PlaseNumber = 6, Price = 200 }
                       );

            builder.Entity<Departure>().HasData(
                new Departure { Id = 1, FlightId = 1, FlightNumber = "QW11", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") },
                new Departure { Id = 2, FlightId = 2, FlightNumber = "KJ76", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") },
                new Departure { Id = 3, FlightId = 3, FlightNumber = "ER86", DepartureTime = Convert.ToDateTime("2018-07-13T08:22:56.6404304+03:00") }
                );

            builder.Entity<Crew>().HasData(
                new Crew { Id = 1, DepartureId = 1 },
                new Crew { Id = 2, DepartureId = 2 },
                new Crew { Id = 3, DepartureId = 3 }
                );
            builder.Entity<Pilot>().HasData(
                new Pilot { Id = 1, CrewId = 1, FirstName = "Ivan", LastName = "Ivanov", Experience = 3, Birthday = new DateTime(1987, 1, 24)},
                new Pilot { Id = 2, CrewId = 2, FirstName = "Peta", LastName = "Penhjd", Experience = 8, Birthday = new DateTime(1987, 1, 24) }
                //new Pilot { Id = 3, CrewId = 3, FirstName = "Max", LastName = "Maximov", Experience = 6, Birthday = new DateTime(1987, 1, 24) }
                );
            builder.Entity<Stewardess>().HasData(
                new Stewardess { Id = 1, CrewId = 1, FirstName = "Ivana", LastName = "Ivanova", Birthday = new DateTime(1987, 1, 24) },
                new Stewardess { Id = 2, CrewId = 2, FirstName = "Petra", LastName = "Penhjd", Birthday = new DateTime(1987, 1, 24) },
                new Stewardess { Id = 3, CrewId = 3, FirstName = "Maxima", LastName = "Maximova", Birthday = new DateTime(1987, 1, 24) },
                new Stewardess { Id = 4, CrewId = 1, FirstName = "Ira", LastName = "Ivanova", Birthday = new DateTime(1987, 1, 24) },
                new Stewardess { Id = 5, CrewId = 2, FirstName = "Lena", LastName = "Penhjd", Birthday = new DateTime(1987, 1, 24) }                   
                );
            builder.Entity<Plane>().HasData(
                new Plane { Id = 1, DepartureId = 1, PlaneTypeId = 1, Name = "dfg4456", ReleaseDate = new DateTime(1995, 1, 22) },
                new Plane { Id = 2, DepartureId = 2, PlaneTypeId = 2, Name = "QQWS1298", ReleaseDate = new DateTime(1995, 1, 22) }
                
                );
            builder.Entity<PlaneType>().HasData(
                new PlaneType { Id = 1, Model = "AN140", Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(200, 0, 0, 0) },
                new PlaneType { Id = 2, Model = "IL235",  Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(250, 0, 0, 0) },
                new PlaneType { Id = 3, Model = "A380",  Capacity = 5000, Seats = 23, Range = 2345, ServiceLife = new TimeSpan(300, 0, 0, 0) }
                );
        }
    }
}