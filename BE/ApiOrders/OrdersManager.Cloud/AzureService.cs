using OrdersManager.Cloud.Interfaces;
using Sandboxable.Microsoft.WindowsAzure.Storage;
using Sandboxable.Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManager.Cloud
{
    public class AzureService : ICloudServices
    {
        public async Task<bool> UploadFileAsync(string filePath)
        {
            
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            string sourceFile = null;
            string destinationFile = null;

            // Retrieve the connection string for use with the application. The storage connection string is stored
            // in an environment variable on the machine running the application called storageconnectionstring.
            // If the environment variable is created after the application is launched in a console or with Visual
            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=nicotiseira;AccountKey=anvxcKA5hWAKa+7FaiX41yTS/Aku6aW0bsOJnYGjfOrwHB9YgBz18YR8jp08vdtgs2g1p3p9isEzPlS8nLI/PA==;EndpointSuffix=core.windows.net";

            string containerName = "imagestest";

            //Environment.GetEnvironmentVariable("storageconnectionstring");

            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 

                  //  Uri urlTest = new Uri("https://nicotiseira.blob.core.windows.net/imagestest");
                    cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

                    //   await cloudBlobContainer.CreateAsync();
                    //  Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);
                    //   Console.WriteLine();

                    if (cloudBlobContainer.CreateIfNotExistsAsync().Result)
                    {

                        // Set the permissions so the blobs are public. 
                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                    }


                    //CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filePath); cloudBlockBlob.Properties.ContentType = imageToUpload.ContentType;

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(containerName);

                    await cloudBlockBlob.UploadFromFileAsync(filePath);



                    //// Create a file in your local MyDocuments folder to upload to a blob.
                    //string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    //string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";
                    //sourceFile = Path.Combine(localPath, localFileName);
                    //// Write text to the file.
                    //File.WriteAllText(sourceFile, "Hello, World!");

                    //Console.WriteLine("Temp file = {0}", sourceFile);
                    //Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);
                    //Console.WriteLine();

                    //// Get a reference to the blob address, then upload the file to the blob.
                    //// Use the value of localFileName for the blob name.
                    //CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                    //await cloudBlockBlob.UploadFromFileAsync(sourceFile);

                    //// List the blobs in the container.
                    //Console.WriteLine("Listing blobs in container.");
                    //BlobContinuationToken blobContinuationToken = null;
                    //do
                    //{
                    //    var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                    //    // Get the value of the continuation token returned by the listing call.
                    //    blobContinuationToken = results.ContinuationToken;
                    //    foreach (IListBlobItem item in results.Results)
                    //    {
                    //        Console.WriteLine(item.Uri);
                    //    }
                    //} while (blobContinuationToken != null); // Loop while the continuation token is not null.
                    //Console.WriteLine();

                    //// Download the blob to a local file, using the reference created earlier. 
                    //// Append the string "_DOWNLOADED" before the .txt extension so that you can see both files in MyDocuments.
                    //destinationFile = sourceFile.Replace(".txt", "_DOWNLOADED.txt");
                    //Console.WriteLine("Downloading blob to {0}", destinationFile);
                    //Console.WriteLine();
                    //await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);

                    return true;

                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);

                    return false;

                }
                finally
                {
                    Console.WriteLine("Press any key to delete the sample files and example container.");
                    Console.ReadLine();
                    // Clean up resources. This includes the container and the two temp files.
                    Console.WriteLine("Deleting the container and any blobs it contains");
                    if (cloudBlobContainer != null)
                    {
                        await cloudBlobContainer.DeleteIfExistsAsync();
                    }
                    Console.WriteLine("Deleting the local source file and local downloaded files");
                    Console.WriteLine();
                    File.Delete(sourceFile);
                    File.Delete(destinationFile);


                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");

                return false;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AzureService() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
