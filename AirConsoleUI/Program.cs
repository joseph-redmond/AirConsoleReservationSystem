using AirConsoleLibrary.Models;
using System;
namespace AirConsoleUI
{
    class Program
    {
        /* Maybe
        enum MainMenuSelections
        {
            R,
            S,
            X

        }*/
        static void Main(string[] args)
        {
            ManifestModel manifest = new ManifestModel();

            string rawUserInput = "";

            do
            {
                rawUserInput = MainMenuGetUserInput();
                if (rawUserInput.ToUpper() == "R")
                {
                    Console.Clear();
                    string flightClass = FlightClassPrompt();
                    if (flightClass.ToUpper() == "B")
                    {
                        Console.Clear();
                        PrintBuisnessClassGrid(manifest);
                        Console.WriteLine();
                        Console.Write("Please Select the passengers seat (ex - 1A): ");
                        string userInput = Console.ReadLine();
                        (int rowNumber, char seatLetter) = splitUserInput(userInput);
                        PassengerSeatingModel seat = GetUserSelectedSeat((rowNumber, seatLetter, manifest));// fix
                        bool seatEmpty = VerifySeatIsAvailable((seat, manifest));
                        if (seatEmpty)
                        {
                            PassengerModel passenger = CreatePassenger();
                            manifest.AddPassenger(passenger, seat);
                            Console.WriteLine($"{passenger.FullName} was added to the manifest with seat {seat.SeatNumber}{seat.SeatLetter}");
                        }
                        else
                        {
                            Console.WriteLine($"Seat is not available.");
                        }
                        Console.WriteLine("Press any key to go back to main menu");
                        Console.ReadKey();
                    }
                    //TODO loop the selections here so it doesnt just go back to main menu
                    else if (flightClass.ToUpper() == "E")
                    {
                        PrintEconomyClassGrid(manifest);
                        Console.WriteLine();
                        Console.Write("Please Select the passengers seat (ex - 1A): ");
                        string userInput = Console.ReadLine();
                        (int rowNumber, char seatLetter) = splitUserInput(userInput);
                        PassengerSeatingModel seat = GetUserSelectedSeat((rowNumber, seatLetter, manifest));// fix
                        bool seatEmpty = VerifySeatIsAvailable((seat, manifest));
                        if (seatEmpty)
                        {
                            PassengerModel passenger = CreatePassenger();
                            manifest.AddPassenger(passenger, seat);
                            Console.WriteLine($"Successfully added {passenger.FullName} with seat {seat.SeatNumber}{seat.SeatLetter} to the manifest");
                        }
                        else
                        {
                            Console.WriteLine("Seat is not available");
                        }
                        Console.Write("Press any key to go back to main menu");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid input");
                    }
                }
                else if (rawUserInput.ToUpper() == "S")
                {
                    Console.Clear();
                    string seatID = "";
                   
                    Console.WriteLine("Seat Verification");
                    Console.Write("Please enter the seat id (ex - 1A): ");
                    seatID = Console.ReadLine();
                    (int seatRow, char seatLetter) = splitUserInput(seatID);
                    PassengerSeatingModel seat = GetUserSelectedTakenSeat((seatRow, seatLetter, manifest));
                    try
                    {
                        if (seat.passenger.FirstName != null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Passenger Details");
                            Console.WriteLine($"Firstname: {seat.passenger.FirstName}");
                            Console.WriteLine($"Lastname: {seat.passenger.LastName}");
                            Console.WriteLine($"Passport Number: {seat.passenger.PassportNumber}");
                            Console.WriteLine();
                            Console.Write("Please press any key to continue to main menu: ");
                            Console.ReadKey();
                        }
                    }
                    catch(NullReferenceException ex) {
                        Console.WriteLine("The seat entered was not found");
                        Console.WriteLine("Please press any key to continue to main menu: ");
                        Console.ReadKey();
                    }
                }
            } while (rawUserInput.ToUpper() != "X");
            Console.WriteLine("Thank you for using our system today");
        }

        private static PassengerSeatingModel GetUserSelectedTakenSeat((int seatRow, char seatLetter, ManifestModel manifest) enteredInfo)
        {
            foreach (var seat in enteredInfo.manifest.SeatsTaken)
            {
                if (seat.SeatNumber == enteredInfo.seatRow)
                {
                    if (Convert.ToChar(seat.SeatLetter.ToString()) == enteredInfo.seatLetter)
                    {
                        return seat;
                    }
                }
            }
            return new PassengerSeatingModel();
        }

        private static (int seatRow, char seatLetter) splitUserInput(string userInput)
        {
            int seatRow = 0;
            char seatLetter = '0';
            try
            {
                seatRow = Convert.ToInt32(userInput.Substring(0, 1));
                seatLetter = Convert.ToChar(userInput.Substring(1, 1));
                return (seatRow, seatLetter);
            }
            catch (Exception ex)
            {
                
            }
            return (seatRow, seatLetter);
        }


        private static void PrintEconomyClassGrid(ManifestModel manifest)
        {
            var currentRow = 0;
            foreach (var seat in manifest.Seats)
            {
                if (seat.SeatNumber > 8)
                {
                    break;
                }
                if (currentRow == 0)
                {
                    Console.Write("  ");
                    Console.Write(" A ");
                    Console.Write(" B ");
                    Console.Write(" C ");
                    Console.Write(" D ");
                    Console.Write(" E ");
                }
                if (currentRow < 5)
                {
                    currentRow = seat.SeatNumber;
                    continue;
                }
                if (currentRow != seat.SeatNumber)
                {
                    currentRow = seat.SeatNumber;
                    Console.WriteLine();
                    Console.Write(currentRow + " ");
                }
                if (seat.Availability == Enums.SeatStatus.Taken)
                {
                    Console.Write(" X ");
                }
                else if (seat.Availability == Enums.SeatStatus.Available)
                {
                    Console.Write("   ");
                }
            }

        }

        private static void PrintBuisnessClassGrid(ManifestModel manifest)
        {
            var currentRow = 0;
            foreach (var seat in manifest.Seats)
            {
                if (seat.SeatNumber > 5)
                {
                    break;
                }
                if (currentRow == 0)
                {
                    Console.Write("  ");
                    Console.Write(" A ");
                    Console.Write(" B ");
                    Console.Write(" C ");
                    Console.Write(" D ");
                    Console.Write(" E ");
                }
                if (currentRow != seat.SeatNumber)
                {
                    currentRow = seat.SeatNumber;
                    Console.WriteLine();
                    Console.Write(currentRow + " ");
                }
                if (seat.Availability == Enums.SeatStatus.Taken)
                {
                    Console.Write(" X ");
                }
                else if (seat.Availability == Enums.SeatStatus.Available)
                {
                    Console.Write("   ");
                }
            }
        }

        private static PassengerSeatingModel GetUserSelectedSeat((int rowNumber, char seatLetter, ManifestModel manifest) enteredInfo)
        {
            foreach (var seat in enteredInfo.manifest.Seats)
            {
                if (seat.SeatNumber == enteredInfo.rowNumber)
                {
                    if (Convert.ToChar(seat.SeatLetter.ToString()) == enteredInfo.seatLetter)
                    {
                        return seat;
                    }
                }
            }
            return new PassengerSeatingModel();
        }

        private static bool VerifySeatIsAvailable((PassengerSeatingModel seat, ManifestModel manifest) enteredInfo)
        {
            bool seatStatus = false;
            foreach (var seat in enteredInfo.manifest.Seats)
            {
                if (seat.SeatNumber == enteredInfo.seat.SeatNumber)
                {
                    if (seat.SeatLetter == enteredInfo.seat.SeatLetter)
                    {
                        if (seat.Availability == Enums.SeatStatus.Available)
                        {
                            seatStatus = true;
                            break;
                        }
                        else
                        {
                            return seatStatus;
                        }
                    }
                }
            }
            return seatStatus;
        }

        private static PassengerModel CreatePassenger()
        {
            string firstName = "";
            string lastName = "";
            string passportNumber = "";
            do
            {
                Console.Write("What is your first name: ");
                firstName = Console.ReadLine();
                Console.Write("What is your last name: ");
                lastName = Console.ReadLine();
                Console.Write("What is your passport number: ");
                passportNumber = Console.ReadLine();
                if(firstName.Trim() == "" || lastName.Trim() == "" || passportNumber.Trim() == "")
                {
                    Console.WriteLine("You left fields blank please try again");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (firstName.Trim() == "" || lastName.Trim() == "" || passportNumber.Trim() == "");
            //TODO Verify User inputted data
            PassengerModel passenger = new PassengerModel(firstName, lastName, passportNumber);
            return passenger;
        }

        private static string FlightClassPrompt()
        {
            Console.WriteLine("Seating Classes");
            Console.WriteLine("E: Economy");
            Console.WriteLine("B: Business");
            Console.Write("What type of seating is the passenger looking for: ");
            return Console.ReadLine();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to The Air Control Reservation System.");
        }

        public static string MainMenuGetUserInput()
        {
            Console.Clear();
            WelcomeMessage();
            Console.WriteLine("Please Select from the options below");
            Console.WriteLine();
            Console.WriteLine("R: Reservation");
            Console.WriteLine("S: Seat Verification");
            Console.WriteLine("X: Exit The System");
            return Console.ReadLine();
        }
    }
}
