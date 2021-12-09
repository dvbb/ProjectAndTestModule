using BankApiServer.Managers;
using BankApiServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankApiServer.Controllers
{
    public class DepositorController : Controller
    {
        [Route("api/depositor")]

        public Depositor GetDepositor(string id)
        {
            return depositorManager.GetDepositor(id);
        }
    }
}