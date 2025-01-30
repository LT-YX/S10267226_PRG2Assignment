//==========================================================
// Student Number : S10267226
// Student Name : Lovette Tew Yu Xin
// Partner Name : Tan Sheng Zhe Zander
//===========================================================


using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Runtime;
using System.Linq;
using S10267226_PRG2Assignment;
using System.Data.Common;

//
Dictionary<string, Flight> flightDictionary = new Dictionary<string, Flight>();
Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDictionary = new Dictionary<string, BoardingGate>();

Dictionary<string, string> specialCodeDictionary = new Dictionary<string, string>();  // Used for feature 5

// Main Program

Console.WriteLine("Loading Airlines...");
ReadAirlines();
Console.WriteLine("Loading Boarding Gates...");
ReadBoardingGates();
Console.WriteLine("Loading Flights...");
LoadFlights();
Console.WriteLine();

string option = "1";
// Loop
while (option != "0")
{
    DisplayMenu();
    Console.WriteLine("Please select your option: ");
    option = Console.ReadLine();

    switch(option)
    {
        case "1": // Feature 3 - Complete
            ListFlights();
            Console.WriteLine();
            break;

        case "2": // Feature 4 - Complete
            ListBoardingGates();
            Console.WriteLine();
            break;

        case "3": // Feature 5
            assignBoardingGate();
            break;

        case "4": // Feature 6 - Complete
            CreateNewFlight();
            Console.WriteLine();
            break;

        case "5": // Feature 7 - Complete
            DisplayFlightSchedule();
            Console.WriteLine();
            break;
        case "6": // Feature 8 - Complete
            ModifyFlightDetails();
            Console.WriteLine();
            break;

        case "7": // Feature 9
            
            break;

        case "0": // Exit

            break;
        default:
            Console.WriteLine("Invalid Option! Please try again.\n");
            break;
    }
}


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
    int counter = 0;
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
                counter += 1;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading airlines file: {ex.Message}");
    }
    Console.WriteLine($"{counter} Airlines Loaded!");
}

void ReadBoardingGates()
{
    int counter = 0;    
    try
    {
        using (StreamReader sr = new StreamReader("boardinggates.csv"))
        {
            string s = sr.ReadLine(); // Reads header and ignores it
            while ((s = sr.ReadLine()) != null)
            {
                string[] information = s.Split(",");
                BoardingGate bg = new BoardingGate(information[0], Convert.ToBoolean(information[1]), Convert.ToBoolean(information[2]), Convert.ToBoolean(information[3]),null);
                boardingGateDictionary.Add(information[0], bg);
                counter += 1;
            }
        }
    }

    catch (Exception ex)
    {
        Console.WriteLine($"Error reading boarding gates file: {ex.Message}");
    }
    Console.WriteLine($"{counter} Boarding Gates Loaded!");
}

// Feature 2

void LoadFlights()
{
    int counter = 0;
    try
    {
        using (StreamReader sr = new StreamReader("flights.csv"))
        {
            string s = sr.ReadLine(); // Reads header and ignores it
            while ((s = sr.ReadLine()) != null)
            {
                string[] information = s.Split(",");
                if (information[4] == "")
                {
                    NORMFlight nf = new NORMFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3])); // NormFlight
                    flightDictionary.Add(information[0], nf);
                    specialCodeDictionary.Add(information[0], information[4]);
                }
                else
                {
                    if (information[4] == "LWTT")
                    {
                        LWTTFlight lf = new LWTTFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3])); // LWTTFlight
                        flightDictionary.Add(information[0], lf);
                        specialCodeDictionary.Add(information[0], information[4]);

                    }
                    else if (information[4] == "CFFT")
                    {
                        CFFTFlight cf = new CFFTFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3])); // CFFTFlight
                        flightDictionary.Add(information[0], cf);
                        specialCodeDictionary.Add(information[0], information[4]);
                    }
                    else
                    {
                        DDJBFlight df = new DDJBFlight(information[0], information[1], information[2], Convert.ToDateTime(information[3])); // DJJBFlight
                        flightDictionary.Add(information[0], df);
                        specialCodeDictionary.Add(information[0], information[4]);

                    }
                }
                counter += 1;
            }
        }
    }
    catch (Exception ex)
    { 
        Console.WriteLine($"Error reading flights.csv file: {ex.Message}");
    }
    Console.WriteLine($"{counter} Flights Loaded!");
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
        Console.WriteLine($"{f.FlightNumber,-15} {airlineDictionary[f.FlightNumber[0..2]].Name,-21} {f.Origin,-21} {f.Destination,-19} {f.ExpectedTime}");
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
void assignBoardingGate()
{
    Console.WriteLine("=============================================\n" +
        "Assign a Boarding Gate to a Flight\n" +
        "=============================================");
    Console.Write("Enter Flight Number: ");
    string flightNumber = Console.ReadLine();
    Console.WriteLine($"Flight Number: {flightDictionary[flightNumber].FlightNumber}\n" +
        $"Origin: {flightDictionary[flightNumber].Origin}\n" +
        $"Destination: {flightDictionary[flightNumber].Destination}\n" +
        $"Expected Time: {flightDictionary[flightNumber].ExpectedTime}\n" +
        $"Special Request Code: {specialCodeDictionary[flightNumber]}");
   
}


// Feature 6
void createAirLineCodeList(List<string> airlineCodeList) // Ensures that when flights.csv is changed, the valid airline codes are 
{
    foreach(var airline in airlineDictionary.Values)
    {
        airlineCodeList.Add(airline.Code);
    }
}
string validateOriginDestination(string location, string text)
{
    string cityName;
    string airportCode;
    while (true)
    {
        try
        {
            Console.Write($"\nEnter {text}: ");
            location = Console.ReadLine().Trim();

            // Check if origin/destination is empty
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentException($"{text} cannot be empty");
            }

            // Check if origin/destination contains both city name and airport code
            string[] locationParts = location.Split(" ");
            if (locationParts.Length != 2)
            {
                throw new ArgumentException($"{text} entered is missing city name or airport code");
            }

            cityName = locationParts[0];

            // Check if origin/destination contains numbers
            if (!cityName.All(char.IsLetter))
            {
                throw new ArgumentException($"{text} City cannot contain Digits");
            }

            // Checks if origin/destination airport code is of correct length
            airportCode = locationParts[1];

            if (airportCode.Length != 5 || !airportCode[1..4].All(char.IsLetter)) // Check of length inclusive of ()
            {
                throw new ArgumentException("Airport code must contain only 3 letters");
            }

            // Formatting of origin
            location = char.ToUpper(cityName[0]) + cityName.Substring(1) + " " + airportCode.ToUpper(); 
            // Substring(1) means index 1 and onwards

            Console.WriteLine($"{text} entered: {location}");
            break;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"{text} must be in the format of \"City followed by a space and Airport code enclosed in parantheses\"\n- Example: Singapore (SIN)");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            Console.WriteLine($"{text} must be in the format of \"City followed by a space and Airport code enclosed in parantheses\"\n- Example: Singapore (SIN)");
        }
    }
    return location;
}
void CreateNewFlight()
{
    string repeat = "Y";

    // Flight Number
    string flightNumber = "";
    string code = "";
    string number = "";

    // Origin
    string origin = "";

    // Destination
    string destination = "";

    // Expected Time
    string expectedTime;
    DateTime validatedTime;

    // Special Request Code
    List<string> requestCodeList = ["CFFT", "DDJB", "LWTT", "None"];
    string specialRequestCode;

    // Validate Flight Number
    List<string> airlineCodeList = new();
    createAirLineCodeList(airlineCodeList);
    Console.Write("=============================================" +
        "\nCreating New Flight" +
        "\n=============================================");

    while (repeat == "Y")
    {

        while (true)
        {
            try
            {
                Console.Write("\nEnter Flight Number: ");
                flightNumber = Console.ReadLine().Trim(); // .Trim() removes whitespaces

                // Checks if flight number is empty
                if (string.IsNullOrWhiteSpace(flightNumber))
                {
                    throw new ArgumentException("Flight number cannot be empty");
                }

                string[] splittedFlightNumber = flightNumber.Split(' ');

                // Checks if flight number can be split into airline code and numbers
                if (splittedFlightNumber.Length != 2)
                {
                    throw new ArgumentException("Invalid format for flight number");
                }

                code = splittedFlightNumber[0];
                number = splittedFlightNumber[1];

                // Checks if airline code is in valid airline codes - Airline codes can be 2-3 letters but are usually 2 letters since some flight systems do not account for 3-digit airline codes [IATA Standard]
                // This code works for airline codes of any length as long as it existed inside airlines.csv
                if (!airlineCodeList.Contains(code.ToUpper()))
                {
                    if (code.All(char.IsLetter) == false) // Checks for reason why code not in airlineCodeList - Code contains numbers [This is kept here incase airline codes with numbers become a new standard]
                    {
                        throw new ArgumentException("Airline codes of flight numbers must contain only letters");
                    }
                    throw new ArgumentException("Invalid airline code for Changi Airport Terminal 5");
                }

                //Checks if number portion of flight number are numbers and of an acceptable length - Numbers can range from 1 to 4 [IATA standard]
                if (number.Length < 1 || number.Length > 4 || !number.All(char.IsDigit))
                {
                    throw new ArgumentException("Back portion of Flight number must contain only 1 to 4 digits");
                }

                flightNumber = flightNumber.ToUpper();

                // Checks if a flight with the exact same flight number exists
                // An airline may reuse flight numbers only if the flights operates on different days
                foreach (Flight f in flightDictionary.Values)
                {
                    if (f.FlightNumber == flightNumber)
                    {
                        throw new ArgumentException($"Flight with flight number {flightNumber} already exists");
                    }
                }

                Console.WriteLine($"Flight Number: {flightNumber}");
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Flight Number should be in the format of \"2-Letter code followed by a space and 1 to 4 digits\" - Example: BA 123\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
                Console.WriteLine("Flight Number should be in the format of \"2-Letter code followed by a space and 1 to 4 digits\" - Example: BA 123\n");
            }

        }
        while (true)
        {
            try
            {
                origin = validateOriginDestination(origin, "Origin");
                destination = validateOriginDestination(destination, "Destination");
                if (destination == origin)
                {
                    throw new ArgumentException();
                }
                break;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Origin and Destination cannot be the same");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }

        // Expectd Departure / Arrival Time
        while (true)
        {
            try
            {
                Console.Write("\nEnter expected Departure/Arrival time (dd/mm/yyyy hh:mm): ");
                expectedTime = Console.ReadLine();

                // Checks if time is empty
                if (string.IsNullOrWhiteSpace(expectedTime))
                {
                    throw new ArgumentException("Expected Departure/Arrival Time cannot be empty");
                }

                string[] splittedTime = expectedTime.Split(' ');

                // Checks if time can be split into date and time to check for date and time portion
                if (splittedTime.Length != 2)
                {
                    throw new ArgumentException("Expecture Departure/Arrival Time is missing Date or Time portions");
                }

                // validate date and time portion
                string date = splittedTime[0];
                string time = splittedTime[1];

                if (date.Any(char.IsLetter) || time.Any(char.IsLetter)) // Checks if letters are present in date & time
                {
                    throw new ArgumentException("Expecture Departure/Arrival Time cannot contain letters");
                }

                validatedTime = Convert.ToDateTime(expectedTime);
                Console.WriteLine($"Expected Departure/Arrival Time: {validatedTime}");
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Format for Departure/Arrival time is (dd/mm/yyyy hh:mm) - Example: 13/01/2025 15:40");
            }
            catch (FormatException)
            {
                Console.WriteLine("Expected Time entered is in the wrong format.");
                Console.WriteLine("Format for Departure/Arrival time is (dd/mm/yyyy hh:mm) - Example: 13/01/2025 15:40");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
                Console.WriteLine("Format for Departure/Arrival time is (dd/mm/yyyy hh:mm) - Example: 13/01/2025 15:40");
            }
        }
        // Validate special request code
        while (true)
        {
            try
            {
                Console.Write("\nEnter Special Request Code (CFFT/DDJB/LWTT/None): ");
                specialRequestCode = Console.ReadLine().Trim();

                // Checks if request code is empty
                if (string.IsNullOrWhiteSpace(specialRequestCode))
                {
                    throw new ArgumentException("Special Request Code cannot be empty");
                }

                if (!requestCodeList.Contains(specialRequestCode))
                {
                    throw new ArgumentException("Invalid Special Request Code entered.");
                }
                specialRequestCode = specialRequestCode.ToUpper();
                Console.WriteLine($"Special Request Code entered: {specialRequestCode}");
                break;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Valid Special Request Codes: CFFT, DDJB, LWTT.\nEnter None if flight has no special request.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
                Console.WriteLine("Valid Special Request Codes: CFFT, DDJB, LWTT.\nEnter None if flight has no special request.");
            }

        }
        Flight newflight;
        if (specialRequestCode == "NONE")
        {
            newflight = new NORMFlight(flightNumber, origin, destination, validatedTime);
            specialRequestCode = "";
        }
        else
        {
            if (specialRequestCode == "CFFT")
            {
                newflight = new CFFTFlight(flightNumber, origin, destination, validatedTime);
                
            }
            else if (specialRequestCode == "DDJB")
            {
                newflight = new DDJBFlight(flightNumber, origin, destination, validatedTime);
            }
            else
            {
                newflight = new LWTTFlight(flightNumber, origin, destination, validatedTime);
            }
            specialRequestCode += ",";
        }

        // Add Flight to Flight Dictionary
        flightDictionary.Add(flightNumber, newflight);

        // Adding Flight to flights.csv
        using (StreamWriter sw = new StreamWriter("flights.csv",true)) // true lets text be appeneded
        {
            sw.WriteLine($"{flightNumber},{origin},{destination},{expectedTime},{specialRequestCode}");
        }

        Console.WriteLine($"{flightNumber} has been added!\n");
        Console.Write("Would you like to add another flight? (Y/N): ");
        repeat = Console.ReadLine().ToUpper();
        while (repeat != "Y" && repeat != "N")
        {
            Console.WriteLine("Invalid Input");
            Console.Write("Would you like to add another flight? (Y/N): ");
            repeat = Console.ReadLine().ToUpper();
        }
    }
}

// Feature 7
void DisplayAirlines()
{
    Console.WriteLine("=============================================\n" +
        "List of Airlines for Changi Airport Terminal 5\n" +
        "=============================================");
    Console.WriteLine($"{"Airline Code",-15} {"Airline Name",-15}");
    foreach (Airline a in airlineDictionary.Values)
    {
        Console.WriteLine($"{a.Code,-15} {a.Name,-15}");
    }
}

void DisplayAirlineFlights(string code)
{
    try
    {
        string airlineCode = code;
        while (true)
        {
            if (airlineDictionary.ContainsKey(airlineCode))
            {
                Console.WriteLine("=============================================\n" +
                    $"List of Flights for {airlineDictionary[airlineCode].Name}\n" +
                    "=============================================");
                Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-20}{"Origin",-21}{"Destination",-19}{"Expected Departure/Arrival Time"}");
                foreach (Flight f in flightDictionary.Values)
                {
                    string[] airlineCode2 = f.FlightNumber.Split(' ');
                    if (airlineCode2[0] == airlineCode)
                    {
                        Console.WriteLine($"{f.FlightNumber,-15}{airlineDictionary[airlineCode2[0]].Name,-20}{f.Origin,-21}{f.Destination,-19} {f.ExpectedTime}");
                    }
                }

                break;
            }
            else
            {
                Console.WriteLine("Invalid Airline Code");
                Console.Write("Enter Airline Code: ");

                try { airlineCode = Console.ReadLine(); }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}



void DisplayFlightSchedule()
{
    try
    {
        //Listing of Airlines
        DisplayAirlines();


        //User Selection of Airline
        Console.Write("Enter Airline Code: ");
        string airlineCode = Console.ReadLine();

        DisplayAirlineFlights(airlineCode);

        //User Selection of Flight Number

        Console.WriteLine();
        Console.Write("Pls enter the flight number: ");
        string selectedFlightNumber = Console.ReadLine();

        foreach (Flight A in flightDictionary.Values) // Searching and Displaying the flight details
        {
            string[] airlineCode2 = A.FlightNumber.Split(' ');
            if (airlineCode2[0] == airlineCode)
            {
                if (airlineCode2[1] == selectedFlightNumber)
                {
                    // Flight Number, Airline Name, Origin, Destination, and Expected Departure/Arrival Time, Special Request Code

                    Console.WriteLine();

                    Console.WriteLine($"{"Flight Number",-15}" +
                        $"{"Airline Name",-20}" +
                        $"{"Origin",-21}" +
                        $"{"Destination",-19}" +
                        $"{"Expected Time",-25}" +
                        $"{"Status",-5}");

                    Console.WriteLine($"{A.FlightNumber,-15}" +
                        $"{airlineDictionary[airlineCode2[0]].Name,-20}" +
                        $"{A.Origin,-21}" +
                        $"{A.Destination,-19}" +
                        $"{A.ExpectedTime,-25}" +
                        $"{A.Status,-5}");
                    return;
                }
            }
        }

        throw new ArgumentException("Invalid Flight Number");

    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex}");
    }
}





// Feature 8
void ModifyFlightDetails()
{
    try
    {
        //Listing of Airlines
        DisplayAirlines();

        //Temp Dictionary for Airline Flights
        Dictionary<string, Flight> AirlineFlightDictionary = new Dictionary<string, Flight>();


        //User Selection of Airline

        Console.Write("Pls enter the airline Code: ");
        string airlineCode = Console.ReadLine();

        if (airlineDictionary.ContainsKey(airlineCode))
        {
            Console.WriteLine($"{"Flight Number",-15} {"Origin",-21} {"Destination",-19}");
            foreach (Flight f in flightDictionary.Values)
            {
                string[] airlineCode2 = f.FlightNumber.Split(' ');
                if (airlineCode2[0] == airlineCode)
                {
                    Console.WriteLine($"{f.FlightNumber,-15} {f.Origin,-21} {f.Destination,-19}");
                    AirlineFlightDictionary.Add(f.FlightNumber, f);
                }
            }
        }

        else
        {
            throw new ArgumentException("Invalid Airline Code");
        }





        //User Selection Flight Number

        Console.WriteLine("Choose an existing Flight to modify or delete:");
        string ChosenFlight = Console.ReadLine();

        while (true)
            if (AirlineFlightDictionary.ContainsKey(ChosenFlight))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid Flight Number");
                Console.WriteLine("Choose an existing Flight to modify or delete:");
                ChosenFlight = Console.ReadLine();
            }

        string[]  ChosenFlightSplit = ChosenFlight.Split(' ');
        Console.WriteLine("");

        //User Selection Action
        Console.WriteLine("[1] choose an existing Flight to modify");
        Console.WriteLine("[2] choose an existing Flight to delete");
        Console.WriteLine("Pls enter an option");
        string User_Action = Console.ReadLine();


        // Action 1
        if (User_Action == "1")
        {          
            foreach (Flight A in AirlineFlightDictionary.Values)
            {
                string[] airlineCode2 = A.FlightNumber.Split(' ');
                if (airlineCode2[1] == ChosenFlightSplit[1])
                {
                    //Basic Information except the Flight Number itself
                    //(i.e. Origin, Destination, and Expected Departure/Arrival Time),
                    //or Status, Special Request Code and update the Flight object’s information accordingly

                    Console.WriteLine();

                    Console.WriteLine($"{"Flight Number",-15}" +
                        $"{"Airline Name",-25}" +
                        $"{"Origin",-20}" +
                        $"{"Destination",-20}" +
                        $"{"Expected Time",-25}" +
                        $"{"Status",-5}");

                    Console.WriteLine($"{A.FlightNumber,-15}" +
                        $"{airlineDictionary[airlineCode2[0]].Name,-25}" +
                        $"{A.Origin,-20}" +
                        $"{A.Destination,-20}" +
                        $"{A.ExpectedTime,-25}" +
                        $"{A.Status,-20}"
                        );

                    while (true)
                    {
                        Console.WriteLine("What would you like to modify?");
                        Console.WriteLine("[1] Modify Basic Information");
                        Console.WriteLine("[2] Modify Status");
                        Console.WriteLine("[3] Modify Special Request Code");
                        Console.WriteLine("[4] Modify Boarding Gate");
                        Console.WriteLine("[5] Exit");
                        Console.WriteLine("Pls enter an option");
                        string User_Action2 = Console.ReadLine();


                        // Modify Basic Information
                        if (User_Action2 == "1")
                        {
                            // Reused Function from method 6
                            A.Origin = validateOriginDestination(A.Origin, "Origin");

                            // Reused Function from method 6
                            A.Destination = validateOriginDestination(A.Destination, "Destination");

                            // Modify Time
                            while (true)
                            {
                                try
                                {
                                    Console.Write("\nEnter expected Departure/Arrival time (dd/mm/yyyy hh:mm): ");
                                    string expectedTime = Console.ReadLine();

                                    // Checks if time is empty
                                    if (string.IsNullOrWhiteSpace(expectedTime))
                                    {
                                        throw new ArgumentException("Expected Departure/Arrival Time cannot be empty");
                                    }

                                    string[] splittedTime = expectedTime.Split(' ');

                                    // Checks if time can be split into date and time to check for date and time portion
                                    if (splittedTime.Length != 2)
                                    {
                                        throw new ArgumentException("Expecture Departure/Arrival Time is missing Date or Time portions");
                                    }

                                    // validate date and time portion
                                    string date = splittedTime[0];
                                    string time = splittedTime[1];

                                    if (date.Any(char.IsLetter) || time.Any(char.IsLetter)) // Checks if letters are present in date & time
                                    {
                                        throw new ArgumentException("Expecture Departure/Arrival Time cannot contain letters");
                                    }

                                    A.ExpectedTime = Convert.ToDateTime(expectedTime);
                                    Console.WriteLine($"Expected Departure/Arrival Time: {A.ExpectedTime}");
                                    break;
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.WriteLine("Format for Departure/Arrival time is (dd/mm/yyyy hh:mm) - Example: 13/01/2025 15:40");
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Expected Time entered is in the wrong format.");
                                    Console.WriteLine("Format for Departure/Arrival time is (dd/mm/yyyy hh:mm) - Example: 13/01/2025 15:40");
                                }
                            }
                        }

                        // Modify Status
                        else if (User_Action2 == "2")
                        {

                        }

                        // Modify Special Request Code
                        else if (User_Action2 == "3")
                        {
                            while (true)
                            {
                                Console.WriteLine("Possible Statuses CFFT, DDJB, LWTTF, NORM (Will Return Null)");
                                Console.Write("Enter Status: ");

                                try
                                {
                                    String New_Status = Console.ReadLine();

                                    if (New_Status == "CFFT" || New_Status == "DDJB" || New_Status == "LWTTF")
                                    {
                                        A.Status = Console.ReadLine();
                                        break;
                                    }

                                    else if (New_Status == "NORM")
                                    {
                                        A.Status = null;
                                        break;
                                    }

                                    else
                                    {
                                        throw new ArgumentException("Invalid Status");
                                    }
                                }

                                catch (Exception ex)
                                {
                                    Console.WriteLine($"{ex}");
                                }
                            }
                        }

                        // Modify Boarding Gate
                        else if (User_Action2 == "4")
                        {

                        }

                        else if (User_Action2 == "5")
                        {
                            break;
                        }

                        else
                        {
                            throw new ArgumentException("Invalid Action");
                        }
                    }
                }
            }
        }


        // Action 2
        else if (User_Action == "2")
        {


            foreach (Flight A in AirlineFlightDictionary.Values)
            {
                string[] airlineCode2 = A.FlightNumber.Split(' ');

                if (airlineCode2[1] == ChosenFlightSplit[1])
                {
                    //Basic Information except the Flight Number itself
                    //(i.e. Origin, Destination, and Expected Departure/Arrival Time),
                    //or Status, Special Request Code and update the Flight object’s information accordingly

                    Console.WriteLine();

                    Console.WriteLine($"{"Flight Number",-15}" +
                        $"{"Airline Name",-25}" +
                        $"{"Origin",-20}" +
                        $"{"Destination",-20}" +
                        $"{"Expected Time",-25}" +
                        $"{"Status",-5}");

                    Console.WriteLine($"{A.FlightNumber,-15}" +
                        $"{airlineDictionary[airlineCode2[0]].Name,-25}" +
                        $"{A.Origin,-20}" +
                        $"{A.Destination,-20}" +
                        $"{A.ExpectedTime,-25}" +
                        $"{A.Status,-20}");
                }
            }

            while (true)
            {
                Console.WriteLine("Are you sure you want to delete this flight? (Y/N)");
                string User_Action3 = Console.ReadLine();


                if (User_Action3 == "Y")
                {
                    foreach (Flight A in AirlineFlightDictionary.Values)
                    {
                        string[] airlineCode2 = A.FlightNumber.Split(' ');
                        if (airlineCode2[1] == ChosenFlightSplit[1])
                        {
                            flightDictionary.Remove(A.FlightNumber);
                            Console.WriteLine("Flight has been deleted");
                            break;
                        }
                    }
                    break;
                }

                else if (User_Action3 == "N")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid Action");
                }
            }
        }

        // Error
        else
        {
            throw new ArgumentException("Invalid Action");
        }

    

    }

    catch (Exception ex)
    {
        Console.WriteLine($"{ex}");
    }
}

// Feature 9