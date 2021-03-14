using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConsoleLibrary.Models
{
    public class PassengerSeatingModel
    {
        public Enums.SeatLetters SeatLetter { get; set; }
        public int SeatNumber { get; set; }
        public PassengerModel passenger { get; set; }
        public Enums.SeatStatus Availability { get; set; } = Enums.SeatStatus.Available;
    }
}
