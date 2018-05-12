using System;
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
        static void Main(string[] args)
        {
            try
            {
                menus = new List<Menu>();
                menus.Add(new Menu(BackSpaceMethodFirstLevel,
                    MenuMethods.AddCar,
                    MenuMethods.DeleteCar,
                    MenuMethods.ShowCar,
                    MenuMethods.AddMoney,
                    MenuMethods.ShowHistory,
                    MenuMethods.ShowBalance,
                    MenuMethods.ShowBalancePerMinute,
                    MenuMethods.ShowPlaces,
                    MenuMethods.ShowLog));
                menus.Add(new Menu(BackSpaceMethodSecondLevel,
                    MenuMethods.Bus,
                    MenuMethods.Passenger,
                    MenuMethods.Motorcycle,
                    MenuMethods.Truck));
                Menu currentMenu = menus.First();
                currentMenu.Show();
            }
            finally
            {
                Core.Parking.Instanse.Dispose();
            }

        }
        public static void BackSpaceMethodFirstLevel()
        {
            Console.Clear();
            currentMenu.Show();
        }

        public static void BackSpaceMethodSecondLevel()
        {
            Console.Clear();
            currentMenu = menus.First();
            currentMenu.Show();
        }

        public static void SelectSecondMenu()
        {
            currentMenu = menus.Last();
            currentMenu.Show();
        }

    }
}
