using ExampleApi.Requests;
using ExampleApi.Responses;
using ExampleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    /// <summary>
    /// A controller used as an example
    /// </summary>
    [ApiController]
    [Route("/api/v1")]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _exampleService;

        /// <summary>
        /// Constructs an instance of an object
        /// </summary>
        /// <param name="exampleService">A example of a service that is injected</param>
        /// <exception cref="ArgumentNullException">Thrown when a required param is null</exception>
        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// An example of a GET endpoint
        /// </summary>
        /// <param name="stringParam">Optional param to specify a string search param</param>
        /// <param name="intParam">Optional param to specify a int search param</param>
        /// <param name="floatParam">Optional param to specify a float search param</param>
        /// <returns>The endpoint response</returns>
        [HttpGet("example")]
        public IActionResult Get(string? stringParam, int? intParam, float? floatParam)
        {
            return Ok(new Response
                          {
                              ReturnString = _exampleService.GetExampleText(),
                              StringParam = stringParam,
                              IntParam = intParam,
                              FloatParam = floatParam
                          });
        }

        /// <summary>
        /// An example of a PUT endpoint
        /// </summary>
        /// <param name="request">A request object</param>
        /// <returns>The endpoint response</returns>
        [HttpPut("example")]
        public IActionResult Put(Request request)
        {
            return Ok(request.StringToReturn);
        }

        /// <summary>
        /// An example of a POST endpoint
        /// </summary>
        /// <param name="request">A request object</param>
        /// <returns></returns>
        [HttpPost("example")]
        public IActionResult Post(Request request)
        {
            return Ok(request.StringToReturn);
        }

        /// <summary>
        /// An example of a DELETE endpoint
        /// </summary>
        /// <returns>The endpoint response</returns>
        [HttpDelete("example")]
        public IActionResult Delete()
        {
            return Ok(_exampleService.GetExampleText());
        }
    }
}