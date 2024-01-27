using Microsoft.EntityFrameworkCore;
using Terminal.Persistant;

namespace Terminal.Entity.Bus.Repository
{
    internal class BusRepository : IBusRepository
    {
        EfDbContext db=new EfDbContext();
        public async Task AddBus(Bus bus) 
        {
            await db.Buses.AddAsync(bus);
        }
        public async Task<List<Bus>> GetBuses(Enum.Type type)
        {
            return await db.Buses.Where(b => b.Type == type).ToListAsync();
        }
        public async Task<Bus> GetBus(int busId)
        {
            return await db.Buses.FirstOrDefaultAsync(b => b.Id == busId);
        }
        public async Task<Bus> GetBusByTrip(int busId)
        {
            return await db.Buses.Include(b=>b.Trips).FirstOrDefaultAsync(b => b.Id == busId);
        }
        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }
    }
}
