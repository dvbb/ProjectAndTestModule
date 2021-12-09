using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBankApiServer.Managers;
using MyBankApiServer.Models;
using Newtonsoft.Json.Linq;

namespace MyBankApiServer.Controllers
{
    [ApiController]
    [Route("api/amount")]

    public class AmountChangeController : Controller
    {
        [HttpGet("{id}")]
        public List<AmountChange> GetAmountChanges(string id)
        {
            return amountChangeManager.GetAmountChange(id);
        }

        [HttpGet("pid/{pid}")]
        public AmountChange GetAmountChangeByPid(string pid)
        {
            return amountChangeManager.GetAmountChangeByPid(pid);
        }

        [HttpPost]
        public int InsertAmountChange([FromBody] AmountChange ac)
        {
            return amountChangeManager.InsertAmountChange(ac.dId, ac.dName, ac.addr, ac.dType, ac.rate, ac.deposit);
        }

        [HttpPut("{id}")]
        public int UpdateAmountChange(string id)
        {
            return amountChangeManager.UpdateAmountChange(id);
        }
    }
}