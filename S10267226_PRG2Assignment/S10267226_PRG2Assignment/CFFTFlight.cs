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
    public class CFFTFlight : Flight
    {
        //Parameters
        public double RequestFee { get; set; }

        //Constructor
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            // Status is inherited. Default set to Scheduled
            RequestFee = 150.00; // CFFT fee is $150
        }

        public override double CalculateFees() // Promotions can be applied in program.cs with a method called applyPromotions()
        {
            double fees = 0; 
            fees += RequestFee; // Fee is $150
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
            return $"Flight Number: {FlightNumber,-10} Origin of Flight: {Origin,-10} Destination: {Destination,-10} Expected Time: {ExpectedTime,-10} Status: {Status,-10} Fees: {CalculateFees()}";
        }
    }
}
