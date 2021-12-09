using BankApiServer.Managers;
using BankApiServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankApiServer.Controllers
{
    [ApiController]
    [Route("api/depositor")]

    public class DepositorController : Controller
    {
        [HttpGet]
        public List<Depositor> GetDepositorAll()
        {
            return depositorManager.GetDepositor();
        }



        [HttpGet("{id}")]
        public Depositor GetDepositor(string id)
        {
            return depositorManager.GetDepositor(id);
        }

        [HttpPost("{id}/{pwd}/{name}")]
        public int InsertDepositor(string id,string pwd,string name)
        {
            return depositorManager.InsertDepositor(id,pwd,name);
        }

        [HttpPut("{id}/{pwd}")]
        public int UpdatePwd(string id, string pwd)
        {
            return depositorManager.UpdatePwd(id, pwd);
        }

    }
}