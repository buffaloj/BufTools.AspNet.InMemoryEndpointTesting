using ExampleApi.Requests;
using ExampleApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _exampleService;

        public ExampleController(IExampleService exampleService)
        {
            _exampleService = exampleService ?? throw new ArgumentNullException();
        }

        [HttpGet("example")]
        public IActionResult Get()
        {
            return Ok(_exampleService.GetExampleText());
        }

        [HttpPut("example")]
        public IActionResult Put(Request request)
        {
            return Ok(request.StringToReturn);
        }

        [HttpPost("example")]
        public IActionResult Post(Request request)
        {
            return Ok(request.StringToReturn);
        }

        [HttpDelete("example")]
        public IActionResult Delete()
        {
            return Ok(_exampleService.GetExampleText());
        }
    }
}