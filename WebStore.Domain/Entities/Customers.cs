using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities
{
    /// <summary> Пользователи </summary>
    //[Table("Customers")]
    public class Customer
    {
        public Customer(int id, string firstName, string surName, string patronymic, int age, string telephone, DateTime birthDay, string login, string password)
        {
            Id = id;
            FirstName = firstName;
            SurName = surName;
            Patronymic = patronymic;
            Age = age;
            Telephone = telephone;
            BirthDay = birthDay;
            Login = login;
            Password = password;
        }

        public Customer()
        {
            
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public string Telephone { get; set; }
        public DateTime BirthDay { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
