using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core
{
    public class Transaction
    {
        public readonly DateTime transacionTime;
        public DateTime TransactionTime { get { return transacionTime; } }
        public string GUID { get; protected set; }
        public double Tax { get; protected set; }

    }
}
