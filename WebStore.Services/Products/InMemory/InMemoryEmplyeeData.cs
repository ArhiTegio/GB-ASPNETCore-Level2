using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryEmplyeeData : IEmployeesData
    {
        //класс-репозиторий напрямую обращается к контексту базы данных
        //private readonly AppDbContext context;
        //public InMemoryEmplyeeData(AppDbContext context)
        //{
        //    this.context = context;
        //}
        public IEnumerable<Employee> GetAll() => TestData.Employees;

        public Employee GetById(int id) => TestData.Employees.FirstOrDefault(x => x.Id == id);

        public void Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentException(nameof(Employee));
            if (TestData.Employees.Contains(employee)) return;
            employee.Id = TestData.Employees.Count() == 0 ? 1 : TestData.Employees.Max(e => e.Id) + 1;
            TestData.Employees.Add(employee);
        }

        public void Edit(int id, Employee employee)
        {
            if(employee is null)
                throw new ArgumentException(nameof(Employee));

            if (TestData.Employees.Contains(employee)) return;

            var db_employee = GetById(id);
            if (db_employee is null)
                return;

            db_employee.SurName = employee.SurName;
            db_employee.FirstName = employee.FirstName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
            db_employee.BirthDay = employee.BirthDay;
            db_employee.Telephone = employee.Telephone;
        }

        public bool Delete(int id)
        {

            var db_employee = GetById(id);
            if (db_employee is null)
                return false;

            TestData.Employees.Remove(db_employee);
            return true;
        }

        public void SaveChanges()
        {
            //TestData.SaveChanges();
        }
    }
}
