using AspTestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExampleApi.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private readonly Browser<Program> _browser;

        public ExampleTests()
        {
            _browser = new Browser<Program>();
        }

        [TestMethod]
        public async Task ExampleEndpoint_WithvalidRequest_ReturnsValue()
        {
            var response = await _browser.CreateRequest("/api/v1/example")
                                         .GetAsync();

            var message = response.Content.ReadAsStringAsync();

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}