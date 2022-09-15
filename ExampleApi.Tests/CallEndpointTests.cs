using AspTestFramework;
using ExampleApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExampleApi.Tests
{
    [TestClass]
    public class ExampleTests
    {
        private Mock<IExampleService> _mock = new Mock<IExampleService>();

        private readonly string _testText = "TestText!";

        [TestMethod]
        public async Task CallExampleEndpoint_WithValidRequest_Succeeds()
        {
            var browser = new Browser<Program>(c => {});

            var response = await browser.CreateRequest("/api/v1/example")
                                        .GetAsync();

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task CallExampleEndpoint_WithDependency_ReturnsDependencyValue()
        {
            _mock.Setup(m => m.GetExampleText()).Returns(_testText);
            var browser = new Browser<Program>(c => c.UseDependency(_mock.Object));
            
            var response = await browser.CreateRequest("/api/v1/example").GetAsync();
            var message = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(_testText, message);
        }
    }
}