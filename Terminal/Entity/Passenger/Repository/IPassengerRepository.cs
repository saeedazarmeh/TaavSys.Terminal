using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Entity.Passenger.Repository
{
    internal interface IPassengerRepository
    {
        Task AddPassenger(Passenger passenger);
        Task<Passenger> GetPassenger(int passengerId);
        Task<Passenger> GetPassengerByPhone(string phone);
        Task<Passenger> GetPassengerWithTicketsAndTrip(string phone);
        Task SaveChanges();
    }
}
