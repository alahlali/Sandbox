using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.IO;

namespace Model.Internal.Tests
{
    public static class WcfTestHelper
    {
        /// <summary>
        /// Uses a <see cref="DataContractSerializer"/> to serialise the object into
        /// memory, then deserialise it again and return the result.  This is useful
        /// in tests to validate that your object is serialisable, and that it
        /// serialises correctly.
        /// </summary>
        public static T DataContractSerializationRoundTrip<T>(T obj)
        {
            return DataContractSerializationRoundTrip(obj, null);
        }

        /// <summary>
        /// Uses a <see cref="DataContractSerializer"/> to serialise the object into
        /// memory, then deserialise it again and return the result.  This is useful
        /// in tests to validate that your object is serialisable, and that it
        /// serialises correctly.
        /// </summary>
        public static T DataContractSerializationRoundTrip<T>(T obj,
                        IEnumerable<Type> knownTypes)
        {
            var serializer = new DataContractSerializer(obj.GetType(), knownTypes);
            var memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, obj);
            memoryStream.Position = 0;
            obj = (T)serializer.ReadObject(memoryStream);
            return obj;
        }
    }
}
