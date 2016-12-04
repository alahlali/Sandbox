using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model.Internal.Tests
{
    [TestClass]
    public class DataContractTests
    {
        [TestMethod]
        public void TestResultSerialization()
        {
            var instance = new Result(12);
            var result = WcfTestHelper.DataContractSerializationRoundTrip(instance);
            Assert.AreEqual(instance.Value, result.Value);
        }
    }
}
