using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.Fluent;

namespace AzureWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqlFluentController : ControllerBase
    {
        private IAzure _azure;
        public SqlFluentController(IAzure azure)
        {
            _azure = azure;
        }

        [HttpGet]
        public string GetServerState()
        {
            string resourceGroupName = "Sql-RG-0000";
            string serverName = "server-0000";
            var server = _azure.SqlServers.GetByResourceGroup(resourceGroupName, serverName);

            return $"{server.Name}: {server.State}";
        }
    }
}
