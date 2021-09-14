namespace DevTest.OrderCalculator.Core.Helpers
{
    public interface ILogger
    {
        void Log(string message);
        void Warning(string message);
        void Error(string message);
    }
}
