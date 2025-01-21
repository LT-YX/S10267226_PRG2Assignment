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
    }
}
