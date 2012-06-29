
using Store.WebUI.Infrastructure;
using NLog;

public class NLogLogger : ILogger
{

    private Logger _logger;

    public NLogLogger()
    {
        _logger = LogManager.GetCurrentClassLogger();
    }

    public void Info(string message)
    {
        _logger.Info(message);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

}