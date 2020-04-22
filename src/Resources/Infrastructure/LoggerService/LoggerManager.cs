using System;
using NLog;

namespace Infrastructure.LoggerService
{
	public class LoggerManager : ILoggerManager
    {
        private static ILogger _logger = null;
        public LoggerManager()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }


        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
        public void LogError(Exception ex)
        {
            _logger.Error(ex);
        }
    }
}