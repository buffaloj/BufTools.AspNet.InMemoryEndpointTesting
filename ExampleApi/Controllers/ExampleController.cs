using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class ExampleController : ControllerBase
    {
        [HttpGet("example")]
        public IActionResult Get()
        {
            return Ok("The get worked!");
        }
    }
}