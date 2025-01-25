using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267226_PRG2Assignment
{
    public class BoardingGate
    {

        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        //Default Constructor
        public BoardingGate() { }

        //Parameterized Constructor
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }

        //Methods
        public double CalculateFees()
        {
            double basefee = 300;
            return basefee;
        }

        public override string ToString()
        {
            return $"Gate: {GateName,-10} Supports CFFT: {SupportsCFFT,-10} Supports DDJB: {SupportsDDJB,-10} Supports LWTT: {SupportsLWTT,-10} Flight: {Flight}";
        }
    }
}
