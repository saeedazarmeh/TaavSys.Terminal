using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Log.Enum;

namespace Terminal.Entity.Log
{
    internal class Log
    {
        public Log(int tripId, int ticketId, Status status, decimal catched, decimal refunds)
        {
            TripId = tripId;
            TicketId = ticketId;
            Status = status;
            Catched = catched;
            Refunds = refunds;
        }

        public int Id { get; set; }
        public int TripId { get; set; }
        public int TicketId { get; set; }
        public Status Status { get; set; }
        public decimal Catched { get; set; }
        public decimal Refunds { get; set; }
        public static decimal TotalBenefit(List<Log> logs)
        {
            decimal totalCatched = 0;
            decimal totalRefunds = 0;
            foreach (Log log in logs)
            {
                totalCatched += log.Catched;
                totalRefunds += log.Refunds;
            }
            return totalCatched + totalRefunds;
        }
    }
}
