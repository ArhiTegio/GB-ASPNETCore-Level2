using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebStore.Infrastructuse.Middlewere
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandling> _logger;

        public ErrorHandling(RequestDelegate next, ILogger<ErrorHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    //Логика обработки тех или иных необходимых решений

        //    var next_task = _next(context); // Запуск следующего элемента конвеера

        //    //Выполнение логики после запуска следующего элемента конвеера 

        //    await next_task; //Ожидание выполнения запущенного элемента конвеера
        //    //Выполнение логики завершения данного элемента конвеера  
        //}

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var next_task = _next(context);
            }
            catch (Exception e)
            {
                HandleException(context, e);
            }
        }

        private void HandleException(HttpContext context, Exception error)
        {
            _logger.LogError(error, $"Ошибка при обработки запроса {context.Request.Path}");
        }
    }
}
