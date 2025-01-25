using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267226_PRG2Assignment
{
    public class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        //Default Constructor
        public Airline() { }

        //Parameterized Constructor
        public Airline(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            Flights.Add(flight.FlightNumber, flight);
            return true;
        }

        public double CalculateTotalFees()
        {
            double totalFees = 0;
            foreach (Flight flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
            }
            return totalFees;
        }

        public bool RemoveFlight(string flightNumber)
        {
            if (Flights.ContainsKey(flightNumber))
            {
                Flights.Remove(flightNumber);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Airline: {Name,-10} Airline Code: {Code,-10} Total Fees: {CalculateTotalFees()}";
        }
    }
}
