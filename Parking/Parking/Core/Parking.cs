using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        public static Parking Instanse { get { return lazy.Value; } }

        private readonly Settings settings;

        List<Car> cars;
        List<Transaction> transactions;
        public double Balance { get; private set; }

        private Parking()
        {
            settings = new Settings();

        }
    }
}
