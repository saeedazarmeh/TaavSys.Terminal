using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using Terminal.Common;
using Terminal.Entity.Bus;
using Terminal.Entity.Bus.Repository;
using Terminal.Entity.Log;
using Terminal.Entity.Log.Repository;
using Terminal.Entity.Passenger;
using Terminal.Entity.Passenger.Repository;
using Terminal.Entity.Trip;
using Terminal.Entity.Trip.Repository;
using Terminal.Entity.Trip.ValueObject;

IBusRepository _bus = new BusRepository();
ITripRepositpry _trip = new TripRepositpry();
IPassengerRepository _passenger = new PassengerRepository();
ILogRepository _Log = new LogRepository();

while (true)
{

    Console.WriteLine("Enter your Operation Index:\n" +
        "1-Add Bus\n" +
        "2-Add Trip\n" +
        "3-Show Trip\n" +
        "4-Book A Ticket\n" +
        "5-Buy A Ticke\n" +
        "6-Change Buy Booked Ticket\n" +
        "7-Cancel A Ticket\n" +
        "8-Trip Logging\n" +
        "9-Exit\n ");
    var choice = int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            {
                Console.WriteLine("Enter Name of Bus");
                var name = Console.ReadLine();
                Console.WriteLine("Enter Name of BusDriverNamer");
                var driverName = Console.ReadLine();
                Console.WriteLine("Enter Type of Bus:1-Normal 2-VIP");
                Enum.TryParse(Console.ReadLine(), out Terminal.Entity.Bus.Enum.Type type);
                var bus = new Bus(type, name, driverName);
                _bus.AddBus(bus);
                _bus.SaveChanges();

            }
            break;
        case 2:
            {
                Console.WriteLine("Enter Trip type:1-Normal 2-VIP");
                Enum.TryParse(Console.ReadLine(), out Terminal.Entity.Bus.Enum.Type type);
                Console.WriteLine("Choose a bus By It's Id");
                var buses = await _bus.GetBuses(type);
                Utility.ShowBuses(buses);
                var choosedBusId = int.Parse(Console.ReadLine());
                var choosedBus = await _bus.GetBus(choosedBusId);
                Console.WriteLine("Enter Origin of Trip");
                var origin = Console.ReadLine();
                Console.WriteLine("Enter Destination of Trip");
                var destination = Console.ReadLine();
                Console.WriteLine("Enter PricePerSeat of Trip");
                var pricePerSeat = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter Date of Trip");
                var dateTime = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter Time of Trip");
                var time = TimeSpan.Parse(Console.ReadLine());
                dateTime = dateTime.Add(time);
                var route = new Route(origin, destination);
                Trip trip;
                if (type == Terminal.Entity.Bus.Enum.Type.VIP)
                {
                    trip = new VIPTrip(route, dateTime, pricePerSeat);
                }
                else
                {
                    trip = new NormalTrip(route, dateTime, pricePerSeat);
                }
                choosedBus.AddTrip(trip);
                _bus.SaveChanges();
            }
            break;
        case 3:
            {
                var trips = await _trip.GetTrips();
                Utility.ShowTrips(trips);
                Console.WriteLine("Choose a trip By It's Id");
                var tripId = int.Parse(Console.ReadLine());
                var choosedTrip = await _trip.GetTripByTickets(tripId);
                choosedTrip.ShowSeet();
            }
            break;
        case 4:
            {

                Console.WriteLine("Enter Passenger PhoneNumber");
                var phone = Console.ReadLine();
                var passenger = await _passenger.GetPassengerByPhone(phone);
                if (passenger == null)
                {
                    Console.WriteLine("Enter Name Of Passenger");
                    var name = Console.ReadLine();
                    var newPassenger = new Passenger(phone, name);
                    await _passenger.AddPassenger(newPassenger);
                    await _passenger.SaveChanges();
                    passenger = await _passenger.GetPassengerByPhone(phone);
                }
                var trips = await _trip.GetTrips();
                Utility.ShowTrips(trips);
                Console.WriteLine("Choose a trip By It's Id");
                var tripId = int.Parse(Console.ReadLine());
                var choosedTrip = await _trip.GetTripByTickets(tripId);
                choosedTrip.ShowSeet();
                var condition = true;
                List<int> seatNumbers = new List<int>();
                while (condition)
                {
                    Console.WriteLine("Enter seatNumber");
                    seatNumbers.Add(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Do you Want To Add More seatNumber:1-Yes 2-No");
                    condition = Console.ReadLine() != "1" ? false : true;
                }
                choosedTrip.BookTicket(seatNumbers, passenger.Id);
                var ticket = choosedTrip.Tickets.Last();
                _trip.SaveChanges();
                var log = new Log(tripId, ticket.Id, Terminal.Entity.Log.Enum.Status.Booked, ticket.Payment,0);
                _Log.Add(log);
                _Log.SaveChanges();
            }
            break;
        case 5:
            {
                Console.WriteLine("Enter Passenger PhoneNumber");
                var phone = Console.ReadLine();
                var passenger = await _passenger.GetPassengerByPhone(phone);
                if (passenger == null)
                {
                    Console.WriteLine("Enter Name Of Passenger");
                    var name = Console.ReadLine();
                    var newPassenger = new Passenger(phone, name);
                    await _passenger.AddPassenger(newPassenger);
                    await _passenger.SaveChanges();
                    passenger = await _passenger.GetPassengerByPhone(phone);
                }
                var trips = await _trip.GetTrips();
                Utility.ShowTrips(trips);
                Console.WriteLine("Choose a trip By It's Id");
                var tripId = int.Parse(Console.ReadLine());
                var choosedTrip = await _trip.GetTripByTickets(tripId);
                choosedTrip.ShowSeet();
                var condition = true;
                List<int> seatNumbers = new List<int>();
                while (condition)
                {
                    Console.WriteLine("Enter seatNumber");
                    seatNumbers.Add(int.Parse(Console.ReadLine()));
                    Console.WriteLine("Do you Want To Add More seatNumber:1-Yes 2-No");
                    condition = Console.ReadLine() != "1" ? false : true;
                }
                choosedTrip.BuyTicket(seatNumbers, passenger.Id);
                var ticket = choosedTrip.Tickets.Last();
                _trip.SaveChanges();
                var log = new Log(tripId, ticket.Id, Terminal.Entity.Log.Enum.Status.Bougth, ticket.Payment, 0);
                _Log.Add(log);
                _Log.SaveChanges();
            }
            break;
        case 6:
            {
                Console.WriteLine("Enter Passenger PhoneNumber");
                var phone = Console.ReadLine();
                var passenger = await _passenger.GetPassengerWithTicketsAndTrip(phone);
                Utility.ShowPassengerTickets(passenger);
                Console.WriteLine("Choose a ticket By It's Id");
                var ticketId = int.Parse(Console.ReadLine());
                var choosedTicket = passenger.Tickets.FirstOrDefault(t => t.Id == ticketId);
                var AddedMoney = choosedTicket.TotalPrice - choosedTicket.Payment;
                var choosedTrip = await _trip.GetTrip(choosedTicket.TripId);
                choosedTrip.ChangeBookTicketToBuyTicket(choosedTicket);
                await _trip.SaveChanges();
                var log = new Log(choosedTicket.TripId, ticketId, Terminal.Entity.Log.Enum.Status.ChangeToBought, AddedMoney, 0);
                _Log.Add(log);
                _Log.SaveChanges();
            }
            break;
        case 7:
            {
                Console.WriteLine("Enter Passenger PhoneNumber");
                var phone = Console.ReadLine();
                var passenger = await _passenger.GetPassengerWithTicketsAndTrip(phone);
                Utility.ShowPassengerTickets(passenger);
                Console.WriteLine("Choose a ticket By It's Id");
                var ticketId = int.Parse(Console.ReadLine());
                var choosedTicket = passenger.Tickets.FirstOrDefault(t => t.Id == ticketId);
                var OldPayment=choosedTicket.Payment;
                var choosedTrip = await _trip.GetTrip(choosedTicket.TripId);
                var ticketType=choosedTicket.Seats.Last().SeatType;
                choosedTrip.CancelingTicket(choosedTicket);
                _trip.SaveChanges();
                var takedBack=choosedTicket.Payment-OldPayment;
                Terminal.Entity.Log.Enum.Status status;
                if(ticketType == Terminal.Entity.Trip.Enum.SeatType.Bought) 
                { 
                    status = Terminal.Entity.Log.Enum.Status.BougthCancel;
                }
                else
                {
                    status = Terminal.Entity.Log.Enum.Status.BookedCancel;
                }
                var log = new Log(choosedTicket.TripId, ticketId, status, 0, takedBack);
                _Log.Add(log);
                _Log.SaveChanges();
            }
            break;
        case 8:
            {
                var trips = await _trip.GetTrips();
                Utility.ShowTrips(trips);
                Console.WriteLine("Choose a trip By It's Id");
                var tripId = int.Parse(Console.ReadLine());
                var choosedTrip=await _trip.GetTripByTickets(tripId);
                var logs=_Log.GetTripLog(tripId);
                Utility.ShowTrioResult(logs,choosedTrip);
            }
            break;
        case 9:
            {
                Environment.Exit(0);
            }
            break;

    }
}
