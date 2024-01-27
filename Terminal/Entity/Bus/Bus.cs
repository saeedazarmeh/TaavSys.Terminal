using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Bus.Repository;

namespace Terminal.Entity.Bus
{
    internal class Bus
    {
        
        public Bus(Enum.Type type, string name,string driverName)
        {
            Type = type;
            Name = name;
            DriverName = driverName;
        }

        public int Id { get;private set; }
        public string Name { get;private set; }
        public string? DriverName { get;private set; }
        public Enum.Type Type { get;private set; }
        public HashSet<Trip.Trip> Trips = new HashSet<Trip.Trip>();

        public void AddTrip(Trip.Trip trip)
        {
            Trips.Add(trip);
        }
  

    }

}
