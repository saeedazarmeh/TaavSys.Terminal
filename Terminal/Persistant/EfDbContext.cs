using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Bus;
using Terminal.Entity.Log;
using Terminal.Entity.Passenger;
using Terminal.Entity.Trip;

namespace Terminal.Persistant
{
    internal class EfDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Db_Terminal;Integrated Security=True");
        }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.ApplyConfigurationsFromAssembly(typeof(EfDbContext).Assembly);
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<Trip>()
                .HasDiscriminator<int>("TripDiscrimination")
                .HasValue<NormalTrip>(1)
                .HasValue<VIPTrip>(2);

        }
    }
}
