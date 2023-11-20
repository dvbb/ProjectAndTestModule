using Azure.Core;
using Azure.ResourceManager.Resources;
using Azure;
using AzureHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Azure.Identity;
using Azure.ResourceManager;

namespace BasicServices.Tests
{
    internal class RbacTests
    {
        private ResourceGroupResource resourceGroup { get; set; }
        private string _rgName;

        [SetUp]
        public async Task SetUp()
        {
            _rgName = BaseClientExtension.CreateRandomName("VMRG");
            _rgName = "ComputeRG555";
            BaseClient baseClient = new BaseClient();
            resourceGroup = await baseClient.CreateResourceGroup(_rgName);
        }

        [TearDown]
        public async Task TearDown()
        {
            //resourceGroup.Delete(WaitUntil.Started);
        }

        [Test]
        /// Azure.ResourceManager.ManagedServiceIdentities
        /// GenericResourceCollection instead of [Azure.ResourceManager.ManagedServiceIdentities: UserAssignedIdentityCollection]
        public async Task Application()
        {
            BaseClient baseClient = new BaseClient();
            GenericResourceCollection genericResourceCollection = baseClient.DefaultTenant.GetGenericResources();

            string userAssignedIdentityName = BaseClientExtension.CreateRandomName("identity");
            ResourceIdentifier userIdentityId =
                new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.ManagedIdentity/userAssignedIdentities/{userAssignedIdentityName}");

            var input = new GenericResourceData(resourceGroup.Data.Location);
            input = new GenericResourceData("");
            var response = await genericResourceCollection.CreateOrUpdateAsync(WaitUntil.Completed, userIdentityId, input);
            await Console.Out.WriteLineAsync(response.Value.Data.Name);
        }

        /// <summary>
        /// Azure.ResourceManager.Authorization
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task RoleDefinition()
        {
            BaseClient baseClient = new BaseClient();
            GenericResourceCollection genericResourceCollection = baseClient.DefaultTenant.GetGenericResources();

            // Create RoleDefinition
            var input = new GenericResourceData("")
            {
                Properties = BinaryData.FromObjectAsJson(new Dictionary<string, object>()
                {
                    { "RoleName", "SDKTestRole" },
                    { "Description", "SDKTestDescription" },
                    { "Type", "CustomRole" },
                    { "AssignableScopes", new List<string>() { resourceGroup.Id } },
                    { "Permissions", new List<Dictionary<string, object>>()
                        {
                           new Dictionary<string, object>()
                           {
                               { "Actions", new List<string>(){ "Microsoft.Authorization/*/read" } }
                           }
                        }
                    },
                })
            };
            var name = "49b923e6-f458-4229-a980-c0e62fcea856";
            var id = $"{resourceGroup.Id}/providers/Microsoft.Authorization/roleDefinitions/{name}";
            var roleDefinitionId = new ResourceIdentifier(id);
            var response = await genericResourceCollection.CreateOrUpdateAsync(WaitUntil.Completed, roleDefinitionId, input);
        }

        [Test]
        public async Task TempTest()
        {
            //BaseClient baseClient = new BaseClient();
            //GenericResourceCollection genericResourceCollection = baseClient.DefaultTenant.GetGenericResources();

            // /{scope}/providers/Microsoft.Resources/deployments/{deploymentName}
            // Create deployments

            TokenCredential cred = new DefaultAzureCredential();
            ArmClient client = new ArmClient(cred);
            var tenants = await client.GetTenants().GetAllAsync().ToEnumerableAsync();
            GenericResourceCollection genericResourceCollection = tenants.First().GetGenericResources();

            var input = new GenericResourceData("") // Set location payload to ""
            {
                Properties = BinaryData.FromObjectAsJson(new Dictionary<string, object>()
                {
                    { "mode", "Incremental" },
                    { "templateLink", new Dictionary<string, object>()
                        {
                            {"uri","https://raw.githubusercontent.com/Azure/azure-quickstart-templates/master/quickstarts/microsoft.storage/storage-account-create/azuredeploy.json"},
                        }
                    },
                    { "parameters", new Dictionary<string, object>()
                        {
                            {"storageAccountType",new Dictionary<string, object>()
                                {
                                    { "value","Standard_GRS" }
                                }
                            },
                        }
                    }
                })
            };
            var name = "deployEx-test-0";
            var deploymentsId = new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Resources/deployments/{name}");
            var response = await genericResourceCollection.CreateOrUpdateAsync(WaitUntil.Completed, deploymentsId, input);
        }

        [Test]
        [Ignore("Need import Azure.ResourceManager.Authorization")]
        /// Azure.ResourceManager.Authorization
        public async Task AddContributorRole()
        {
            // Add a contributor role to resourceGroup1 for above UserAssignedIdentity
            // `b24988ac-6180-42a0-ab88-20f7382dd24c` is [contributor] role definition name

            //ResourceIdentifier scopeId = resourceGroup.Id;
            //ResourceIdentifier roleDefinitionId = new ResourceIdentifier($"{subscription.Id}/providers/Microsoft.Authorization/roleDefinitions/b24988ac-6180-42a0-ab88-20f7382dd24c");
            //ResourceIdentifier userAssignedIdentityPrincipalId = identity.Data.PrincipalId.Value;

            //string roleAssignmentName = Guid.NewGuid().ToString();
            //RoleAssignmentCreateOrUpdateContent content = new RoleAssignmentCreateOrUpdateContent(
            //    roleDefinitionId: roleDefinitionId,
            //    principalId: userAssignedIdentityPrincipalId)
            //{
            //    PrincipalType = RoleManagementPrincipalType.ServicePrincipal
            //};
            //var roleAssignmentLro = await client.GetRoleAssignments(scopeId).CreateOrUpdateAsync(WaitUntil.Completed, roleAssignmentName, content);
            //RoleAssignmentResource roleAssignment = roleAssignmentLro.Value;
            //await Console.Out.WriteLineAsync("Created contribute role in resourceGroup1:");
            //await Console.Out.WriteLineAsync("\tName: " + roleAssignment.Data.Name);
            //await Console.Out.WriteLineAsync("\tPrincipalType: " + roleAssignment.Data.PrincipalType);
            //await Console.Out.WriteLineAsync("\tScope: " + roleAssignment.Data.Scope);
            //await Console.Out.WriteLineAsync("\tPrincipalId: " + roleAssignment.Data.PrincipalId);
        }
    }
}
