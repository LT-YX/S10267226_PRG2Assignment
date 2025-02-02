using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10267226
// Student Name : Lovette Tew Yu Xin
// Partner Name : Tan Sheng Zhe Zander
//===========================================================

namespace S10267226_PRG2Assignment
{
    public abstract class Flight : IComparable<Flight>
    {
        //Parameters
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        //Constructor
        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedTime = expectedTime;
            Status = "Scheduled"; // Default set to Scheduled

        }

        //Methods
        public virtual double CalculateFees()
        {
            double fees = 0;
            if (Origin.ToLower() == "singapore (sin)")
            {
                fees += 800.0;
            }
            if (Destination.ToLower() == "singapore (sin)")
            {
                fees += 500.0;
            }
            return fees;
        }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-10} Origin of Flight: {Origin,-10} Destination: {Destination,-10} Expected Time: {ExpectedTime,-10} Status: {Status}";
        }

        public int CompareTo(Flight anotherFlight)
        {
            return ExpectedTime.CompareTo(anotherFlight.ExpectedTime);
        }

    }
}
