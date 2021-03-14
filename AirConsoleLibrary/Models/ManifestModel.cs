using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConsoleLibrary.Models
{
    public class ManifestModel
    {
        public List<PassengerSeatingModel> Seats { get; private set; } = new List<PassengerSeatingModel>();
        public List<PassengerSeatingModel> SeatsTaken { get; private set; } = new List<PassengerSeatingModel>();

        public ManifestModel()
        {
            InitializeSeats();
        }
        private void InitializeSeats()
        {
            var seatLetters = EnumHelper.GetValues<Enums.SeatLetters>();
            foreach(int rowNumber in Enumerable.Range(1,8))
            {
                foreach(var letter in seatLetters)
                {
                    PassengerSeatingModel seat = new PassengerSeatingModel();
                    seat.SeatNumber = rowNumber;
                    seat.SeatLetter = letter;
                    Seats.Add(seat);
                }
            }
        }

        public void AddPassenger(PassengerModel passenger, PassengerSeatingModel seat)
        {
            try
            {
                seat.passenger = passenger;
                seat.Availability = Enums.SeatStatus.Taken;
                SeatsTaken.Add(seat);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}
