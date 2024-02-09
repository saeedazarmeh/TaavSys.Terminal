using Terminal.Entity.Trip.Repository;
using Terminal.Entity.Trip.Service;
using Terminal.Entity.Trip.ValueObject;
using Route = Terminal.Entity.Trip.ValueObject.Route;

namespace Terminal.Entity.Trip
{
    internal class NormalTrip : Trip
    {
        private readonly ITripService _service = new TripService();
        private readonly ITripRepositpry _repository = new TripRepositpry();
        public NormalTrip()
        {

        }
        public NormalTrip(Route route, DateTime time, decimal pricePerSeet) : base(route, time, pricePerSeet)
        {
        }

        public override async void BookTicket(List<int> seatNumbers, int passengerId)
        {
            if (seatNumbers.Max() <=44 && seatNumbers.Min() > 0 && _service.CheckSeatsBeEmpty(seatNumbers, Tickets) && DateTime.Now < Time)
            {
                var ticket = new Ticket(passengerId, seatNumbers.Count * PricePerSeet, seatNumbers.Count * PricePerSeet * 3 / 10);
                AddTicket(ticket);
                await _repository.SaveChanges();
                var seats = ticket.ConverSeatNumbersToSeatList(seatNumbers, Enum.SeatType.Booked);
                ticket.AddSeats(seats);
                await _repository.SaveChanges();
            }
        }

        public override async void BuyTicket(List<int> seatNumbers, int passengerId)
        {
            if (seatNumbers.Max() <=44 && seatNumbers.Min() > 0 && _service.CheckSeatsBeEmpty(seatNumbers, Tickets) && DateTime.Now < Time)
            {
                var ticket = new Ticket(passengerId, seatNumbers.Count * PricePerSeet, seatNumbers.Count * PricePerSeet);
                AddTicket(ticket);
                await _repository.SaveChanges();
                var seats = ticket.ConverSeatNumbersToSeatList(seatNumbers, Enum.SeatType.Bought);
                ticket.AddSeats(seats);
                await _repository.SaveChanges();
            }
        }

        public override int CalEmptySeatNembers()
        {
            var NotEmptySeats = 0;
            foreach (var ticket in Tickets)
            {
                NotEmptySeats += ticket.Seats.Count;
            }
            return 44 - NotEmptySeats;
        }

        public override void ShowSeet()
        {
            var seats = _service.ConvertTicketsSeatsListToSeatsList(Tickets);
            string seatPattern = "";
            string seat = "";
            for (int i = 1; i <= 44; i++)
            {
                if (i < 21 || i > 24)
                {
                    if (i % 4 == 0)
                    {
                        seat = _service.ShowSeatDet(i, seats) + "\n";
                    }
                    else if (i % 2 == 0)
                    {
                        seat = _service.ShowSeatDet(i, seats) + "    ";
                    }
                    else
                    {
                        seat = _service.ShowSeatDet(i, seats) + " ";
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        seat = _service.ShowSeatDet(i, seats) + "\n";
                    }
                    else
                    {
                        seat = _service.ShowSeatDet(i, seats) + " ";
                    }
                }
                seatPattern = seatPattern + seat;
            }
            Console.WriteLine(seatPattern);
            
        }
    }
}
