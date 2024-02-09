using Terminal.Persistant;

namespace Terminal.Entity.Log.Repository
{
    internal class LogRepository : ILogRepository
    {
        EfDbContext db=new EfDbContext();
        public async Task Add(Log log)
        {
            await db.Logs.AddAsync(log);
        }

        public List<Log> GetTripLog(int tripId)
        {
            return db.Logs.Where(l=>l.TripId==tripId).ToList(); 
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
