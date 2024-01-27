using Terminal.Entity.Trip.Enum;

namespace Terminal.Entity.Trip
{
    internal class Seat
    {
        public Seat(SeatType seatType,int seatNumber)
        {
            SeatType = seatType;
            SeatNumber = seatNumber;
        }

        public int Id { get; set; }
        public int SeatNumber { get;private set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get;set; }
        public SeatType SeatType { get;private set; }
        public void ChangeSeatType(SeatType seatType)
        {
            SeatType = seatType;
        }
    }
}