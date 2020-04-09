using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmploeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmploeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _employeesData.GetAll();
        }

        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return _employeesData.GetById(id);
        }

        [HttpPost]
        public void Add([FromBody] Employee employee)
        {
            _employeesData.Add(employee);
        }

        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee employee)
        {
            _employeesData.Edit(id, employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _employeesData.Delete(id);
        }

        public void SaveChanges()
        {
            _employeesData.SaveChanges();
        }
    }
}