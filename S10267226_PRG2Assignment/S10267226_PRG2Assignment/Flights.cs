using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267226_PRG2Assignment
{
    public abstract class Flight
    {
        //Parameters
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        //Constructor
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = status;

        }

        //Methods
        public abstract double CalculateFees();

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-10} Origin of Flight: {Origin,-10} Destination: {Destination,-10} Expected Time: {ExpectedTime,-10} Status: {Status}";
        }

    }
}
