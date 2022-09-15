using AspTestFramework;
using ExampleApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExampleApi.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private readonly Browser<Program> _browser;
        private readonly Mock<IExampleService> _serviceMock = new Mock<IExampleService>();

        public ExampleTests()
        {
            _serviceMock.Setup(m => m.GetExampleText())
                        .Returns("TestText!");

            _browser = new Browser<Program>(c => c.UseDependency(_serviceMock.Object));
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