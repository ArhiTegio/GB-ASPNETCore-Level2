using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    //[Route("users")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ICustomerData _CustomersData;
        public EmployeesController(ICustomerData employeesData) => _CustomersData = employeesData;


        //[Routing("employees")]
        public IActionResult Index() => View(_CustomersData.GetAll().Select(x => new EmployeeViewModel()
        {
            Id = x.Id,
            Age = x.Age,
            Telephone = x.Telephone,
            SecondName = x.SurName,
            Name = x.FirstName,
            BirthDay = x.BirthDay, 
            Patronymic = x.Patronymic,
        }));

        [HttpGet]
        public IActionResult Details(Customer _employee)
        {

            var employee = _CustomersData.GetById(_employee.Id);

            if (employee is null)
                return NotFound();
            return View(new EmployeeViewModel()
            {
                Id = employee.Id,
                Age = employee.Age,
                Telephone = employee.Telephone,
                SecondName = employee.SurName,
                Name = employee.FirstName,
                BirthDay = employee.BirthDay,
                Patronymic = employee.Patronymic,
            });
        }

        //[Route("employees/{Id}")]
        //[HttpPost]
        //public IActionResult Details(Employee customer)
        //{
        //    if (surName is null && firstName is null && patronymic is null && age is null && telephone is null &&
        //        birthDay is null && id != null && int.TryParse(id, out var _Id))
        //    {
        //        TestData.dEmployees.Remove(_Id);
        //        return View("Index", TestData.dEmployees);
        //    }

        //    if (int.TryParse(id, out var Id))
        //    {
        //        if (TestData.dEmployees.TryGetValue(Id, out var customer))
        //        {
        //            if (int.TryParse(age, out var age_cheaked))
        //                customer.Age = age_cheaked;
        //            customer.SurName = surName;
        //            customer.FirstName = firstName;
        //            customer.Patronymic = patronymic;
        //            customer.BirthDay = birthDay;
        //            customer.Telephone = telephone;
        //        }

        //        return TestData.dEmployees.TryGetValue(Id, out var employee_)
        //            ? View("Index", TestData.dEmployees)
        //            : (IActionResult)NotFound();
        //    }
        //    else
        //        return (IActionResult)NotFound();
        //}
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel customer)
        {
            if (customer is null)
                throw new ArgumentNullException(nameof(EmployeeViewModel));

            if (!ModelState.IsValid)
                return View(customer);
            _CustomersData.Add(new Customer()
            {
                Id = customer.Id,
                Age = customer.Age,
                Telephone = customer.Telephone,
                SurName = customer.SecondName,
                FirstName = customer.Name,
                BirthDay = customer.BirthDay,
                Patronymic = customer.Patronymic,
                Login = customer.Login,
                Password = customer.Password,
            });
            _CustomersData.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int? id)
        {
            if (id is null) return View(new EmployeeViewModel());

            if (id == 0) return BadRequest();

            var customer = _CustomersData.GetById(id.Value);

            if (customer is null)
                return NotFound();

            return View(new EmployeeViewModel()
            {
                Id = customer.Id,
                Age = customer.Age,
                Telephone = customer.Telephone,
                SecondName = customer.SurName,
                Name = customer.FirstName,
                BirthDay = customer.BirthDay,
                Patronymic = customer.Patronymic,
                Login = customer.Login,
                Password = customer.Password,
            });
        }

        [Authorize(Roles = Role.Administrator)]
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel custumer)
        {
            if (custumer is null)
                throw new ArgumentNullException(nameof(EmployeeViewModel));

            if(int.TryParse(custumer.Name, out var name) || int.TryParse(custumer.SecondName, out name) || int.TryParse(custumer.Patronymic, out name))
                ModelState.AddModelError(string.Empty, "Число не может быть фамилией, именем или отчеством!");

            if (!ModelState.IsValid)
                return View(custumer);

            var id = custumer.Id;
            if(id > 0)
                _CustomersData.Add(new Customer()
                {
                    Id = custumer.Id,
                    Age = custumer.Age,
                    Telephone = custumer.Telephone,
                    SurName = custumer.SecondName,
                    FirstName = custumer.Name,
                    BirthDay = custumer.BirthDay,
                    Patronymic = custumer.Patronymic,
                    Login = custumer.Login,
                    Password = custumer.Password,

                });
            else
                _CustomersData.Edit(id, new Customer()
                {
                    Id = custumer.Id,
                    Age = custumer.Age,
                    Telephone = custumer.Telephone,
                    SurName = custumer.SecondName,
                    FirstName = custumer.Name,
                    BirthDay = custumer.BirthDay,
                    Patronymic = custumer.Patronymic,
                    Login = custumer.Login,
                    Password = custumer.Password,
                });

            _CustomersData.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();
            var employee = _CustomersData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel()
            {
                Id = employee.Id,
                Age = employee.Age,
                Telephone = employee.Telephone,
                SecondName = employee.SurName,
                Name = employee.FirstName,
                BirthDay = employee.BirthDay,
                Patronymic = employee.Patronymic,
            });
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeteleConfirned(int id)
        {
            _CustomersData.Delete(id);
            _CustomersData.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}