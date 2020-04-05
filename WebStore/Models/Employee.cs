using System;

namespace WebStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }

        public string Telephone { get; set; }
        public DateTime BirthDay { get; set; }

        public Employee() { }

        public  Employee(int id, string firstName, string surName, string patronymic, int age, string telephone, DateTime birthDay)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.SurName = surName;
            this.Patronymic = patronymic;
            this.Age = age;
            this.Telephone = telephone;
            this.BirthDay = birthDay;
        }
    }
}
