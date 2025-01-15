//==========================================================
// Student Number of Student 1 : S10267226
// Student 1 Name : Lovette Tew Yu Xin
// Student Number of Student 2 : S10265760
// Student 2 Name : Tan Sheng Zhe Zander
//==========================================================

namespace PRG02_Assignment
{
    public class BoardingGate
    {
        public string GateName { get; set; };
        public bool SupportsCFFT { get; set; };
        public bool SupportsDDJB { get; set; };
        public bool SupportsLWTT { get; set; };
        public Flight Flight { get; set; };

        //Default Constructor
        public BoardingGate() { }

        //Parameterized Constructor
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT = supportsLWTT;
            Flight = flight;
        }
    }
}
