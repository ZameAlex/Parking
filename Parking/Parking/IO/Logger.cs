using Parking.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.IO
{
    public static class Logger
    {
        public static void WriteLog(double balance, string logFile)
        {
            using (StreamWriter writer = new StreamWriter(logFile, true))
            {
                writer.WriteLine($"Time: {DateTime.Now.ToLongTimeString()}; Balance: {balance}");
            }
        }

        public static string ReadLog(string logFile)
        {
            StringBuilder builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(logFile))
            {
                while (!reader.EndOfStream)
                {
                    builder.Append(reader.ReadLine());
                    builder.Append('\n');
                }
            }
            return builder.ToString();
        }

        public static void WriteException(string message, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine($"{DateTime.Now.ToLongTimeString()}\tERR\t{message}");
            }
        }
       
    }
}
