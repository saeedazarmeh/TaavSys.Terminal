using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Passenger;

namespace Terminal.Entity.Trip
{
    internal class Ticket
    {
        public Ticket(int passengerId, decimal totalPrice, decimal payment)
        {
            PassengerId = passengerId;
            TotalPrice = totalPrice;
            Payment = payment;
        }

        public int Id { get; private set; }
        public decimal TotalPrice { get; private set; }
        public decimal Payment { get; private set; }
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        public int PassengerId { get; private set; }
        public Passenger.Passenger Passenger { get; private set; }
        public HashSet<Seat> Seats { get; set; } = new HashSet<Seat>();
        public void AddSeats(List<Seat> seats)
        {
            seats.ForEach(s => Seats.Add(s));
        }
        public void ChangePaymentTicket(decimal newPayment)
        {
            Payment = newPayment;
        }
        public List<Seat> ConverSeatNumbersToSeatList(List<int> seatNumbers, Enum.SeatType seatType)
        {
            List<Seat> seats = new List<Seat>();
            seatNumbers.ForEach(s => seats.Add(new Seat(seatType, s)));
            return seats;
        }
        
     

    }
}
