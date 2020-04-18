using System;
using System.Collections.Concurrent;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public sealed class Log4NetProvider : ILoggerProvider
    {
        private readonly string _configurationFile;

        private readonly ConcurrentDictionary<string, Log4NetLogger> _loggers = new ConcurrentDictionary<string, Log4NetLogger>();

        public void Dispose() => _loggers.Clear();

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, x =>
            {
                var xml = new XmlDocument();
                xml.Load(_configurationFile);
                return new Log4NetLogger(categoryName, xml["log4net"]);
            });
        }

        public Log4NetProvider(string configurationFile) => _configurationFile = configurationFile;
    }
}