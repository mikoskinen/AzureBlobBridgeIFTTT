using Nancy;
using Nancy.Responses;

namespace AzureBlobBridgeIFTTT
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = (parameters) => new TextResponse("Good to go!");
        }
    }
}