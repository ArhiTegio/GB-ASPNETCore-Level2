using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>Контроллер управления сотрудниками</summary>
    //[Route("api/[controller]")]
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmploeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _employeesData;

        public EmploeesApiController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }


        /// <summary>Получить всех сотрудников</summary>
        /// <returns>Список сотрудников магазина</returns>
        [HttpGet]
        public IEnumerable<Employee> GetAll()
        {
            return _employeesData.GetAll();
        }

        /// <summary> Получить сотрудников </summary>
        /// <param name="id">Идентификатор запрашиваемого сотрудника</param>
        /// <returns>Сотрудник с указанным идентификатором</returns>
        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return _employeesData.GetById(id);
        }

        /// <summary>Добавить сотрудника</summary>
        /// <param name="employee">Новый сотрудник магазина</param>
        [HttpPost]
        public void Add([FromBody] Employee employee)
        {
            _employeesData.Add(employee);
        }

        /// <summary>Редактирование сотрудника</summary>
        /// <param name="id">Идентификатор редактируемого сотрудника</param>
        /// <param name="employee">Информация, ыносимая в БД о сотруднике</param>
        [HttpPut("{id}")]
        public void Edit(int id, [FromBody] Employee employee)
        {
            _employeesData.Edit(id, employee);
        }

        /// <summary>Удалить сотрудника</summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>Истина, если сотрудник успешно удалён</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _employeesData.Delete(id);
        }

        [NonAction]
        public void SaveChanges()
        {
            _employeesData.SaveChanges();
        }
    }
}