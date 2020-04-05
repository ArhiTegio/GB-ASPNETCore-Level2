using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Api;

namespace WebStore.Components
{
    public class WebApiTestController : Controller
    {
        private readonly IValuesService _values;

        public WebApiTestController(IValuesService values) => _values = values;

        public async Task<IActionResult> Index()
        {
            var values = await _values.GetAsync();
            return View(values);
        }
    }
}