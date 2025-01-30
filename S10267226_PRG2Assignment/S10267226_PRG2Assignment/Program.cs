﻿//==========================================================
// Student Number : S10267226
// Student Name : Lovette Tew Yu Xin
// Partner Name : Tan Sheng Zhe Zander
//===========================================================
// Features:
// - Lovette: 2,3,5,6,9
// - Zander:  1,4,7,8 
//===========================================================

using System.Collections.Specialized;
using System.Diagnostics.Metrics;
using System.Runtime;
using System.Linq;
using S10267226_PRG2Assignment;
using System.Data.Common;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;

//
Dictionary<string, Flight> flightDictionary = new Dictionary<string, Flight>();
Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDictionary = new Dictionary<string, BoardingGate>();

Dictionary<string, string> specialCodeDictionary = new Dictionary<string, string>();  // Used for feature 5 AND feature 8

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
            Console.WriteLine("Goodbye!");
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

// Validation Methods

// -- Validation for Empty inputs
void ValidateEmpty(string? toBeValidated, string errorMessage)
{
    if (string.IsNullOrWhiteSpace(toBeValidated))
    {
        throw new ArgumentException($"{errorMessage} cannot be empty");
    }
}

// -- Validation for inputs that have at least two portions [Origin and Destination can have more portions]
void ValidateSplit(string[] split, string errorMessage)
{
    if (split.Length < 2)
    {
        throw new ArgumentException(errorMessage);
    }
}
// - Validation for origin and destinations inputs
string ValidateOriginDestination(string location, string text)
{
    string cityName;
    string airportCode;
    while (true)
    {
        try
        {
            Console.WriteLine($"\nEnter {text}: ");
            location = Console.ReadLine().Trim();

            // Check if origin/destination is empty
            ValidateEmpty(location, text);

            // Check if origin/destination contains both city name and airport code
            string[] locationParts = location.Split(" ");

            ValidateSplit(locationParts, $"{text} entered is missing city name or airport code");

            cityName = "";

            // Compensates for the case where city names have multiple parts to it
            for (int i = 0; i < locationParts.Length-1;i++)
            {
                cityName += locationParts[i] + " ";
                Console.WriteLine(cityName);
            }
            // Removes additional whitespace
            cityName = cityName.Trim();

            // Check if city name of origin/destination contains only letters or spaces
            if (!cityName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c))) // Returns true when city name is not entirely letters or spaces
            {
                throw new ArgumentException($"{text} City name can only contain letters or spaces");
            }

            // Checks if origin/destination airport code is of correct length
            airportCode = locationParts[locationParts.Length-1] ;

            if (airportCode.Length != 5 || !airportCode[1..4].All(char.IsLetter)) // Check of length inclusive of ()
            {
                throw new ArgumentException("Airport code must contain only 3 letters and enclosed in parantheses ().");
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
// -- Validation for time inputs
DateTime ValidateExpectedTime(string expectedTime)
{
    DateTime validatedTime;
    while (true)
    {
        try
        {
            Console.WriteLine("\nEnter expected Departure/Arrival time (dd/mm/yyyy hh:mm): ");
            expectedTime = Console.ReadLine();

            // Checks if time is empty
            ValidateEmpty(expectedTime, "Expected Departure/Arrival Time");

            string[] splittedTime = expectedTime.Split(' ');

            // Checks if time can be split into date and time to check for date and time portion
            ValidateSplit(splittedTime, "Expecture Departure/Arrival Time is missing Date or Time portions");

            // validate date and time portion
            string date = splittedTime[0];
            string time = splittedTime[1];

            if (date.Any(char.IsLetter) || time.Any(char.IsLetter)) // Checks if letters are present in date & time | Not checking for digits only since / is not a digit
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
    return validatedTime;
}

// -- Check if a value is contained in the list
void ValidatePresence(List<string> itemList, string item, string errorMessage)
{
    if (!itemList.Contains(item)) // Alternative is itemList.Contains(item) == False | Using ! shortens it to if (true)
    {
        throw new ArgumentException(errorMessage);
    }
}


// Feature 1
void ReadAirlines()
{
    int counter = 0; // Counter to display the number of airlines loaded
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
    int counter = 0; // Counter to display number of boarding gates loaded
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
    int counter = 0; // counter to display number of flights loaded
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
                    // stores the special request code and matches it to the flight since flight does not store special request code
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
    // variables with place holder values
    string selectedFlightNumber = "";
    string selectedOrigin = "";
    string selectedDestination = "";
    DateTime selectedExpectedTime = Convert.ToDateTime("30/01/2025 12:45"); // Place holder value
    string selectedSpecialCode = "";
    string option = "flight";
    string text = "Flight Number";

    string boardingGate = "";
    bool supportDDJB = false;
    bool supportCFFT = false;
    bool supportLWTT = false;

    Console.WriteLine("=============================================\n" +
        "Assign a Boarding Gate to a Flight\n" +
        "=============================================");

    // Validating Flight Number & Boarding Gate
    while (true)
    {
        try
        {
            if (option == "flight")
            {
                Console.WriteLine("\nEnter Flight Number: ");
                string flightNumber = Console.ReadLine().ToUpper();

                // Check if empty input
                ValidateEmpty(flightNumber, text);



                selectedFlightNumber = flightDictionary[flightNumber].FlightNumber;
                selectedOrigin = flightDictionary[flightNumber].Origin;
                selectedDestination = flightDictionary[flightNumber].Destination;
                selectedExpectedTime = flightDictionary[flightNumber].ExpectedTime;
                if (specialCodeDictionary.ContainsKey(flightNumber))
                {
                    selectedSpecialCode = specialCodeDictionary[flightNumber];
                }
                else
                {
                    selectedSpecialCode = "None";
                }
                option = "boarding gate";
                text = "Boarding Gate";
            }
            if (option == "boarding gate")
            {
                string modifier = "";
                while (true)
                {
                    Console.WriteLine($"\n{modifier}Enter Boarding Gate Name: ");
                    boardingGate = Console.ReadLine().ToUpper();

                    // Check if empty input
                    ValidateEmpty(boardingGate, text);

                    supportCFFT = boardingGateDictionary[boardingGate].SupportsCFFT;
                    supportDDJB = boardingGateDictionary[boardingGate].SupportsDDJB;
                    supportLWTT = boardingGateDictionary[boardingGate].SupportsLWTT;

                    Flight? f = boardingGateDictionary[boardingGate].Flight;
                    if (f != null)
                    {
                        Console.WriteLine($"{boardingGate} has been assigned to Flight {f.FlightNumber}");
                        modifier = "Re-";
                    }
                    else
                    {
                        break;
                    }
                }
            }
            break;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine($"Enter valid {text}");
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"{text} does not exist");
            Console.WriteLine($"Re-check the {text}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
            Console.WriteLine("Please try again");
        }

    }
    // Displaying Flight info & Boarding Gate
    Console.WriteLine($"\nFlight Information: \nFlight Number: {selectedFlightNumber}\nOrigin: {selectedOrigin}\n" +
        $"Destination: {selectedDestination}\nExpected Time: {selectedExpectedTime}\nSpecial Request Code: {selectedSpecialCode}");
    Console.WriteLine($"\nBoarding Gate Information: \nBoarding Gate Name: {boardingGate}\nSupports DDJB: {supportDDJB}" +
        $"\nSupports CCFT: {supportCFFT}\nSupports LWTT: {supportLWTT}\n");

    // Assigning Flight to boarding gate
    boardingGateDictionary[boardingGate].Flight = flightDictionary[selectedFlightNumber];

    // Updating Flight Status
    string input;
    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
    input = Console.ReadLine().ToUpper();
    while (input != "Y" && input != "N")
    {
        Console.WriteLine("Invalid choice\n");
        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        input = Console.ReadLine();
    }
    if (input == "Y")
    {
        Console.WriteLine("\n1. Delayed\n2. Boarding\n3. On Time");
        Console.WriteLine("Please select the new status of the flight: ");
        string statusChoice = Console.ReadLine();
        while (statusChoice != "1" && statusChoice != "2" && statusChoice != "3")
        {
            Console.WriteLine("Invalid choice\n");
            Console.WriteLine("1. Delayed\n2. Boarding\n3. On Time");
            Console.WriteLine("Please select the new status of the flight: ");
            statusChoice = Console.ReadLine();
        }
        if (statusChoice == "1")
        {
            flightDictionary[selectedFlightNumber].Status = "Delayed";
        }
        else if (statusChoice == "2")
        {
            flightDictionary[selectedFlightNumber].Status = "Boarding";
        }
        else
        {
            flightDictionary[selectedFlightNumber].Status = "On Time";
        }
    }
    // Output that flight has been assigned
    Console.WriteLine($"Flight {selectedFlightNumber} has been assigned to Boarding Gate {boardingGate}!");


}


// Feature 6

// Ensures that when airlines.csv is changed, the valid airline codes are updated | Incase airlines are removed or added
void CreateAirLineCodeList(List<string> airlineCodeList) 
{
    foreach(var airline in airlineDictionary.Values)
    {
        airlineCodeList.Add(airline.Code);
    }
}

void CreateNewFlight()

    // Variables used in the method. Some have a dummy value assigned to it so that Visual Studio doesn't produce a warning
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
    string expectedTime = "";
    DateTime validatedTime;

    // Special Request Code
    List<string> requestCodeList = ["CFFT", "DDJB", "LWTT", "NONE"];
    string specialRequestCode;

    // Validate Flight Number
    List<string> airlineCodeList = new();
    CreateAirLineCodeList(airlineCodeList);
    Console.Write("=============================================" +
        "\nCreating New Flight" +
        "\n=============================================");

    while (repeat == "Y")
    {

        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter Flight Number: ");
                flightNumber = Console.ReadLine().Trim(); // .Trim() removes whitespaces

                // Checks if flight number is empty
                ValidateEmpty(flightNumber, "Flight Number");

                string[] splittedFlightNumber = flightNumber.Split(' ');

                // Checks if flight number can be split into airline code and numbers
                ValidateSplit(splittedFlightNumber, "Invalid format for flight number");

                code = splittedFlightNumber[0];
                number = splittedFlightNumber[1];

                // Checks if airline code is in valid airline codes - Airline codes can be 2-3 letters but are usually 2 letters since some flight systems do not account for 3-digit airline codes [IATA Standard]
                // This code works for airline codes of any length as long as it existed inside airlines.csv
                ValidatePresence(airlineCodeList, code.ToUpper(), "Invalid airline code for Changi Airport Terminal 5");
                
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
                if (code.All(char.IsLetter) == false) // Checks for characters that are not letters. This allows the user to be informed that airline codes can only contain letters when they type in not letters
                {
                    Console.WriteLine("\"Airline codes of flight numbers must contain only letters\"");
                }
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
                origin = ValidateOriginDestination(origin, "Origin");
                destination = ValidateOriginDestination(destination, "Destination");
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

        validatedTime = ValidateExpectedTime(expectedTime);
        
        // Validate special request code
        while (true)
        {
            try
            {
                Console.WriteLine("\nEnter Special Request Code (CFFT/DDJB/LWTT/None): ");
                specialRequestCode = Console.ReadLine().Trim();

                // Checks if request code is empty
                ValidateEmpty(specialRequestCode, "Special Request Code");

                specialRequestCode = specialRequestCode.ToUpper();

                // Check if special request code exists
                ValidatePresence(requestCodeList, specialRequestCode, "Invalid Special Request Code entered.");
                
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

        // Add Flight's special request code to special request code dictionary - Ensures that feature 5 works
        // This is added here because the if-statements will modify special request code so that it'll be in the right format when writing to file.
        specialCodeDictionary.Add(flightNumber, specialRequestCode);

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
        Console.WriteLine("Would you like to add another flight? (Y/N): ");
        repeat = Console.ReadLine().ToUpper();
        while (repeat != "Y" && repeat != "N")
        {
            Console.WriteLine("Invalid Input");
            Console.WriteLine("Would you like to add another flight? (Y/N): ");
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
                        Console.WriteLine($"{f.FlightNumber,-15}{airlineDictionary[airlineCode2[0]].Name,-20}{f.Origin,-21}{f.Destination,-19}{f.ExpectedTime}");
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


        foreach (Flight flight in flightDictionary.Values) // Searching and Displaying the flight details
        {
            if (flight.FlightNumber == selectedFlightNumber)
            {
                // Flight Number, Airline Name, Origin, Destination, and Expected Departure/Arrival Time, Special Request Code

                Console.WriteLine();

                Console.WriteLine($"{"Flight Number",-15}" +
                    $"{"Airline Name",-20}" +
                    $"{"Origin",-21}" +
                    $"{"Destination",-19}" +
                    $"{"Expected Time",-25}" +
                    $"{"Status",-10}" +
                    $"{"Boarding Gate",-15}"
                    );

                Console.WriteLine($"{flight.FlightNumber,-15}" +
                    $"{airlineDictionary[airlineCode].Name,-20}" +
                    $"{flight.Origin,-21}" +
                    $"{flight.Destination,-19}" +
                    $"{flight.ExpectedTime,-25}" +
                    $"{flight.Status,-10}" +
                    $"{boardingGateDictionary.Values.FirstOrDefault(x => x.Flight?.FlightNumber == flight.FlightNumber)?.GateName ?? "Unassigned",-15}"
                    );
                return;
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
            foreach (Flight flight in AirlineFlightDictionary.Values)
            {
                string[] airlineCode2 = flight.FlightNumber.Split(' ');
                if (airlineCode2[1] == ChosenFlightSplit[1])
                {
                    
                    while (true)
                    {
                        //Basic Information except the Flight Number itself
                        //(i.e. Origin, Destination, and Expected Departure/Arrival Time),
                        //or Status, Special Request Code and update the Flight object’s information accordingly
                        string SpecialRequestCode = specialCodeDictionary[ChosenFlight];
                        Console.WriteLine();

                        Console.WriteLine($"{"Flight Number",-15}" +
                            $"{"Airline Name",-25}" +
                            $"{"Origin",-20}" +
                            $"{"Destination",-20}" +
                            $"{"Expected Time",-25}" +
                            $"{"Status",-10}" +
                            $"{"Special Request Code",-22}" +
                            $"{"Boarding Gate",-15}"
                            );

                        Console.WriteLine($"{flight.FlightNumber,-15}" +
                            $"{airlineDictionary[airlineCode2[0]].Name,-25}" +
                            $"{flight.Origin,-20}" +
                            $"{flight.Destination,-20}" +
                            $"{flight.ExpectedTime,-25}" +
                            $"{flight.Status,-10}" +
                            $"{SpecialRequestCode,-22}" +
                            $"{boardingGateDictionary.Values.FirstOrDefault(x => x.Flight?.FlightNumber == flight.FlightNumber)?.GateName ?? "Unassigned",-15}"
                            );


                        Console.WriteLine("What would you like to modify?");
                        Console.WriteLine("[1] Modify Basic Information");
                        Console.WriteLine("[2] Modify Status");
                        Console.WriteLine("[3] Modify Special Request Code");
                        Console.WriteLine("[4] Modify Boarding Gate");
                        Console.WriteLine("[5] Exit");
                        Console.WriteLine("Please enter an option: ");
                        string User_Action2 = Console.ReadLine();


                        // Modify Basic Information
                        if (User_Action2 == "1")
                        {
                            // Reused Function from method 6
                            flight.Origin = ValidateOriginDestination(flight.Origin, "Origin");
                            // Reused Function from method 6

                            flight.Destination = ValidateOriginDestination(flight.Destination, "Destination");



                            // Modify Time
                            string expectedTime = ""; // Place holder value
                            DateTime newTime = ValidateExpectedTime(expectedTime);
                            flight.ExpectedTime = newTime;
                        }

                        // Modify Status
                        else if (User_Action2 == "2")
                        {
                            Console.Write("Enter new Status: ");
                            flight.Status = Console.ReadLine();
                        }

                        // Modify Special Request Code
                        else if (User_Action2 == "3")
                        {
                            while (true)
                            {
                                Console.WriteLine("Possible Special Request Code: CFFT, DDJB, LWTT. Enter None if flight has no special request.");
                                Console.WriteLine("Enter Special Request Code: ");

                                try
                                {
                                    string New_Status = Console.ReadLine().Trim().ToUpper(); 
                                    // Trim removes whitespace
                                    // Ensures that when the enters the request code in lower case it is still accepted

                                    if (New_Status == "CFFT" || New_Status == "DDJB" || New_Status == "LWTTF")
                                    {
                                        specialCodeDictionary[flight.FlightNumber] = New_Status; // Assign the status directly from user input
                                        Console.WriteLine($"Status updated to: {flight.Status}");
                                        break;
                                    }

                                    else if (New_Status == "NONE")
                                    {
                                        specialCodeDictionary[flight.FlightNumber] = "NONE";
                                        Console.WriteLine("Special request code set to "+ New_Status);
                                        break;
                                    }

                                    else
                                    {
                                        throw new ArgumentException("Invalid Status");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message} - Please enter a valid status.");
                                }
                            }
                        }


                        // Modify Boarding Gate
                        else if (User_Action2 == "4")
                        {
                            try
                            {
                                while (true)
                                {
                                    ListBoardingGates();
                                    Console.WriteLine("Enter Boarding Gate Name: ");
                                    string boardingGate = Console.ReadLine().ToUpper();

                                    if (boardingGateDictionary.ContainsKey(boardingGate))
                                    {
                                        if (boardingGateDictionary[boardingGate].SupportsCFFT == true && specialCodeDictionary[flight.FlightNumber] == "CFFT")
                                        {
                                            boardingGateDictionary[boardingGate].Flight = flight;
                                            Console.WriteLine("Boarding Gate has been assigned to Flight");
                                            break;
                                        }

                                        else if (boardingGateDictionary[boardingGate].SupportsDDJB == true && specialCodeDictionary[flight.FlightNumber] == "DDJB")
                                        {
                                            boardingGateDictionary[boardingGate].Flight = flight;
                                            Console.WriteLine("Boarding Gate has been assigned to Flight");
                                            break;
                                        }

                                        else if (boardingGateDictionary[boardingGate].SupportsLWTT == true && specialCodeDictionary[flight.FlightNumber] == "LWTT")
                                        {
                                            boardingGateDictionary[boardingGate].Flight = flight;
                                            Console.WriteLine("Boarding Gate has been assigned to Flight");
                                            break;
                                        }

                                        else
                                        {
                                            throw new ArgumentException("Boarding Gate does not support the Special Request Code of the Flight");
                                        }
                                    }

                                    else
                                    {
                                        throw new ArgumentException("Invalid Boarding Gate");
                                    }
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex}");
                            }
                        }

                        else if (User_Action2 == "5")
                        {
                            Console.WriteLine("Flight updated!");
                            Console.WriteLine($"Flight Number: {flight.FlightNumber,-15}" +
                            $"\nAirline Name: {airlineDictionary[airlineCode2[0]].Name,-25}" +
                            $"\nOrigin: {flight.Origin,-20}" +
                            $"\nDestination: {flight.Destination,-20}" +
                            $"\nExpected Departure/Arrival Time: {flight.ExpectedTime,-25}" +
                            $"\nStatus: {flight.Status,-10}" +
                            $"\nSpecial Request Code: {SpecialRequestCode,-22}" +
                            $"\nBoarding Gate: {boardingGateDictionary.Values.FirstOrDefault(x => x.Flight?.FlightNumber == flight.FlightNumber)?.GateName ?? "Unassigned",-15}"
                            );
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


            foreach (Flight flight in AirlineFlightDictionary.Values)
            {
                string[] airlineCode2 = flight.FlightNumber.Split(' ');

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

                    Console.WriteLine($"{flight.FlightNumber,-15}" +
                        $"{airlineDictionary[airlineCode2[0]].Name,-25}" +
                        $"{flight.Origin,-20}" +
                        $"{flight.Destination,-20}" +
                        $"{flight.ExpectedTime,-25}" +
                        $"{flight.Status,-20}");
                }
            }

            while (true)
            {
                Console.WriteLine("Are you sure you want to delete this flight? (Y/N)");
                string User_Action3 = Console.ReadLine();


                if (User_Action3 == "Y")
                {
                    foreach (Flight flight in AirlineFlightDictionary.Values)
                    {
                        string[] airlineCode2 = flight.FlightNumber.Split(' ');
                        if (airlineCode2[1] == ChosenFlightSplit[1])
                        {
                            // Removes flight from flight dictionary
                            flightDictionary.Remove(flight.FlightNumber);
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