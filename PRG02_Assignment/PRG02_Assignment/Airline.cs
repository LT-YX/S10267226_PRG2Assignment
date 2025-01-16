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


