﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core
{
    public enum CarType
    {
        Passenger,
        Truck,
        Bus,
        Motorcycle
    }
    public class Car
    {

        public string ID { get; protected set; }
        public double Balance { get; protected set; }
        public CarType Type { get; protected set; }

        public Car(CarType type)
        {
            Type = type;
            ID = Guid.NewGuid().ToString();
            Balance = 0;
        }

        public void AddMoney(double value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("You can add only positive values");
            else
                Balance += value;
        }

    }
}