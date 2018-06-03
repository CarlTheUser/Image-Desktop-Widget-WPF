using System.IO;

namespace GeneralMerchandise.Logging
{
    public sealed class TextLogger : ILogger
    {

        #region Singleton
        private static readonly object theLock = new object();

        private static volatile TextLogger instance;

        public static TextLogger GetInstance(string logFileLocation)
        {
            if (instance != null) return instance;

            lock (theLock)
            {
                if (instance == null)
                {
                    instance = new TextLogger(logFileLocation);
                }
            }

            instance.LogFileLocation = logFileLocation;

            return instance;

        }

        #endregion

        public string LogFileLocation { get; set; }

        private TextLogger(string logfileLocation)
        {
            LogFileLocation = logfileLocation;
            CheckLogFile();
        }

        public void Log(string message)
        {
            CheckLogFile();

            using (TextWriter tw = new StreamWriter(LogFileLocation, true))
            {
                tw.WriteLine(message);
                tw.Close();
            }
        }

        private void CheckLogFile()
        {

            if (!File.Exists(LogFileLocation))
            {
                File.Create(LogFileLocation).Dispose();
            }
        }
    }
}
