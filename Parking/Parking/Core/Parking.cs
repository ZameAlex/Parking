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
        List<Transaction> transactionsForLogging;
        public double Balance { get; private set; }

        private Parking()
        {
            settings = new Settings();
            cars = new List<Car>();
            transactions = new List<Transaction>();
        }
        /// <summary>
        /// If ParkingSpace>cars.Count, we can add car
        /// </summary>
        /// <param name="type">CarType</param>
        /// <param name="Guid">Returns ID of new Car</param>
        /// <returns>True, if car was added suceesful, false, if parking space is not enough</returns>
        public bool AddCar(CarType type,double balance, out string Guid)
        {
            try
            {
                if (settings.ParkingSpace > cars.Count)
                {
                    var tempCar = new Car(type);
                    tempCar.AddMoney(balance);
                    Guid = tempCar.ID;
                    cars.Add(tempCar);

                    return true;
                }
                Guid = null;
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Guid">ID of the target car to remove it</param>
        /// <param name="balance">returns true, if balance is more than 0, due to know, what the reason of false</param>
        /// <returns>True, if remove was succeded, falce, if balance is not enough</returns>
        /// <exception>Can throw ArgumentNullException, if car was not found</exception>
        public bool RemoveCar(string Guid)
        {
            try
            {
                var carForRemove = cars.Find(car => car.ID == Guid);
                if (carForRemove.Balance > 0)
                {
                    cars.Remove(carForRemove);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Add sum to balance car
        /// </summary>
        /// <param name="Guid">Id of car</param>
        /// <param name="money">Sum, what you want to add</param>
        /// <returns></returns>
        /// <exception>Can throw ArgumentNullException, if car was not found</exception>
        public bool AddBalance(string Guid, double money)
        {
            try
            {
                var carForAddMoney = cars.Find(car => car.ID == Guid);
                carForAddMoney.AddMoney(money);
                return true;
            }
            catch (ArgumentNullException ex)
            {
                throw;
            }
        }

        private void Payment()
        {
            foreach(var car in cars)
            {
                Transaction transaction;
                car.Pay(CalculateTax(car), out transaction);
                transactions.Add(transaction);
            }
        }

        private double CalculateTax(Car car)
        {
            if (Balance < 0)
                return settings.Fine * settings.TaxesForCarType[car.Type.ToString()];
            return settings.TaxesForCarType[car.Type.ToString()];
        }

        public List<Transaction> ShowTransactions()
        {
            return transactions;
        }

        public int TotalPlaces()
        {
            return settings.ParkingSpace;
        }

        public int FreePlaces()
        {
            return settings.ParkingSpace - cars.Count;
        }

        public int OccupiedPlaces()
        {
            return cars.Count;
        }

        public double ShowIncome(bool minute)
        {
            if (minute)
                return Balance;
            else
            {
                return MinuteIncomeCalculate();
            }
        }

        private double MinuteIncomeCalculate()
        {
            double income = 0;
            foreach (var transaction in transactions)
            {
                income += transaction.Tax;
            }
            return income;
        }

        public string ShowLog()
        {
            return null;
        }
    }
}
