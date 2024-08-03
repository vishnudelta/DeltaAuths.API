using Auth.Model;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeltaAuths.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserAuth _UserAuthRepository;

        public LoginController(IConfiguration configuration, IUserAuth UserAuthRepository)
        {
            _configuration = configuration;
            _UserAuthRepository = UserAuthRepository;
        }

        [HttpPost("UserAuth")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            return Ok(await _UserAuthRepository.GetUserAsync(user.Username, user.Password));
        }

        [HttpGet("Demo")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value3", "value4", "value5" };
        }
    }
}
