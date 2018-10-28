using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using LibLogger = Microsoft.Practices.EnterpriseLibrary.Logging.Logger;

namespace CardValidationService.Logging
{
    public static class Logger
    {
        static Logger()
        {
            var logWriter = new LogWriterFactory().Create();
            LibLogger.SetLogWriter(logWriter, false);
        }

        public static void Info(string title, string message)
            => LibLogger.Write(new LogEntry
            {
                Severity = TraceEventType.Information,
                Title = title,
                Message = message,
            });

        public static void Error(string title, string message, Exception e)
            => LibLogger.Write(new LogEntry
            {
                Severity = TraceEventType.Error,
                Title = title,
                Message = $"{message}\r\n{e}",
            });
    }
}
