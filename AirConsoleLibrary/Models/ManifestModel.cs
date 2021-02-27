using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirConsoleLibrary.Models
{
    class ManifestModel
    {
        public List<GridSpotModel> SeatsAvailable { get; private set; }
        public Dictionary<GridSpotModel, PassengerModel> SeatsTaken { get; set; }

    }
}
