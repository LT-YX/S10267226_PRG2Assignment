﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10267226_PRG2Assignment
{
    public class DDJBFlight : Flight
    {
        //Parameters
        public double RequestFee { get; set; }

        //Constructor
        public DDJBFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }

        public override double CalculateFees() // Promotions can be applied in program.cs with a method called applyPromotions()
        {
            double fees = 300.0; // Base fee for boarding gate
            fees += RequestFee; // Fee is $300
            if (Origin == "SIN")
            {
                fees += 500.0;
            }
            if (Destination == "SIN")
            {
                fees += 800.0;
            }
            return fees;
        }

        public override string ToString()
        {
            return $"Flight Number: {FlightNumber,-10} Origin of Flight: {Origin,-10} Destination: {Destination,-10} Expected Time: {ExpectedTime,-10} Status: {Status,-10} Fees: {CalculateFees()}";
        }
    }
}
