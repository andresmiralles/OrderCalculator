using System;

namespace DevTest.OrderCalculator.Core.Helpers
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:O}{message}");
        }

        public void Warning(string message)
        {
            Log($"Warning: {message}");
        }
        public void Error(string message)
        {
            Log($"ERROR: {message}");
        }
    }
}
