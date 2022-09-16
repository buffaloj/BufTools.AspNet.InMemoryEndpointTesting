using AspTestFramework;
using ExampleApi.Requests;
using ExampleApi.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text.Json;

namespace ExampleApi.Tests
{
    [TestClass]
    public class CallEndpointTests
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

        [TestMethod]
        public async Task CallExampleEndpoint_WithPutRequest_Succeeds()
        {
            var browser = new Browser<Program>(c => { });
            var request = PutRequest.Example();
            var json = JsonSerializer.Serialize(request);

            var response = await browser.CreateRequest("/api/v1/example")
                                        .WithJsonContent(json)
                                        .PutAsync();

            var message = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(request.StringToReturn, message);
        }
    }
}