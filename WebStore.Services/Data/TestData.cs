using System;
using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain.Models;
using static AllRussianName.Russian;

namespace WebStore.Services.Data
{
    public class TestData
    {
        private static Dictionary<int, Employee> _dEmployees = new Dictionary<int, Employee>();
        private static HashSet<Employee> _hashSetEmployees = new HashSet<Employee>();
        public static Random FactoryRandom { get; } = new Random();
        public static string[] AllName { get; } = GetManNames();
        public static string[] AllSurname { get; } = GetManSurname();

        public static Dictionary<int, Employee> dEmployees
        {
            get => _dEmployees;
            set => _dEmployees = value;
        }

        public static HashSet<Employee> HashSetEmployees

        {
            get => _hashSetEmployees;
            set => _hashSetEmployees = value;
        }

        private static List<Employee> employees = new List<Employee>();
        public static List<Employee> Employees
        {
            get
            {
                if (employees.Count == 0)
                {
                    var year = DateTime.Now.Year;
                    var coutEmploee = FactoryRandom.Next(30, 100);
                    for (int i = 1; i < coutEmploee; i++)
                    {
                        var age = FactoryRandom.Next(18, 75); 
                        employees.Add(new Employee(i, AllName[FactoryRandom.Next(0, AllName.Length - 1)],
                            AllSurname[FactoryRandom.Next(0, AllSurname.Length - 1)],
                            $"{AllName[FactoryRandom.Next(0, AllName.Length - 1)]}ович",
                            age,
                            $"+79{String.Format("{0:D2}", FactoryRandom.Next(80, 100))}-{String.Format("{0:D3}", FactoryRandom.Next(0, 1000))}-{String.Format("{0:D4}", FactoryRandom.Next(0, 10000))}",
                            new DateTime(DateTime.Now.Year - age, FactoryRandom.Next(1, 12), FactoryRandom.Next(1, 31))));
                    }
                }

                return employees;
            }

        }


        private static List<Customer> customer = new List<Customer>();

        public static List<Customer> Customers
        {
            get
            {
                if (customer.Count == 0)
                    customer = GenerationCustomers();
                return customer;
            }
        }

        private static List<Customer> GenerationCustomers()
        {
            var customer = new List<Customer>();
                var year = DateTime.Now.Year;
                var coutCustomer = FactoryRandom.Next(30, 100);
                for (int i = 1; i < coutCustomer; i++)
                {
                    var age = FactoryRandom.Next(18, 75);
                    var name = AllName[FactoryRandom.Next(0, AllName.Length - 1)];
                    customer.Add(new Customer(i, name,
                        AllSurname[FactoryRandom.Next(0, AllSurname.Length - 1)],
                        $"{AllName[FactoryRandom.Next(0, AllName.Length - 1)]}ович",
                        age,
                        $"+79{String.Format("{0:D2}", FactoryRandom.Next(80, 100))}-{String.Format("{0:D3}", FactoryRandom.Next(0, 1000))}-{String.Format("{0:D4}", FactoryRandom.Next(0, 10000))}",
                        new DateTime(DateTime.Now.Year - age, FactoryRandom.Next(1, 12), FactoryRandom.Next(1, 31)), name, FactoryRandom.Next(1000,100000).ToString()));
                }
            

            return customer;
        }

        public TestData()
        {

        }

        public static IEnumerable<Brand> Brands { get; } = new[]
{
            new Brand { Id = 1, Name = "Acne", Order = 0 },
            new Brand { Id = 2, Name = "Grune Erde", Order = 1 },
            new Brand { Id = 3, Name = "Albiro", Order = 2 },
            new Brand { Id = 4, Name = "Ronhill", Order = 3 },
            new Brand { Id = 5, Name = "Oddmolly", Order = 4 },
            new Brand { Id = 6, Name = "Boudestijn", Order = 5 },
            new Brand { Id = 7, Name = "Rosch creative culture", Order = 6 },
        };

        public static IEnumerable<Section> Sections { get; } = new[]
       {
              new Section { Id = 1, Name = "Спорт", Order = 0 },
              new Section { Id = 2, Name = "Nike", Order = 0, ParentId = 1 },
              new Section { Id = 3, Name = "Under Armour", Order = 1, ParentId = 1 },
              new Section { Id = 4, Name = "Adidas", Order = 2, ParentId = 1 },
              new Section { Id = 5, Name = "Puma", Order = 3, ParentId = 1 },
              new Section { Id = 6, Name = "ASICS", Order = 4, ParentId = 1 },
              new Section { Id = 7, Name = "Для мужчин", Order = 1 },
              new Section { Id = 8, Name = "Fendi", Order = 0, ParentId = 7 },
              new Section { Id = 9, Name = "Guess", Order = 1, ParentId = 7 },
              new Section { Id = 10, Name = "Valentino", Order = 2, ParentId = 7 },
              new Section { Id = 11, Name = "Диор", Order = 3, ParentId = 7 },
              new Section { Id = 12, Name = "Версачи", Order = 4, ParentId = 7 },
              new Section { Id = 13, Name = "Армани", Order = 5, ParentId = 7 },
              new Section { Id = 14, Name = "Prada", Order = 6, ParentId = 7 },
              new Section { Id = 15, Name = "Дольче и Габбана", Order = 7, ParentId = 7 },
              new Section { Id = 16, Name = "Шанель", Order = 8, ParentId = 7 },
              new Section { Id = 17, Name = "Гуччи", Order = 9, ParentId = 7 },
              new Section { Id = 18, Name = "Для женщин", Order = 2 },
              new Section { Id = 19, Name = "Fendi", Order = 0, ParentId = 18 },
              new Section { Id = 20, Name = "Guess", Order = 1, ParentId = 18 },
              new Section { Id = 21, Name = "Valentino", Order = 2, ParentId = 18 },
              new Section { Id = 22, Name = "Dior", Order = 3, ParentId = 18 },
              new Section { Id = 23, Name = "Versace", Order = 4, ParentId = 18 },
              new Section { Id = 24, Name = "Для детей", Order = 3 },
              new Section { Id = 25, Name = "Мода", Order = 4 },
              new Section { Id = 26, Name = "Для дома", Order = 5 },
              new Section { Id = 27, Name = "Интерьер", Order = 6 },
              new Section { Id = 28, Name = "Одежда", Order = 7 },
              new Section { Id = 29, Name = "Сумки", Order = 8 },
              new Section { Id = 30, Name = "Обувь", Order = 9 },
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product { Id = 1, Name = "Белое платье", Price = 1025, ImageUrl = "product1.jpg", Order = 0, SectionId = 2, BrandId = 1 },
            new Product { Id = 2, Name = "Розовое платье", Price = 1025, ImageUrl = "product2.jpg", Order = 1, SectionId = 2, BrandId = 1 },
            new Product { Id = 3, Name = "Красное платье", Price = 1025, ImageUrl = "product3.jpg", Order = 2, SectionId = 2, BrandId = 1 },
            new Product { Id = 4, Name = "Джинсы", Price = 1025, ImageUrl = "product4.jpg", Order = 3, SectionId = 2, BrandId = 1 },
            new Product { Id = 5, Name = "Лёгкая майка", Price = 1025, ImageUrl = "product5.jpg", Order = 4, SectionId = 2, BrandId = 2 },
            new Product { Id = 6, Name = "Лёгкое голубое поло", Price = 1025, ImageUrl = "product6.jpg", Order = 5, SectionId = 2, BrandId = 1 },
            new Product { Id = 7, Name = "Платье белое", Price = 1025, ImageUrl = "product7.jpg", Order = 6, SectionId = 2, BrandId = 1 },
            new Product { Id = 8, Name = "Костюм кролика", Price = 1025, ImageUrl = "product8.jpg", Order = 7, SectionId = 25, BrandId = 1 },
            new Product { Id = 9, Name = "Красное китайское платье", Price = 1025, ImageUrl = "product9.jpg", Order = 8, SectionId = 25, BrandId = 1 },
            new Product { Id = 10, Name = "Женские джинсы", Price = 1025, ImageUrl = "product10.jpg", Order = 9, SectionId = 25, BrandId = 3 },
            new Product { Id = 11, Name = "Джинсы женские", Price = 1025, ImageUrl = "product11.jpg", Order = 10, SectionId = 25, BrandId = 3 },
            new Product { Id = 12, Name = "Летний костюм", Price = 1025, ImageUrl = "product12.jpg", Order = 11, SectionId = 25, BrandId = 3 },
        };
    }
}
