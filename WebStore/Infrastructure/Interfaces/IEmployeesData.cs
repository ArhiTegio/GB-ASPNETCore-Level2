using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IEmployeesData
    {
        IEnumerable<Employee> GetAll();

        Employee GetById(int id);

        void Add(Employee employee);

        void Edit(int id, Employee employee);

        bool Delete(int id);

        void SaveChanges();
    }
}