using _4TeamTask.Services;
using _4TeamTask.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _4TeamTask.WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get/{userId}")]
        public async Task<IActionResult> GetUserData(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    return Ok(await _service.GetUserData(userId));
                }
                else
                {
                    return BadRequest("Error: userId parameter was invalid or missing.");
                }
            }
            catch
            {
                return StatusCode(500, ErrorMessages.ErrorHasOccured);
            }
        }

        [HttpGet]
        [Route("save/{userId}")]
        public async Task<IActionResult> SaveUserData(string userId)
        {
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    return Ok(await _service.SaveUserData(userId));
                }
                else
                {
                    return BadRequest("Error: userId parameter was invalid or missing.");
                }
            }
            catch
            {
                return StatusCode(500, ErrorMessages.ErrorHasOccured);
            }
        }
    }
}
