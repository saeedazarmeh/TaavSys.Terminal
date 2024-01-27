using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Entity.Trip.Service
{
    internal interface ITripService
    {
        bool CheckSeatsBeEmpty(List<int> seatsNumber, HashSet<Ticket> tickets);
        List<int> ConvertTicketsSeatsListToSeatNumbers(HashSet<Ticket> tickets);
        string ShowSeatDet(int seatNumber, List<Seat> seats);
        List<Seat> ConvertTicketsSeatsListToSeatsList(HashSet<Ticket> tickets);
    }
}
