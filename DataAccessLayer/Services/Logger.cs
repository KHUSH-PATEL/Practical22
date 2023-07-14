using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class Logger
    {
        private string _logPath = @"C:\Users\khush\source\repos\Design Pattern\Practical22\DataAccessLayer\Logs\logs.txt";
        public void Log(string message)
        {
            string formatMessage = $"{DateTime.Now} : {message}";
            using (StreamWriter sr = File.AppendText(_logPath))
            {
                try
                {
                    sr.WriteLine(formatMessage);
                }
                catch (Exception ex)
                {
                    string value = $"An exception has occured : {ex.Message}";
                    sr.WriteLine(value);
                }
            }
        }
    }
}
