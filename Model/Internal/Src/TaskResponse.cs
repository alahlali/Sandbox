using System;
using System.Runtime.Serialization;

namespace Model.Internal
{
    [DataContract]
    public sealed class TaskResponse
    {
        [DataMember] public Guid Id;
        [DataMember] public bool IsSuccess;
        [DataMember] public Result Result;

        public TaskResponse(Guid id, bool isSuccess, Result result)
        {
            Id = id;
            IsSuccess = isSuccess;
            Result = result;
        }
    }
}