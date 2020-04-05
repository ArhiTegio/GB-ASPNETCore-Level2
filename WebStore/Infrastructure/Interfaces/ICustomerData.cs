using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
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
