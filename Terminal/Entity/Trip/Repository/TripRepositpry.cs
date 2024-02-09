using Microsoft.EntityFrameworkCore;
using Terminal.Persistant;

namespace Terminal.Entity.Trip.Repository
{
    internal class TripRepositpry: ITripRepositpry
    {
        EfDbContext db=new EfDbContext();
        public async Task AddTrip(Trip trip)
        {
           await db.Trips.AddAsync(trip); 
        }

        public async Task<Trip> GetTrip(int tripId)
        {
            return await db.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
        }

        public async Task<Trip> GetTripByTickets(int tripId)
        {
            return await db.Trips.Include(t => t.Tickets).ThenInclude(t => t.Seats).FirstOrDefaultAsync(t => t.Id == tripId);
        }

        public async Task<List<Trip>> GetTrips()
        {
            return await db.Trips.Include(t=>t.Bus).Where(t=>t.Time>DateTime.Now).ToListAsync();
        }

        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }
        public void SaveChange()
        {
             db.SaveChangesAsync();
        }

        public void SaveSeatUpdat(Seat seat)
        {
             db.Entry(seat).State = EntityState.Modified;
        }

      
        public void SaveTicketUpdat(Ticket ticket)
        {
            db.Entry(ticket).State = EntityState.Modified;
        }
    }
}
