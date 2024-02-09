using Microsoft.EntityFrameworkCore;
using Terminal.Persistant;

namespace Terminal.Entity.Passenger.Repository
{
    internal class PassengerRepository : IPassengerRepository
    {
        EfDbContext db=new EfDbContext();
        public async Task AddPassenger(Passenger passenger)
        {
            await db.Passengers.AddAsync(passenger);
        }
        public async Task<Passenger> GetPassenger(int passengerId)
        {
            return await db.Passengers.FirstOrDefaultAsync(p => p.Id == passengerId);
        }

        public async Task<Passenger> GetPassengerByPhone(string phone)
        {
            return await db.Passengers.FirstOrDefaultAsync(p => p.PhoneNumber == phone);
        }

        public async Task<Passenger> GetPassengerWithTicketsAndTrip(string phone)
        {
            return await db.Passengers.Include(p => p.Tickets).ThenInclude(t => t.Seats)
                .Include(p=>p.Tickets).ThenInclude(t=>t.Trip).FirstOrDefaultAsync(p => p.PhoneNumber == phone );
        }
        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }
    }
}
