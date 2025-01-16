//==========================================================
// Student Number of Student 1 : S10267226
// Student 1 Name : Lovette Tew Yu Xin
// Student Number of Student 2 : S10265760
// Student 2 Name : Tan Sheng Zhe Zander
//==========================================================

using PRG02_Assignment;

// Main Program
loadFlights();

// Methods

// Displays menu
void displayMenu()
{
    Console.WriteLine("=============================================" +
        "\nWelcome to Changi Airport Terminal 5" +
        "\n=============================================" +
        "\n1. List All Flights" +
        "\n2. List Boarding Gates" +
        "\n3. Assign a Boarding Gate to a Flight" +
        "\n4. Create Flight" +
        "\n5. Display Airline Flights" +
        "\n6. Modify Flight Details" +
        "\n7. Display Flight Schedule" +
        "\n0. Exit\n");
}

// Feature 2

void loadFlights()
{
    using (StreamReader sr = new StreamReader("flights.csv"))
    {
        string s = sr.ReadLine(); // Reads header and ignores it
        while ((s = sr.ReadLine()) != null)
        {
            string[] information = s.Split(",");
            if (information[4] == "")
            {
                NORMFlight nf = new NORMFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3]), information[4]); // NormFlight
                Console.WriteLine("Normal flight created");
            }
            //else
            //{
                /*if (information[4] == "LWTT")
                {
                    
                }*/
           // }
        }
    }
}
