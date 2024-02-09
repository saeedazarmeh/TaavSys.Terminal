using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Entity.Log.Repository
{
    internal interface ILogRepository
    {
        Task Add(Log log);
        List<Log> GetTripLog(int tripId);
        void SaveChanges();
    }
}
