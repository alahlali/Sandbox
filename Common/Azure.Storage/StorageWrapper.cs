using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure.Storage
{
    public class StorageWrapper : IStorageWrapper
    {
        private readonly CloudBlobClient _blobClient;

        public StorageWrapper(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            _blobClient = storageAccount.CreateCloudBlobClient();
        }

        public Task PutDataAsync<TData>(TData data, string containerName, Guid guid)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(guid.ToString());
            MemoryStream stream = SerializeToStream(data);

            blockBlob.UploadFromStream(stream);
            return Task.FromResult(false);
        }

        public Task<TData> GetDataAsync<TData>(Guid guid, string containerName)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(guid.ToString());

            var data = DeSerializeFromStream<TData>(stream => blockBlob.DownloadToStream(stream));

            return Task.FromResult(data);
        }

        private static MemoryStream SerializeToStream<TData>(TData data)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(data.GetType(), new List<Type>());
            ser.WriteObject(stream, data);
            stream.Position = 0;

            return stream;
        }

        private static TData DeSerializeFromStream<TData>(Action<MemoryStream> getData)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TData), new List<Type>());
            getData(stream);
            stream.Position = 0;
            return (TData)ser.ReadObject(stream);
        }
    }
}