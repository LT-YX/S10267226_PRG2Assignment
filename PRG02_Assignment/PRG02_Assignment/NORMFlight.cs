//==========================================================
// Student Number of Student 1 : S10267226
// Student 1 Name : Lovette Tew Yu Xin
// Student Number of Student 2 : S10265760
// Student 2 Name : Tan Sheng Zhe Zander
//==========================================================
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG02_Assignment
{
    public class NORMFlight : Flight
    {
        //Constructor
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status):base(flightNumber,origin,destination,expectedTime,status) 
        { 
        
        }
        
        //Methods
        public override double CalculateFees() // Promotions can be applied in program.cs with a method called applyPromotions()
        {
            double fees = 300.0; // Base fee for boarding gate
            if ("(SIN)" in Origin)
            {
                fees += 500.0;
            }
            if ("(SIN)" in Destination)
            {
                fees += 800.0;
            }
            return fees;    
        }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-10} Origin of Flight: {Origin,-10} Destination: {Destination,-10} Expected Time: {ExpectedTime,-10} Status: {Status,-10} Fees: {CalculateFees()}"
        }
    }
}
