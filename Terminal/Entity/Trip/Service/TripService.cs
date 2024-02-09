using Microsoft.EntityFrameworkCore;
using Terminal.Entity.Trip.Repository;
using Terminal.Persistant;

namespace Terminal.Entity.Trip.Service
{
    internal class TripService:ITripService
    {
        EfDbContext db=new EfDbContext();
        private readonly ITripRepositpry _tripRepository=new TripRepositpry();
        public bool CheckSeatsBeEmpty(List<int> seatsNumber,HashSet<Ticket> tickets)
        {
            var seats = ConvertTicketsSeatsListToSeatNumbers(tickets);
            foreach (var seat in seatsNumber)
            {
                if (seats.Contains(seat))
                {
                    return false;
                    break;
                }
            }
            return true;
        }
        public List<int> ConvertTicketsSeatsListToSeatNumbers(HashSet<Ticket> tickets)
        {
            List<int> seatNumbers = new List<int>();
            foreach (var ticket in tickets)
            {
                foreach (var seat in ticket.Seats)
                {
                    seatNumbers.Add(seat.SeatNumber);
                }
            }
            return seatNumbers;
        }
        public string ShowSeatDet(int seatNumber, List<Seat> seats)
        {
            string seatDet=seatNumber.ToString();
            if (seatDet.Length == 1) seatDet = "0" + seatDet;
            foreach (var seat in seats)
            {
                if (seat.SeatNumber==(seatNumber))
                {
                    if (seat.SeatType == Enum.SeatType.Bought)
                    {
                        seatDet = "bb";
                    }
                    else
                    {
                        seatDet = "rr";
                    }
                    break;
                }
            }
            return seatDet;
        }
        public List<Seat> ConvertTicketsSeatsListToSeatsList(HashSet<Ticket> tickets)
        {
            List<Seat> seats = new List<Seat>();
            foreach (var ticket in tickets)
            {
                foreach (var seat in ticket.Seats)
                {
                    seats.Add(seat);
                }
            }
            return seats;
        }
        public void DeleteSeats(Ticket ticket)
        {
            db.Seats.RemoveRange(ticket.Seats);
            _tripRepository.SaveChange();

        }
    }
}
