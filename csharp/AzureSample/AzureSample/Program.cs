﻿using System;
using System.Threading.Tasks;

namespace AzureSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TestBase testbase = new TestBase();
            Console.WriteLine("Hello World!");

            var subscriptionCollection = testbase.Client.GetSubscriptions();
            var tagToLookFor = "TagKey-9823";

            var iterationCount = 0;

            while (true)
            {
                Console.WriteLine($"Iteration #{iterationCount}");

                var subscriptionResources = subscriptionCollection.GetAllAsync();  // stale

                await foreach (var subscriptionResource in subscriptionResources)
                {
                    if (subscriptionResource.Data.Tags.ContainsKey(tagToLookFor))
                    {
                        Console.WriteLine($">> {subscriptionResource.Data.DisplayName} ({tagToLookFor} tag): {subscriptionResource.Data.Tags[tagToLookFor]}");
                    }
                }

                Console.WriteLine();

                iterationCount++;
                await Task.Delay(10000);
            }
        }
    }
}
