using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Bus;
using Terminal.Entity.Log;
using Terminal.Entity.Passenger;
using Terminal.Entity.Trip;

namespace Terminal.Common
{
    internal static class Utility
    {
        internal static void ShowBuses(List<Bus> buses)
        {
            foreach (var busItem in buses)
            {
                Console.WriteLine($"Id:{busItem.Id} Type:{busItem.Type} Driver:{busItem.DriverName}\n");
            }
        }
        internal static void ShowTrips(List<Trip> trips)
        {
            var i = 0;
            foreach (var trip in trips)
            {
                i++;
                Console.WriteLine($"number:{i} TripId:{trip.Id} \nBusName:{trip.Bus.Name} BusType:{trip.Bus.Type} BusDriverName:{trip.Bus.DriverName}\n origin:{trip.Route.Origin} destination:{trip.Route.Destination}\n");
            }
        }
        internal static void ShowPassengerTickets(Passenger passenger)
        {
            int i = 0;
            foreach (var ticket in passenger.Tickets)
            {
                if (ticket.Seats.Count > 0)
                {
                    i++;
                    Console.WriteLine($"number:{i} TicketId:{ticket.Id} Origin:{ticket.Trip.Route.Origin} " +
                        $" destination:{ticket.Trip.Route.Destination} Time:{ticket.Trip.Time} \nSeatNumbers:");
                    foreach (var seat in ticket.Seats) { Console.WriteLine(seat.SeatNumber + "  "); }
                }
            }
        }
        internal static void ShowTrioResult(List<Log> logs,Trip choosedTrip)
        {
            var totalProfit = Log.TotalBenefit(logs);
            var emptySeatNum = choosedTrip.CalEmptySeatNembers();
            var numberOfBookedCanceled = logs.Where(l => l.Status == Terminal.Entity.Log.Enum.Status.BookedCancel).Count();
            var numberOfBougthCanceled = logs.Where(l => l.Status == Terminal.Entity.Log.Enum.Status.BougthCancel).Count();
            Console.WriteLine($"TotalP)rofit:{totalProfit} EmptySeatNuimbers:{emptySeatNum} " +
            $"CanceledTickets:Booked{numberOfBookedCanceled} Bougth:{numberOfBougthCanceled}");
        }
    }
}
