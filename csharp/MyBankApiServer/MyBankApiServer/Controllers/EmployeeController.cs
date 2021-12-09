using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApiServer.Managers;
using BankApiServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyBankApiServer.Controllers
{
    [ApiController]
    [Route("api/employee")]

    public class EmployeeController : Controller
    {

        [HttpGet]
        public List<Employee> GetAllEmployee()
        {
            return employeeManager.GetEmployee();
        }

        [HttpGet("{id}")]
        public Employee GetEmployee(string id)
        {
            Employee employee = employeeManager.GetEmployee(id);
            return employee;
        }

        [HttpPut("{id}/{pwd}")]
        public int UpdatePwd(string id,string pwd)
        {
            return employeeManager.UpdatePwd(id,pwd);
        }

    }
}