using Terminal.Entity.Trip.Repository;
using Terminal.Entity.Trip.Service;
using Terminal.Entity.Trip.ValueObject;
using Route = Terminal.Entity.Trip.ValueObject.Route;

namespace Terminal.Entity.Trip
{
    internal class VIPTrip : Trip
    {
        private readonly ITripService _service = new TripService();
        private readonly ITripRepositpry _repository = new TripRepositpry();
        public VIPTrip()
        {

        }
        public VIPTrip(Route route, DateTime time, decimal pricePerSeet) : base(route, time, pricePerSeet)
        {
        }

        public override async void BookTicket(List<int> seatNumbers, int passengerId)
        {
            if (seatNumbers.Max() <= 30 && seatNumbers.Min() > 0 && _service.CheckSeatsBeEmpty(seatNumbers, Tickets) && DateTime.Now < Time)
            {
                var ticket = new Ticket(passengerId, seatNumbers.Count * PricePerSeet, seatNumbers.Count * PricePerSeet * 3 / 10);
                AddTicket(ticket);
                await _repository.SaveChanges();
                var seats = ticket.ConverSeatNumbersToSeatList(seatNumbers,Enum.SeatType.Booked);
                ticket.AddSeats(seats);
                await _repository.SaveChanges();
            }
        }

        public override async void BuyTicket(List<int> seatNumbers, int passengerId)
        {
            if (seatNumbers.Max() <= 30 && seatNumbers.Min() > 0 && _service.CheckSeatsBeEmpty(seatNumbers, Tickets) && DateTime.Now < Time)
            {
                var ticket = new Ticket(passengerId, seatNumbers.Count * PricePerSeet, seatNumbers.Count * PricePerSeet);
                AddTicket(ticket);
                await _repository.SaveChanges();
                var seats = ticket.ConverSeatNumbersToSeatList(seatNumbers, Enum.SeatType.Bought);
                ticket.AddSeats(seats);
                await _repository.SaveChanges();
            }
        }

        public override void ShowSeet()
        {
            var seats = _service.ConvertTicketsSeatsListToSeatsList(Tickets);
            string seatPattern = "";
            string seat = "";
            for (int i = 1; i <= 30; i++)
            {
                if (i < 16 || i > 18)
                {
                    if ((i - 1) % 3 == 0)
                    {
                        seat = _service.ShowSeatDet(i, seats)+ "     ";
                    }
                    else if (i % 3 == 0)
                    {
                        seat = _service.ShowSeatDet(i, seats) + "\n";
                    }
                    else
                    {
                        seat = _service.ShowSeatDet(i, seats) + " ";
                    }
                }
                else 
                {
                    seat = _service.ShowSeatDet(i, seats) + "\n";
                }
                seatPattern = seatPattern + seat;
            }
            Console.WriteLine(seatPattern);
        }
    }
}
