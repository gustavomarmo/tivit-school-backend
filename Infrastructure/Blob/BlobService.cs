using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace edu_connect_backend.Infrastructure.Blob
{
    public class BlobService
    {
        private readonly BlobServiceClient _client;
        private const string Container = "uploads";

        public BlobService(IConfiguration configuration)
        {
            _client = new BlobServiceClient(
                configuration["AzureBlob:ConnectionString"]
            );
        }

        public async Task<string> UploadAsync(IFormFile arquivo, string pasta)
        {
            var containerClient = _client.GetBlobContainerClient(Container);

            var nomeArquivo = $"{pasta}/{Guid.NewGuid()}{Path.GetExtension(arquivo.FileName)}";
            var blobClient = containerClient.GetBlobClient(nomeArquivo);

            using var stream = arquivo.OpenReadStream();
            await blobClient.UploadAsync(stream, new BlobHttpHeaders
            {
                ContentType = arquivo.ContentType
            });

            return blobClient.Uri.ToString();
        }

        public async Task DeleteAsync(string url)
        {
            var uri = new Uri(url);
            var nomeArquivo = string.Join("/", uri.Segments[2..]);
            var containerClient = _client.GetBlobContainerClient(Container);
            var blobClient = containerClient.GetBlobClient(nomeArquivo);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}