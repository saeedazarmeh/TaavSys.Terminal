using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Entity.Trip;

namespace Terminal.Entity.Passenger
{
    internal class Passenger
    {
        public Passenger(string phoneNumber, string name)
        {
            PhoneNumber = phoneNumber;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public HashSet<Ticket> Tickets { get; private set; } = new HashSet<Ticket>();

        public void Edit(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);    
        }
    }
}
