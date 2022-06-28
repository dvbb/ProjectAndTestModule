using AzureSample;
using Microsoft.Azure;
using Microsoft.Azure.Management.Automation;
using Microsoft.Azure.Management.Automation.Models;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AutomationSample
{
    public class AutomationTests : TestBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Ignore("package version:3.8.3-preview. passed.")]
        public async Task AutomationTest()
        {
            ServiceClientCredentials credentials = await GetDefaultCredentialAsync();
            //AutomationClient client = new AutomationClient(credentials);
            //client.SubscriptionId = subscription;

            //string rgName = "Auto-RG-0000";
            //string automationAccountName = "automation-0000";
            //string runbookName = "test";
            //var Runbook = await client.Runbook.GetAsync(rgName, automationAccountName, runbookName);
            //var RunbookDraft = await client.RunbookDraft.GetAsync(rgName, automationAccountName, runbookName);

            //FileStream stream = new FileStream(@"D:\script.txt", FileMode.Open);

            //await client.RunbookDraft.ReplaceContentAsync(rgName, automationAccountName, runbookName, stream);
        }

        [Test]
        //[Ignore("package version:2.0.7.")]
        public async Task UpdateRunbookDraft()
        {
            string token = await GetToken();
            TokenCloudCredentials subscriptionCloudCredentials = new TokenCloudCredentials(subscription,token);
            AutomationManagementClient client = new AutomationManagementClient(subscriptionCloudCredentials);

            string rgName = "Auto-RG-0000";
            string automationAccountName = "automation-0000";
            var data = new RunbookCreateOrUpdateDraftParameters() 
            { 
                Name = "runbook-0000",
                Location = "eastus",
                Properties = new RunbookCreateOrUpdateDraftProperties()
                {
                    RunbookType = "PowerShell7",
                    Draft = new RunbookDraft(),
                }
            };
            var response = await client.Runbooks.CreateOrUpdateWithDraftAsync(rgName, automationAccountName, data);

            // System.ArgumentNullException : Value cannot be null. (Parameter 'operationStatusLink')
            RunbookDraftUpdateParameters draftData = new RunbookDraftUpdateParameters() {
                Name = "runbook-0000",
                Stream = "write-host \"hello world\"",
            };
            var draftResponse = await client.RunbookDraft.UpdateGraphAsync(rgName, automationAccountName, draftData);
        }
    }
}