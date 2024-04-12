using Task.Integration.Data.Models;

namespace Task.Connector
{
    public class NullLogger : ILogger
    {
        public void Debug(string message) {}

        public void Error(string message) {}

        public void Warn(string message) { }
    }
}