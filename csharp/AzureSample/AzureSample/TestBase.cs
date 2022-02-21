using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace AzureSample
{
    public class TestBase
    {
        public string clientId  => Environment.GetEnvironmentVariable("CLIENT_ID");
        public string clientSecret => Environment.GetEnvironmentVariable("CLIENT_SECRET");
        public string tenantId => Environment.GetEnvironmentVariable("TENANT_ID");
        public string subscription  => Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");

        public TestBase()
        {
            CheckEffective(clientId);
            CheckEffective(clientSecret);
            CheckEffective(tenantId);
            CheckEffective(subscription);
        }

        private void CheckEffective(string value)
        {
            if (value == null || string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException($"argument cannot be null.please make sure all enviroment parameters are correct.");
            }
        }
    }
}
