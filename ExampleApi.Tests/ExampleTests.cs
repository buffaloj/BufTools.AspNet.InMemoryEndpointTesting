using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExampleApi.Tests
{
    [TestClass]
    public class ExampleTests
    {
        public ExampleTests()
        {
        }

        [TestMethod]
        public Task ExampleEndpoint_WithvalidRequest_ReturnsValue()
        {
            Assert.IsTrue(false);

            return Task.FromResult(true);
        }
    }
}