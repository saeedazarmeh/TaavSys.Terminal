using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Entity.Trip.Repository
{
    internal interface ITripRepositpry
    {
        Task AddTrip(Trip trip);
        Task<List<Trip>> GetTrips();
        Task<Trip> GetTrip(int tripId);
        Task<Trip> GetTripByTickets(int tripId);
        Task SaveChanges();
    }
}
