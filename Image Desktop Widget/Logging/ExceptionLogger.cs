using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public sealed class ExceptionLogger
    {

        public ILogger Logger { get; set; }

        public ExceptionLogger(ILogger logger)
        {
            Logger = logger;
        }

        public void LogException(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(DateTime.Now.ToString())
                .AppendLine(ex.Message)
                .AppendLine(ex.Source)
                .AppendLine(ex.StackTrace);

            Logger.Log(sb.ToString());

        }

    }
}
