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

        [HttpPost]
        public int  Add([FromBody] Employee employee)
        {
            return _employeeRepository.Add(employee);
        }



    }
}