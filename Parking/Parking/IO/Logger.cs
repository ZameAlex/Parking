﻿using Parking.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.IO
{
    public class Logger
    {
        private FileStream logFile;
        public Logger(string fileName)
        {
            logFile = new FileStream(fileName, FileMode.CreateNew);
        }
        public void WriteLog(double balance)
        {
            using (StreamWriter writer = new StreamWriter(logFile))
            {
                writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {balance}");
            }
        }

        public string ReadLog()
        {
            StringBuilder builder = new StringBuilder();
            using (StreamReader reader = new StreamReader(logFile))
            {
                builder.Append(reader.ReadLine());
            }
            return builder.ToString();
        }

        public void WriteException(string message, string fileName)
        {
            if(String.IsNullOrEmpty(fileName))
                using (StreamWriter writer = new StreamWriter(logFile.Name, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToLongTimeString()}\tERR\t{message}");
                }
            else
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine($"{DateTime.Now.ToLongTimeString()}\tERR\t{message}");
                }
        }
       
    }
}