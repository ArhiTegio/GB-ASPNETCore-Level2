using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration, WebAPI.Employees)
        {
            
        }

        public IEnumerable<Employee> GetAll() => Get<List<Employee>>(_serviceAddress);

        public Employee GetById(int id) => Get<Employee>($"{_serviceAddress}/{id}");

        public void Add(Employee employee) => Post(_serviceAddress, employee);

        public void Edit(int id, Employee employee) => Put($"{_serviceAddress}/{id}", employee);

        public bool Delete(int id) => Delete($"{_serviceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges() { }
    }
}
