# Azure Blob Bridge for IFTTT

![Azure Blob Bridge for IFTTT logo](https://raw.githubusercontent.com/mikoskinen/AzureBlobBridgeIFTTT/master/logo/logo_small.jpg)

Azure Blob Bridge for IFTTT provides an easy way to use Azure Blob storage as a IFTTT destination channel. 

[![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://azuredeploy.net/)

The project is intended to be used with the IFTTT's Maker-channel. Example:

Feedly (new post) -> Maker channel -> Azure Blob Bridge -> Azure Blob storage (new post stored as a blob) 

Azure Blob Bridge works by providing HTTP POST API for uploading blobs to your Azure Storage Account. 

## Configuration

You need to define three configuration settings, either through the Azure Deploy or through Web.config:

* apiKey = Api key is used to define the URL for your api. NOTE: Use only valid URI characters. Example: apiKey = "hello" -> Your API works through http://yourdomain.com/api/hello
* storageConnectionString = The Azure Storage connection string. Example: DefaultEndpointsProtocol=https;AccountName=mystoragewe;AccountKey=wjN544545444233rwsdsdddddkkkkkkk999999999923wu3zJXSoBJ4LAWBbtEYKqbwjIQ==
* container = The Azure container where files will be written. NOTE: Use only valid container character (so no upper cases or numbers). If container is missing, it will be automatically created as private. Example: mycontainer

## Usage with IFTTT

Coming soon.

## Usage with HTTP POST
<pre><code>
POST http://myapp.domain.com/api/apikey HTTP/1.1
Content-Length: 38
Content-type: Application/json

{"author":"hello", "text":"mycontent"}
</pre></code>

This will create a new blob with provided content (body). Content-type is set if provided in request headers. File name is automatically generated: 
<pre><code>
DateTime.UtcNow.Ticks + ".dat";
</pre></code>

## Background

Unfortunately IFTTT doesn't currently provide channel for Azure Blob storage. This project can be used to work around the limitation.

## Contact

If you have any questions, you can contact [Mikael Koskinen](http://mikaelkoskinen.net) or [Adafy Oy](http://adafy.com).

## License

This software is distributed under the terms of the MIT License (see mit.txt).