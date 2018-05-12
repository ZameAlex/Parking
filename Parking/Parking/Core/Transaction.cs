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
        public string CarID { get; protected set; }
        public double Tax { get; protected set; }
        public Transaction(string id, double tax)
        {
            CarID = id;
            Tax = tax;
            transacionTime = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Time: {TransactionTime}; Car: {CarID}; Tax: {Tax}\n";
        }
    }
}
