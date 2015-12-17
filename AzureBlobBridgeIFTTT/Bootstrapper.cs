using System;
using System.Configuration;
using System.Runtime.Remoting.Messaging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Nancy.Responses;
using Nancy.TinyIoc;
using StructureMap;

namespace AzureBlobBridgeIFTTT
{
    public class Bootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(IContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var accessKey = ConfigurationManager.AppSettings["apiKey"];
            var storageConnectionString = ConfigurationManager.AppSettings["storageConnectionString"];
            var containerConfig = ConfigurationManager.AppSettings["container"];

            var missingConfiguration = string.IsNullOrWhiteSpace(accessKey) || string.IsNullOrWhiteSpace(storageConnectionString) || string.IsNullOrWhiteSpace(containerConfig);

            if (missingConfiguration)
            {
                SetError(container, "Missing configuration. Make sure to define the following appSettings: apiKey, storageConnectionString, container.");
                return;
            }

            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            CloudBlobClient blobClient;
            try
            {
                blobClient = storageAccount.CreateCloudBlobClient();
            }
            catch (Exception)
            {
                SetError(container, "Failed to create blob client. Make sure you have defined correct Azure storage connection string.");
                return;
            }

            try
            {
                var blobContainer = blobClient.GetContainerReference(containerConfig);
                blobContainer.CreateIfNotExists(BlobContainerPublicAccessType.Off);

                SetOk(container, blobContainer);
            }
            catch (Exception)
            {
                SetError(container, "Failed to initialize container. Make sure you have defined correct container.");
            }
        }

        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
            var error = container.GetInstance<ErrorProvider>();

            if (!error.InvalidConfiguration)
                return;

            pipelines.BeforeRequest += (ctx) => new TextResponse(HttpStatusCode.PreconditionFailed, error.Error);
        }

        private static void SetOk(IContainer container, CloudBlobContainer blobContainer)
        {
            container.Configure(
                x =>
                {
                    x.For<ErrorProvider>().Use(new ErrorProvider());
                    x.For<CloudBlobContainer>().Use(blobContainer);
                });
        }

        private static void SetError(IContainer container, string error)
        {
            container.Configure(
                x =>
                {
                    x.For<ErrorProvider>().Use(new ErrorProvider(error));
                    x.For<CloudBlobContainer>().Use<CloudBlobContainer>(s => null);
                });
        }
    }

    public class ErrorProvider
    {
        public string Error { get; private set; }

        public bool InvalidConfiguration => !string.IsNullOrWhiteSpace(Error);

        public ErrorProvider()
        {
        }

        public ErrorProvider(string error)
        {
            Error = error;
        }
    }
}