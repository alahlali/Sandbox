using System.Runtime.Serialization;

namespace Model.Internal
{
    [DataContract]
    public sealed class TaskResponse
    {
        [DataMember] public string Id;
        [DataMember] public bool IsSuccess;
        [DataMember] public Result Result;

        public TaskResponse(string id, bool isSuccess, Result result)
        {
            Id = id;
            IsSuccess = isSuccess;
            Result = result;
        }
    }
}