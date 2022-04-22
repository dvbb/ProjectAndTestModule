using Azure.Core;
using Azure.Identity;
using AzureSample;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Rest;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Track1
{
    internal class DataFactoryTests : TestBase
    {
        [Test]
        public async Task AdfTest()
        {

            // Get AccessToken with Azure.Identity
            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            string[] scopes = { "https://management.core.windows.net/.default" };
            TokenRequestContext tokenRequestContext = new TokenRequestContext(scopes, "");
            var response = await clientSecretCredential.GetTokenAsync(tokenRequestContext);
            string accessToken = response.Token;

            // Get a existing an ADF pipeline
            TokenCredentials bauthCredentials = new TokenCredentials(accessToken);
            ServiceClientCredentials credentials = bauthCredentials;
            string rgName = "ADF-RG-0000";
            string factoryName = "adf-0000";
            string pipelineName = "CopyPipeline";
            var client = new DataFactoryManagementClient(credentials)
            {
                SubscriptionId = subscription
            };
            var pipeline = await client.Pipelines.GetAsync(rgName, factoryName, pipelineName);
            Console.WriteLine(pipeline.Name);
            Console.WriteLine(pipeline.Id);

            #region tutorial code      
            //string storageLinkedServiceName = "AzureStorageLinkedService";
            //string storageAccount = "saforadf220420";
            //string storageKey = "lTgE0VaQD/c2UAFH8s2QAgAMW0rF3ifGtuABd+OxoINbWnKI4gWAuOmY80rsvhGO1mK7Z3+rwCmK+ASth410oQ==";
            //string blobDatasetName = "testdataset1";
            // linked service

            // Create an Azure Storage linked service
            //LinkedServiceResource storageLinkedService = new LinkedServiceResource(
            //    new AzureStorageLinkedService
            //    {
            //        ConnectionString = new SecureString(
            //            "DefaultEndpointsProtocol=https;AccountName=" + storageAccount +
            //            ";AccountKey=" + storageKey)
            //    }
            //);
            //var link = await client.LinkedServices.CreateOrUpdateAsync(
            //    rgName, factoryName, storageLinkedServiceName, storageLinkedService);
            //var list = await client.LinkedServices.ListByFactoryAsync(rgName, factoryName);
            //var link = await client.LinkedServices.GetAsync(rgName, factoryName, storageLinkedServiceName);

            // Create an Azure Blob dataset
            //DatasetResource blobDataset = new DatasetResource(
            //    new AzureBlobDataset
            //    {
            //        LinkedServiceName = new LinkedServiceReference
            //        {
            //            ReferenceName = storageLinkedServiceName
            //        },
            //        FolderPath = new Expression { Value = "@{dataset().path}" },
            //        Parameters = new Dictionary<string, ParameterSpecification>
            //        {
            //            { "path", new ParameterSpecification { Type = ParameterType.String } }
            //        }
            //    }
            //);
            //client.Datasets.CreateOrUpdate(rgName, factoryName, blobDatasetName, blobDataset);

            //var dataset = await client.Datasets.ListByFactoryAsync(rgName, factoryName);

            // Create a pipeline with a copy activity
            //Console.WriteLine("Creating pipeline " + pipelineName + "...");
            //PipelineResource pipeline = new PipelineResource
            //{
            //    Parameters = new Dictionary<string, ParameterSpecification>
            //    {
            //        { "inputPath", new ParameterSpecification { Type = ParameterType.String } },
            //        { "outputPath", new ParameterSpecification { Type = ParameterType.String } }
            //    },
            //    Activities = new List<Activity>
            //    {
            //        new CopyActivity
            //        {
            //            Name = "CopyFromBlobToBlob",
            //            Inputs = new List<DatasetReference>
            //            {
            //                new DatasetReference()
            //                {
            //                    ReferenceName = blobDatasetName,
            //                    Parameters = new Dictionary<string, object>
            //                    {
            //                        { "path", "@pipeline().parameters.inputPath" }
            //                    }
            //                }
            //            },
            //            Outputs = new List<DatasetReference>
            //            {
            //                new DatasetReference
            //                {
            //                    ReferenceName = blobDatasetName,
            //                    Parameters = new Dictionary<string, object>
            //                    {
            //                        { "path", "@pipeline().parameters.outputPath" }
            //                    }
            //                }
            //            },
            //            Source = new BlobSource { },
            //            Sink = new BlobSink { }
            //        }
            //    }
            //};
            //client.Pipelines.CreateOrUpdate(rgName, factoryName, pipelineName, pipeline);
            //var pipeline = await client.Pipelines.GetAsync(rgName, factoryName, pipelineName);
            #endregion
        }
    }
}
