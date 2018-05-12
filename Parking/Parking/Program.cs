﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Core;
using Parking.IO;
using System.Threading;

namespace Parking
{
    class Program
    {
        static List<Menu> menus;
        static Menu currentMenu;
        static Logger logger;
        static void Main(string[] args)
        {
            menus = new List<Menu>();
            logger = new Logger("Parking.log");
            menus.Add(new Menu(BackSpaceMethodFirstLevel, AddCar, DeleteCar, ShowCar, AddMoney, ShowHistory, ShowBalance, ShowBalancePerMinute, ShowPlaces, ShowLog));
            menus.Add(new Menu(BackSpaceMethodSecondLevel, Bus, Passenger, Motorcycle, Truck));
            Menu currentMenu = menus.First();
            currentMenu.Show();

        }

        #region FirstMenuLevelMethods
        static void BackSpaceMethodFirstLevel()
        {
            Console.Clear();
            currentMenu.Show();
        }

        static void BackSpaceMethodSecondLevel()
        {
            Console.Clear();
            currentMenu = menus.First();
            currentMenu.Show();
        }

        static void AddCar()
        {
            Console.Clear();
            Console.WriteLine("Select car type");
            currentMenu = menus.Last();
            currentMenu.Show();
        }

        static void DeleteCar()
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
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Car wasn`t deleted!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }

        }

        static void AddMoney()
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
            catch(FormatException ex)
            {
                Console.WriteLine("Wrong format!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Too big number!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Car didn`t find!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Car didn`t find!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            Console.WriteLine("Money was added succesfuly");

        }

        static void ShowHistory()
        {
            Console.Clear();
            var transactions = Core.Parking.Instanse.ShowTransactions();
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction.ToString());
            }
        }

        static void ShowBalance()
        {
            Console.Clear();
            Console.WriteLine($"Total income: {Core.Parking.Instanse.ShowIncome(false)}");
        }

        static void ShowBalancePerMinute()
        {
            Console.Clear();
            Console.WriteLine($"Income for last minute: {Core.Parking.Instanse.ShowIncome(true)}");
        }

        static void ShowPlaces()
        {
            Console.WriteLine($@"Places:\nTotal: { Core.Parking.Instanse.TotalPlaces()},
                                Free: { Core.Parking.Instanse.FreePlaces()},
                                 Occupied: {  Core.Parking.Instanse.OccupiedPlaces()}");
        }
        static void ShowLog()
        {
            Console.Clear();
            Console.WriteLine(Core.Parking.Instanse.ShowLog());
        }

        static void ShowCar()
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
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Car didn`t find!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Car didn`t find!");
                logger.WriteException(ex.Message, "Parking.log");
                return;
            }
        }
        #endregion FirstMenuLevelMethods



        #region SecondMenuLevelMethods
        static void Bus()
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
        static void Passenger()
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

        static void Truck()
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

        static void Motorcycle()
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

        static bool EnterBalance(out double balance)
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
