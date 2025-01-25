using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267226_PRG2Assignment
{
    public class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFee { get; set; } = new Dictionary<string, double>();

        public Terminal() { }
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }

        public bool AddAirline(Airline airline)
        {
            if (Airlines.ContainsKey(airline.Code))
            {
                return false;
            }
            Airlines.Add(airline.Code, airline);
            return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (BoardingGates.ContainsKey(boardingGate.GateName))
            {
                return false;
            }
            BoardingGates.Add(boardingGate.GateName, boardingGate);
            return true;
        }

        public Airline? GetAirlineFromFlight(Flight Flight)
        {
            foreach (var airline in Airlines.Values)
            {
                if (airline.Flights.ContainsValue(Flight))
                {
                    return airline;
                }
            }

            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (var fee in GateFee)
            {
                Console.WriteLine($"{fee.Key}: {fee.Value}");
            }
        }

    }
}