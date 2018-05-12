using Parking.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.Core
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());
        public static Parking Instanse { get { return lazy.Value; } }

        private readonly Settings settings;
        Logger logger;

        private List<Car> cars;
        private Dictionary<Car, Timer> carTimers;

        private List<Transaction> transactions;

        private List<Transaction> transactionsForLogging;
        private Timer logTimer;

        private Timer deleteOldTransactionsTimer;

        public double Balance { get; private set; }

        private Parking()
        {
            settings = new Settings();
            cars = new List<Car>();
            transactions = new List<Transaction>();
            transactionsForLogging = new List<Transaction>();
            carTimers = new Dictionary<Car, Timer>();
            logger = new Logger("Transactions.log");
            logTimer = new Timer(Log, null, 60000, 60000);
            deleteOldTransactionsTimer = new Timer(DeleteOldTransaction, null, 61000, 1000);
        }
        /// <summary>
        /// If ParkingSpace>cars.Count, we can add car
        /// </summary>
        /// <param name="type">CarType</param>
        /// <param name="Guid">Returns ID of new Car</param>
        /// <returns>True, if car was added suceesful, false, if parking space is not enough</returns>
        public bool AddCar(CarType type, double balance, out string Guid)
        {
            try
            {
                if (settings.ParkingSpace > cars.Count)
                {
                    var tempCar = new Car(type);
                    tempCar.AddMoney(balance);
                    Guid = tempCar.ID;
                    cars.Add(tempCar);
                    var timer = new Timer(Payment, tempCar, settings.Timeout * 1000, settings.Timeout * 1000);
                    carTimers.Add(tempCar, timer);
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
                    carTimers[carForRemove].Dispose();
                    carTimers.Remove(carForRemove);
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

        private double CalculateTax(Car car)
        {
            if (Balance < 0)
                return settings.Fine * settings.TaxesForCarType[car.Type.ToString()];
            return settings.TaxesForCarType[car.Type.ToString()];
        }

        #region ShowMethods

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
            return logger.ReadLog();
        }
        #endregion ShowMethods


        private void Payment(object obj)
        {
            try
            {
                var car = obj as Car;
                Transaction transaction;
                var tax = CalculateTax(car);
                car.Pay(CalculateTax(car), out transaction);
                Balance += tax;
                transactions.Add(transaction);
            }
            catch (Exception ex)
            {
                logger.WriteException(ex.Message, "Parking.log");
            }
        }

        private void Log(object obj)
        { 
            double balance=0;
            foreach (var t in transactionsForLogging)
                balance += t.Tax;
            logger.WriteLog(balance);
        }

        private void DeleteOldTransaction(object obj)
        {
            for(int counter = transactions.Count-1;counter>0;counter--)
            {
                if (transactions[counter].TransactionTime < DateTime.Now.AddMinutes(-1))
                    transactions.RemoveAt(counter);
                else
                    break;
            }
        }

        ~Parking()
        {
            foreach (var item in carTimers)
                item.Value.Dispose();
            deleteOldTransactionsTimer.Dispose();
            logTimer.Dispose();
        }
    }

}

