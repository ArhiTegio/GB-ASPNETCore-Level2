using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace WebStore.Logger
{
    public sealed class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string nameCategory, XmlElement configuration)
        {
            var logger_repository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), 
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(logger_repository.Name, nameCategory);
            log4net.Config.XmlConfigurator.Configure(logger_repository, configuration);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if(formatter is null)
                throw new ArgumentException(nameof(formatter));

            if (!IsEnabled(logLevel)) 
                return;

            var log_massage = formatter(state, exception);

            if (string.IsNullOrEmpty(log_massage) && exception is null)
                return;


            switch (logLevel)
            {
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(log_massage);
                    break;

                case LogLevel.Information:
                    _log.Info(log_massage);
                    break;

                case LogLevel.Warning:
                    _log.Warn(log_massage);
                    break;

                case LogLevel.Error:
                    _log.Error(log_massage ?? exception.ToString());
                    break;

                case LogLevel.Critical:
                    _log.Fatal(log_massage ?? exception.ToString());
                    break;

                case LogLevel.None:
                    break;
            }
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(level), level, null);

                case LogLevel.Trace: 
                case LogLevel.Debug: 
                    return _log.IsDebugEnabled;

                case LogLevel.Information:
                    return _log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _log.IsWarnEnabled;

                case LogLevel.Error:
                    return _log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _log.IsFatalEnabled;

                case LogLevel.None:
                    return false;
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;
    }
}