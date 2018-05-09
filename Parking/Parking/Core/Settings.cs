using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core
{
    class Settings
    {
        public int Timeout { get; set; }
        public Dictionary<string,double> TaxesForCarType { get; set; }
        public int ParkingSpace { get; set; }
        public double Fine { get; set; }
    }
}
