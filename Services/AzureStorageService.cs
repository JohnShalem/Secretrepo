using Azure.Storage.Blobs;

namespace WhatsAppAPI.Services
{
    public class AzureStorageService
    {
        public async void SaveRequestToAzureFile(string request)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=wtspteststorage;AccountKey=+qzC5WmbZ3V3wKM9FlaR1S8MEdcCO1pYBld/J2vuZFzL8Ty7zxCwfWT4EkVqXVQdSQ9h8By1XdJV+AStOyxWQw==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("wtspblob");
            BlobClient blobClient2 = containerClient.GetBlobClient("wtspreqresponse.txt");


            string existingContent;
            using (MemoryStream stream = new MemoryStream())
            {
                await blobClient2.DownloadToAsync(stream);
                existingContent = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }

            string data = existingContent + "           " + DateTime.Now.ToString() + request;
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data)))
            {
                await blobClient2.UploadAsync(stream, true);
            }
        }

        public async Task<bool> SaveResponseToAzureFile(string response)
        {
            // Get a reference to the container
            BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=wtspteststorage;AccountKey=+qzC5WmbZ3V3wKM9FlaR1S8MEdcCO1pYBld/J2vuZFzL8Ty7zxCwfWT4EkVqXVQdSQ9h8By1XdJV+AStOyxWQw==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("wtspblob");

            // Get a reference to the existing blob
            BlobClient blobClient = containerClient.GetBlobClient("wtspresponse.txt");
            // Download the existing content of the blob
            string existingContent;
            using (MemoryStream stream = new MemoryStream())
            {
                await blobClient.DownloadToAsync(stream);
                existingContent = System.Text.Encoding.UTF8.GetString(stream.ToArray());
            }

            string updatedContent = existingContent + "           " + DateTime.Now.ToString() + response;

            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(updatedContent)))
            {
                await blobClient.UploadAsync(stream, true);
                return true; 
            }
            
        }
    }
}
