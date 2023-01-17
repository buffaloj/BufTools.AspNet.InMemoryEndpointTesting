using BufTools.AspNet.TestFramework;
using ExampleApi.Requests;
using ExampleApi.Responses;
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
            
            var result = await browser.CreateRequest("/api/v1/example").GetAsync();
            var json = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = JsonSerializer.Deserialize<Response>(json, options);

            Assert.AreEqual(_testText, response?.ReturnString);
        }

        [TestMethod]
        public async Task CallExampleEndpoint_WithPutRequest_Succeeds()
        {
            var browser = new Browser<Program>(c => { });
            var request = Request.Example();
            var json = JsonSerializer.Serialize(request);

            var response = await browser.CreateRequest("/api/v1/example")
                                        .WithJsonContent(json)
                                        .PutAsync();

            var message = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(request.StringToReturn, message);
        }

        [TestMethod]
        public async Task CallExampleEndpoint_WithPostRequest_Succeeds()
        {
            var browser = new Browser<Program>(c => { });
            var request = Request.Example();
            var json = JsonSerializer.Serialize(request);

            var response = await browser.CreateRequest("/api/v1/example")
                                        .WithJsonContent(json)
                                        .PostAsync();

            var message = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(request.StringToReturn, message);
        }

        [TestMethod]
        public async Task CallExampleEndpoint_WithDeleteRequest_ReturnsDependencyValue()
        {
            _mock.Setup(m => m.GetExampleText()).Returns(_testText);
            var browser = new Browser<Program>(c =>
            {
                c.UseDependency(_mock.Object);
                c.UseDependency(_mock.Object);
            });

            var response = await browser.CreateRequest("/api/v1/example").DeleteAsync();
            var message = await response.Content.ReadAsStringAsync();

            Assert.AreEqual(_testText, message);
        }

        [TestMethod]
        public async Task CallExampleEndpoint_WithOptionalParams_ReturnsOptonalParams()
        {
            var browser = new Browser<Program>(c => { });
            var stringParam = "AStringParam!";
            var intParam = 8;
            var floatParam = 20.63f;

            var result = await browser.CreateRequest("/api/v1/example")
                                      .WithQueryParam("stringParam", stringParam)
                                      .WithQueryParam("intParam", intParam)
                                      .WithQueryParam("floatParam", floatParam)
                                      .GetAsync();

            var json = await result.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = JsonSerializer.Deserialize<Response>(json, options);

            Assert.AreEqual(stringParam, response?.StringParam);
            Assert.AreEqual(intParam, response?.IntParam);
            Assert.AreEqual(floatParam, response?.FloatParam);
        }
    }
}