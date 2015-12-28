using System;
using System.IO;

namespace PixelloTools.Logging
{
    public class ConsoleLogger :Ilogger
    {
        private static Ilogger _ilogger;

        private ConsoleLogger() { }
        
        public static Ilogger GetLoger()
        {
            return _ilogger ?? (_ilogger = new ConsoleLogger());
        }

        public TextWriter GetLoggerTextWriter() => Console.Out;

        public void LogIt(object log) => Console.WriteLine(log);
    }
}
