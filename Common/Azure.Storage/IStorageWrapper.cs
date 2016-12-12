using System;
using System.Threading.Tasks;

namespace Azure.Storage
{
    public interface IStorageWrapper
    {
        Task PutDataAsync<TData>(TData data, string containerName, Guid guid);
        Task<TData> GetDataAsync<TData>(Guid guid, string containerName);
    }
}