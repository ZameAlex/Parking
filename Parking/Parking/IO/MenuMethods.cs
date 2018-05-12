using Parking.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parking.IO
{
    public static class MenuMethods
    {
        #region FirstMenuLevelMethods
        

        public static void AddCar()
        {
            Console.Clear();
            Console.WriteLine("Select car type");
            Program.SelectSecondMenu();
        }

        public static void DeleteCar()
        {
            Console.Clear();
            Console.WriteLine("Enter car ID");
            var id = Console.ReadLine();
            try
            {
                var result = Core.Parking.Instanse.RemoveCar(id);
                if (result)
                    Console.WriteLine("Car was removed succesfuly");
                else
                    Console.WriteLine("Sou shold add money to car!");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Car didn`t find!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Car wasn`t deleted!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }

        }

        public static void AddMoney()
        {
            Console.Clear();
            Console.WriteLine("Enter car id");
            var id = Console.ReadLine();
            Console.WriteLine("Enter value of replenish");
            try
            {
                var money = Convert.ToDouble(Console.ReadLine());
                if (money < 0)
                {
                    Console.WriteLine("You should enter positive number!");
                    Thread.Sleep(1500);
                    return;
                }

                Core.Parking.Instanse.AddBalance(id, money);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Wrong format!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Too big number!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Car didn`t find!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Car didn`t find!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            Console.WriteLine("Money was added succesfuly");

        }

        public static void ShowHistory()
        {
            Console.Clear();
            var transactions = Core.Parking.Instanse.ShowTransactions();
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        public static void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine($"Total income: {Core.Parking.Instanse.ShowIncome(false)}");
        }

        public static void ShowBalancePerMinute()
        {
            Console.Clear();
            Console.WriteLine($"Income for last minute: {Core.Parking.Instanse.ShowIncome(true)}");
        }

        public static void ShowPlaces()
        {
            Console.WriteLine($@"Places:\nTotal: { Core.Parking.Instanse.TotalPlaces()},
                                Free: { Core.Parking.Instanse.FreePlaces()},
                                 Occupied: {  Core.Parking.Instanse.OccupiedPlaces()}");
        }
        public static void ShowLog()
        {
            Console.Clear();
            Console.WriteLine(Core.Parking.Instanse.ShowLog());
        }

        public static void ShowCar()
        {
            Console.Clear();
            Console.WriteLine("Enter car ID");
            var guid = Console.ReadLine();
            try
            {
                Console.WriteLine(Core.Parking.Instanse.ShowCar(guid));
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Car didn`t find!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Car didn`t find!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Car didn`t find!");
                Logger.WriteException(ex.Message, "Parking.log");
                return;
            }
        }
        #endregion FirstMenuLevelMethods



        #region SecondMenuLevelMethods
        public static void Bus()
        {
            Console.Clear();
            Console.WriteLine("Enter balance");
            double balance;
            var result = EnterBalance(out balance);
            if (result)
            {
                string guid;
                Core.Parking.Instanse.AddCar(CarType.Bus, balance, out guid);
                Console.WriteLine("Car was added sucesfully");
                Console.WriteLine($"Car ID: {guid}");
            }
        }
        public static void Passenger()
        {
            Console.Clear();
            Console.WriteLine("Enter balance");
            double balance;
            var result = EnterBalance(out balance);
            if (result)
            {
                string guid;
                Core.Parking.Instanse.AddCar(CarType.Passenger, balance, out guid);
                Console.WriteLine("Car was added sucesfully");
                Console.WriteLine($"Car ID: {guid}");
            }
        }

        public static void Truck()
        {
            Console.Clear();
            Console.WriteLine("Enter balance");
            double balance;
            var result = EnterBalance(out balance);
            if (result)
            {
                string guid;
                Core.Parking.Instanse.AddCar(CarType.Truck, balance, out guid);
                Console.WriteLine("Car was added sucesfully");
                Console.WriteLine($"Car ID: {guid}");
            }
        }

        public static void Motorcycle()
        {
            Console.Clear();
            Console.WriteLine("Enter balance");
            double balance;
            var result = EnterBalance(out balance);
            if (result)
            {
                string guid;
                Core.Parking.Instanse.AddCar(CarType.Motorcycle, balance, out guid);
                Console.WriteLine("Car was added sucesfully");
                Console.WriteLine($"Car ID: {guid}");
            }
        }

        public static bool EnterBalance(out double balance)
        {
            balance = Convert.ToDouble(Console.ReadLine());
            if (balance < 0)
            {
                Console.WriteLine("Write positive number!");
                Thread.Sleep(1500);
                return false;
            }
            return true;
        }
        #endregion SecondMenuLevelMethods

    }
}
