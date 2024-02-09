using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Passenger;
using Terminal.Entity.Trip.Enum;
using Terminal.Entity.Trip.Repository;
using Terminal.Entity.Trip.Service;
using Terminal.Entity.Trip.ValueObject;
using Route = Terminal.Entity.Trip.ValueObject.Route;

namespace Terminal.Entity.Trip
{
    internal abstract class Trip
    {
        private readonly ITripRepositpry _repository = new TripRepositpry();
        private readonly ITripService _service= new TripService();
        public Trip()
        {

        }
        public Trip(Route route, DateTime time, decimal pricePerSeet)
        {
            Route = route;
            Time = time;
            PricePerSeet = pricePerSeet;
        }

        public int Id { get; private set; }
       
        public Route Route { get; private set; }
        public DateTime Time { get; private set; }
        public decimal PricePerSeet { get; private set; }
        public int BusId { get; set; }
        public Bus.Bus Bus { get; set; }
        public HashSet<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();
        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
        }
        public abstract void BookTicket(List<int> seetNumbers,int passengerId);
        public abstract void BuyTicket(List<int> seetNumbers, int passengerId);
        public virtual async void ChangeBookTicketToBuyTicket(Ticket ticket)
        {
            if(ticket != null && ticket.Seats.First().SeatType == SeatType.Booked && DateTime.Now < Time)
            {
                foreach(var seat in ticket.Seats) 
                { 
                    seat.ChangeSeatType(SeatType.Bought);
                    _repository.SaveSeatUpdat(seat);
                }
                await _repository.SaveChanges();
                ticket.ChangePaymentTicket( ticket.TotalPrice);
                _repository.SaveTicketUpdat(ticket);
                await _repository.SaveChanges();
            }

        }
        public virtual async void CancelingTicket(Ticket ticket)
        {
            if (ticket != null && DateTime.Now < Time)
            {
                if (ticket.Seats.First().SeatType == SeatType.Booked)
                {
                    ticket.ChangePaymentTicket(ticket.Payment * 2 / 10);
                }
                else
                {
                    ticket.ChangePaymentTicket(ticket.TotalPrice / 10);
                }
                 _repository.SaveTicketUpdat(ticket);
                Thread.Sleep(1000);
                _service.DeleteSeats(ticket);

         
                _repository.SaveChange();
            }
        }
        public abstract void ShowSeet();
        public abstract int CalEmptySeatNembers();
    }
}
