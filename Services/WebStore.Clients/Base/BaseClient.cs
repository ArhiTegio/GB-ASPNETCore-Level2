using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebStore.Clients.Base
{
    public abstract class BaseClient : IDisposable
    {
        protected readonly string _serviceAddress;
        protected readonly HttpClient _client;
        protected BaseClient(IConfiguration configuration, string ServiceAddress)
        {
            _serviceAddress = ServiceAddress;
            _client = new HttpClient
            {
                BaseAddress = new Uri(configuration["WebApiURL"])
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || disposing) return;
            _disposed = true;
            _client.Dispose();
        }
    }
}
