using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal.Entity.Trip.ValueObject
{
    public class Route
    {
        public Route(string origin, string destination)
        {
            Origin = origin;
            Destination = destination;
        }

        public string Origin { get;  set; }
        public string Destination { get;  set; }
    }
}
