using System;

namespace GeneralMerchandise.Logging
{
    public sealed class ConsoleLogger : ILogger
    {
        #region Singleton

        private static readonly object theLock = new object();

        private static volatile ConsoleLogger instance;

        public static ConsoleLogger Instance
        {
            get
            {
                if (instance != null) return instance;

                lock (theLock)
                {
                    if (instance == null)
                    {
                        instance = new ConsoleLogger();
                    }
                }

                return instance;
            }
        }

        #endregion

        private ConsoleLogger()
        {

        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
