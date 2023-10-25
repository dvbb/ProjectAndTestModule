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
            resourceGroup.Delete(WaitUntil.Started);
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
            var response = await genericResourceCollection.CreateOrUpdateAsync(WaitUntil.Completed, userIdentityId, input);
            await Console.Out.WriteLineAsync(response.Value.Data.Name);
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
