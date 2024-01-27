using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Entity.Bus.Repository
{
    internal interface IBusRepository
    {
        Task AddBus(Bus bus);
        Task<List<Bus>> GetBuses(Enum.Type type);
        Task<Bus> GetBus(int busId);
        Task<Bus> GetBusByTrip(int busId);
        Task SaveChanges();
    }
}
