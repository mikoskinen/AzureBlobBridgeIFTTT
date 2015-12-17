using System.Configuration;
using System.Runtime.InteropServices.ComTypes;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;

namespace AzureBlobBridgeIFTTT
{
    public class ConfigurationCheck : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            //pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            //{
            //    //var accessKey = ConfigurationManager.AppSettings["apiKey"];
            //    //var storageConnectionString = ConfigurationManager.AppSettings["storageConnectionString"];
            //    //var containerConfig = ConfigurationManager.AppSettings["container"];

            //    //var missingConfiguration = string.IsNullOrWhiteSpace(accessKey) || string.IsNullOrWhiteSpace(storageConnectionString) || string.IsNullOrWhiteSpace(containerConfig);

            //    //if (missingConfiguration)
            //    //    return new TextResponse(HttpStatusCode.PreconditionFailed, "Missing configuration. Make sure to define the following appSettings: apiKey, storageConnectionString, container.");

            //    //return null;
            //});

            //pipelines.OnError += (ctx, ex) => {
            //    return null;
            //};
        }
    }
}