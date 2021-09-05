using _4TeamTask.Services;
using _4TeamTask.WebApi.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace _4TeamTask.WebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger _logger;

        public UserController(ILogger<UserController> logger, IUserService service)
        {
            _logger = logger;
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
                    var data = await _service.GetUserData(userId);
                    _logger.LogInformation($"Request to: UserController, GetUserData with prameter userId: {userId}, result data: {Newtonsoft.Json.JsonConvert.SerializeObject(data)}");
                    return Ok(data);
                }
                else
                {
                    var message = $"Error: userId: {userId} parameter was invalid or missing.";
                    _logger.LogInformation($"Request to: UserController, GetUserData. {message}");
                    return BadRequest(message);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
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
                    var data = await _service.SaveUserData(userId);
                    _logger.LogInformation($"Request to: UserController, SaveUserData with prameter userId: {userId}, result data: {Newtonsoft.Json.JsonConvert.SerializeObject(data)}");
                    return Ok(data);
                }
                else
                {
                    var message = $"Error: userId: {userId} parameter was invalid or missing.";
                    _logger.LogInformation($"Request to: UserController, SaveUserData. {message}");
                    return BadRequest(message);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return StatusCode(500, ErrorMessages.ErrorHasOccured);
            }
        }
    }
}
