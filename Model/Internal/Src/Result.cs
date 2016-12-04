using System.Runtime.Serialization;

namespace Model.Internal
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public int Value;

        public Result(int value)
        {
            Value = value;
        }
    }
}
