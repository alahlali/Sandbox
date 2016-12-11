using System;
using System.Runtime.Serialization;

namespace Model.Internal
{
    [DataContract]
    public sealed class TaskRequestMessage
    {
        [DataMember] public Guid Id;

        public TaskRequestMessage(Guid id)
        {
            Id = id;
        }
    }
}