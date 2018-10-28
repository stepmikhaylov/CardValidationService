using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace CardValidationService.Logging
{
    public class DiagnosticsTraceListener : CustomTraceListener
    {
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            var logEntry = (LogEntry)data;
            WriteLine($"{logEntry.Severity} - {logEntry.Title}: {logEntry.Message}");
        }

        public override void Write(string message)
            => Trace.Write(message);

        public override void WriteLine(string message)
            => Trace.WriteLine(message);
    }
}
