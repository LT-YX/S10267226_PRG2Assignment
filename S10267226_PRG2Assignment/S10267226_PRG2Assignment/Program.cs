//==========================================================
// Student Number : S10267226
// Student Name : Lovette Tew Yu Xin
// Partner Name : Tan Sheng Zhe Zander
//===========================================================


using S10267226_PRG2Assignment;

//
Dictionary<string, Flight> flightDictionary = new Dictionary<string, Flight>();


// Main Program
LoadFlights();
ListFlights();


// Methods

// Displays menu
void DisplayMenu()
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

void LoadFlights()
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
                flightDictionary.Add(information[0], nf);
            }
            else
            {
                if (information[4] == "LWTT")
                {
                    LWTTFlight lf = new LWTTFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3]), information[4],500.00); // LWTTFlight
                    flightDictionary.Add(information[0], lf);

                }
                else if (information[4] == "CFFT")
                {
                    CFFTFlight cf = new CFFTFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3]), information[4], 150.00); // CFFTFlight
                    flightDictionary.Add(information[0], cf);
                }
                else
                {
                    DDJBFlight df = new DDJBFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3]), information[4], 300.00); // DJJBFlight
                    flightDictionary.Add(information[0], df);

                }
            }
        }
    }
}

// Feature 3
void ListFlights()
{
    Console.WriteLine("=============================================\n" +
        "List of Flights for Changi Airport Terminal 5\n" +
        "=============================================");
    Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-21} {"Origin",-21} {"Destination",-19} {"Expected Departure/Arrival Time"}");
    foreach (Flight f in flightDictionary.Values)
    {
        Console.WriteLine($"{f.FlightNumber,-15} {"airline",-21} {f.Origin,-21} {f.Destination,-19} {f.ExpectedTime}");
    }
}