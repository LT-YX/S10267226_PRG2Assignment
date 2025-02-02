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
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight? flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
        }

        //Methods
        public double CalculateFees()
        {
            double basefee = 300; // base fee for boarding gates
            double flightFee = Flight.CalculateFees();
            double totalFee = basefee + flightFee;
      
            return totalFee;
        }

        public override string ToString()
        {
            return $"Gate: {GateName,-10} Supports CFFT: {SupportsCFFT,-10} Supports DDJB: {SupportsDDJB,-10} Supports LWTT: {SupportsLWTT,-10} Flight: {Flight}";
        }
    }
}
