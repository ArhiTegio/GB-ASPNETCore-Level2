using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly WebStoreDB _db;
        public SqlCustomerData(WebStoreDB db) => _db = db;
        public IEnumerable<Customer> GetAll() => _db.Customers.AsEnumerable();
        public Customer GetById(int id) => _db.Customers.FirstOrDefaultAsync(x => x.Id == id).Result;

        public void Add(Customer customer)
        {
            if (customer is null)
                throw new ArgumentException(nameof(Employee));
            if (_db.Customers.ContainsAsync(customer).Result) return;
            customer.Id = _db.Customers.CountAsync().Result == 0 ? 1 : _db.Customers.MaxAsync(e => e.Id).Result + 1;
            _db.Customers.Add(customer);
        }

        public void Edit(int id, Customer customer)
        {
            if (customer is null)
                throw new ArgumentException(nameof(Employee));

            if (_db.Customers.Contains(customer)) return;

            var db_customer = GetById(id);
            if (db_customer is null)
                return;

            db_customer.SurName = customer.SurName;
            db_customer.FirstName = customer.FirstName;
            db_customer.Patronymic = customer.Patronymic;
            db_customer.Age = customer.Age;
            db_customer.BirthDay = customer.BirthDay;
            db_customer.Telephone = customer.Telephone;
        }

        public bool Delete(int id)
        {
            var db_customer = GetById(id);
            if (db_customer is null)
                return false;

            _db.Customers.Remove(db_customer);
            return true;
        }

        public void SaveChanges()
        {
            _db.SaveChangesAsync();
        }
    }
}
