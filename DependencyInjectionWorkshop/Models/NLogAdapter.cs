namespace DependencyInjectionWorkshop.Models;

public interface IMyLogger
{
    void Info(string message);
}

internal class NLogAdapter : IMyLogger
{
    public void Info(string message)
    {
        var logger = NLog.LogManager.GetCurrentClassLogger();
        logger.Info(message);
    }
}