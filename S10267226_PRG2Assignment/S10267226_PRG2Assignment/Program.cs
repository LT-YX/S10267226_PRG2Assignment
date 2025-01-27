//==========================================================
// Student Number : S10267226
// Student Name : Lovette Tew Yu Xin
// Partner Name : Tan Sheng Zhe Zander
//===========================================================


using S10267226_PRG2Assignment;

//
Dictionary<string, Flight> flightDictionary = new Dictionary<string, Flight>();
Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDictionary = new Dictionary<string, BoardingGate>();

// Main Program
ReadAirlines();
ReadBoardingGates();
LoadFlights();
DisplayFlightSchedule();

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

// Feature 1

void ReadAirlines()
{
    try
    {
        using (StreamReader sr = new StreamReader("airlines.csv"))
        {
            string s = sr.ReadLine(); // Reads header and ignores it
            while ((s = sr.ReadLine()) != null)
            {
                string[] information = s.Split(",");
                Airline a = new Airline(information[0], information[1]);
                airlineDictionary.Add(information[1], a);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading airlines file: {ex.Message}");
    }
}

void ReadBoardingGates()
{
    try
    {
        using (StreamReader sr = new StreamReader("boardinggates.csv"))
        {
            string s = sr.ReadLine(); // Reads header and ignores it
            while ((s = sr.ReadLine()) != null)
            {
                string[] information = s.Split(",");
                BoardingGate bg = new BoardingGate(information[0], Convert.ToBoolean(information[1]), Convert.ToBoolean(information[2]), Convert.ToBoolean(information[3]));
                boardingGateDictionary.Add(information[0], bg);
            }
        }
    }

    catch (Exception ex)
    {
        Console.WriteLine($"Error reading boarding gates file: {ex.Message}");
    }
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

// Feature 4
void ListBoardingGates()
{
    Console.WriteLine("=============================================\n" +
                    "List of Boarding Gates for Changi Airport Terminal 5\n" +
                    "=============================================");

    Console.WriteLine($"{"Gate Name",-15} {"Supports CFFT",-21} {"Supports DDJB",-21} {"Supports LWTT"}");
    foreach (BoardingGate bg in boardingGateDictionary.Values)
    {
        Console.WriteLine($"{bg.GateName,-15} {bg.SupportsCFFT,-21} {bg.SupportsDDJB,-21} {bg.SupportsLWTT}");
    }
}

// Feature 5

// Feature 6

// Feature 7

void DisplayFlightSchedule()
{
    try
    {
        //Listing of Airlines
        /*Console.WriteLine("=============================================\n" +
                        "List of Flights for Changi Airport Terminal 5\n" +
                        "=============================================");
        Console.WriteLine($"{"Name",-15} {"Code",-2}");
        */

        foreach (Airline f in airlineDictionary.Values)
        {
            Console.WriteLine($"{f.Name,-20} {f.Code,-2}");
        }

        Console.Write("Pls enter the airline Code: ");
        string airlineCode = Console.ReadLine();

        Console.WriteLine();

        if (airlineDictionary.ContainsKey(airlineCode))
        {
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-21} {"Origin",-21} {"Destination",-19}");
            foreach(Flight f in airlineDictionary[airlineCode].Flights.Values)
            {

                Console.WriteLine($"{f.FlightNumber,-15} {f.Origin,-21} {f.Destination,-19}");
            }
        }

        else
        {
            Console.WriteLine("Invalid Airline Code");
        }
    }

    catch (Exception ex)
    {
        Console.WriteLine("Pls Enter Valid Input");
    }
}