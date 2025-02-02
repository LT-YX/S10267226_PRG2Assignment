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
    public class NORMFlight : Flight
    {
        //Constructor
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            // Status is inherited. Default set to Scheduled
        }

        //Methods
        public override double CalculateFees() // Promotions can be applied in program.cs with a method called applyPromotions()
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
            return $"Flight Number: {FlightNumber,-10} Origin of Flight: {Origin,-10} Destination: {Destination,-10} Expected Time: {ExpectedTime,-10} Status: {Status,-10} Fees: {CalculateFees()}";
        }
    }
}
