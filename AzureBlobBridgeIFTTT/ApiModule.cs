using System;
using System.Configuration;
using System.Web.ModelBinding;
using Microsoft.WindowsAzure.Storage.Blob;
using Nancy;
using Nancy.Responses;

namespace AzureBlobBridgeIFTTT
{
    public class ApiModule : NancyModule
    {
        public ApiModule(CloudBlobContainer blobContainer) : base("/api")
        {
            Put[ConfigurationManager.AppSettings["apiKey"], true] = async (ct, parameters) =>
            {
                var contentType = Request.Headers.ContentType;
                if (!string.IsNullOrWhiteSpace(contentType))
                {
                    contentType = "Application/json";
                }

                var fileName = DateTime.UtcNow.Ticks + ".dat";

                var blob = blobContainer.GetBlockBlobReference(fileName);

                await blob.UploadFromStreamAsync(Request.Body);
                await blob.FetchAttributesAsync();

                blob.Properties.CacheControl = $"public, max-age={TimeSpan.FromMinutes(10).TotalSeconds}, must-revalidate";
                blob.Properties.ContentType = contentType;

                await blob.SetPropertiesAsync();

                return new TextResponse(HttpStatusCode.Created);
            };
        }
    }
}