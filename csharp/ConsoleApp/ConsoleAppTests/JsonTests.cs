using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppTests
{
    internal class JsonTests
    {
        [Test]
        public void Serialize()
        {
            var customers = Customer.Init();
            Console.WriteLine($"direct output List<T>:\n{customers}");

            var customersJson = JsonConvert.SerializeObject(customers);
            Console.WriteLine();
            Console.WriteLine($"Output Json object:\n{customersJson}");
        }

        [Test]
        public void Deserialize()
        {
            string jsonStr = "[{\"customer_name\":\"Eric\",\"email\":\"eric@gmail.com\"},{\"customer_name\":\"Jack\",\"email\":\"jack@gmail.com\"},{\"customer_name\":\"Ford\",\"email\":\"ford@gmail.com\"}]";
            var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonStr);

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Name} - {customer.Email}");
            }
        }
    }

    [Serializable]
    internal class Customer
    {
        [JsonProperty("customer_name")]
        public string Name { get; set; }
        [JsonProperty("email")] 
        public string Email { get; set; }

        public static List<Customer> Init()
        {
            return new List<Customer>
            {
                new Customer()
                {
                    Name ="Eric",
                    Email = "eric@gmail.com"
                },
                new Customer()
                {
                    Name ="Jack",
                    Email = "jack@gmail.com"
                },
                new Customer()
                {
                    Name ="Ford",
                    Email = "ford@gmail.com"
                },
            };
        }
    }

}
