using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rentful.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class HiController(ILogger<HiController> logger) : ControllerBase
    {

        private readonly ILogger<HiController> _logger = logger;

        [AllowAnonymous]
        [HttpGet]
        public async Task<string> Get()
        {
            _logger.LogWarning("Test logger");
            return await Task.FromResult("Hello, World!");
        }
    }
}
