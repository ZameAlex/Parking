using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Parking.Core
{
    class Settings
    {
        public int Timeout { get; private set; }
        public Dictionary<string,double> TaxesForCarType { get; private set; }
        public int ParkingSpace { get; private set; }
        public double Fine { get; private set; }
        public int logTimeout { get; private set; }
        public string logPath { get; set; }

        /// <summary>
        /// Using App.config for storing settings and ConfigurationManager for reading
        /// </summary>
        public Settings()
        {
            try
            {
                var generalSettings = (ConfigurationManager.GetSection("parkingSettings/generalSettings") as System.Collections.Hashtable)
                    .Cast<System.Collections.DictionaryEntry>()
                    .ToDictionary(item => item.Key.ToString(), item => item.Value.ToString());
                Timeout = Convert.ToInt32(generalSettings["timeout"]);
                ParkingSpace = Convert.ToInt32(generalSettings["parkingSpace"]);
                Fine = Convert.ToDouble(generalSettings["fine"]);
                logTimeout = Convert.ToInt32(generalSettings["logTimeout"]);
                logPath = generalSettings["pathToLog"];
                TaxesForCarType = new Dictionary<string, double>();
                var paymentSettings = (ConfigurationManager.GetSection("parkingSettings/paymentSettings") as System.Collections.Hashtable)
                    .Cast<System.Collections.DictionaryEntry>()
                    .ToDictionary(item => item.Key.ToString(), item => item.Value.ToString());
                foreach (var item in paymentSettings)
                    TaxesForCarType.Add(item.Key, Convert.ToDouble(item.Value));
            }
            catch (InvalidCastException ICEx)
            {
                Console.WriteLine($"Exception occured : {ICEx.Message} \nError while casting settings, it will be filled by default");
                SetDefaultSettings();
            }
            catch(FormatException FEx)
            {
                Console.WriteLine($"Exception occured : {FEx.Message} \nInvalid parameters format, settings will be filled by default");
                SetDefaultSettings();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured : {ex.Message} \nSettings will be filled by default");
                SetDefaultSettings();
            }
            finally
            {
               
            }
        }

        private void SetDefaultSettings()
        {
            Timeout = 3;
            ParkingSpace = 25;
            Fine = 2.5;
            logPath = "Transactions.log";
            logTimeout = 60;
            TaxesForCarType = new Dictionary<string, double>();
            TaxesForCarType.Add(CarType.Passenger.ToString(), 3);
            TaxesForCarType.Add(CarType.Truck.ToString(), 5);
            TaxesForCarType.Add(CarType.Bus.ToString(), 2);
            TaxesForCarType.Add(CarType.Motorcycle.ToString(), 1);
        }

        
    }
}
