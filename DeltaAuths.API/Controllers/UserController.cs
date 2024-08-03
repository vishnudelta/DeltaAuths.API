using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Auth.Model;
using Auth.Services;

namespace DeltaAuths.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserMaster _userMaster;

        public UserController(IConfiguration configuration, IUserMaster userMaster)
        {
            _configuration = configuration;
            _userMaster = userMaster;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserMaster user)
        {
            var response = await _userMaster.AddUserAsync(user);
            if ((bool)response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserMaster user)
        {
            var response = await _userMaster.UpdateUserAsync(user);
            if ((bool)response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var response = await _userMaster.DeleteUserAsync(userId);
            if ((bool)response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
