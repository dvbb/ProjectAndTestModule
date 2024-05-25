using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApiServer.Models;
using Microsoft.AspNetCore.Mvc;
using MyBankApiServerV2.Repositories;

namespace MyBankApiServer.Controllers
{
    [ApiController]
    [Route("api/employee")]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [Route("Delete")]
        public int Delete(int id)
        {
            return _employeeRepository.Delete(id);
        }

        [HttpPost]
        [Route("Update")]
        public int Update([FromBody] Employee employee)
        {
            return _employeeRepository.Update(employee);
        }

        [HttpPost]
        [Route("Add")]
        public int Add([FromBody] Employee employee)
        {
            return _employeeRepository.Add(employee);
        }

        [HttpGet]
        [Route("Get")]
        public Employee Get(int id)
        {
            return _employeeRepository.Get(id);
        }

        [HttpPost]
        [Route("GetAll")]
        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

    }
}