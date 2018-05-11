using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Core;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            var parking = Core.Parking.Instanse;
            string guid;
            parking.AddCar(CarType.Bus, out guid);
        }
    }
}
