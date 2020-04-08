using System.Collections.Generic;
using WebStore.Domain.Entities;

namespace WebStore.Interfaces.Services
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetAll();

        Customer GetById(int id);

        void Add(Customer customer);

        void Edit(int id, Customer customer);

        bool Delete(int id);

        void SaveChanges();

    }
}
