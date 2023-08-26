using Data.DTOs;
using Data.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Utilities;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> RegisterAccount(UserCreationDto userCreationDto)
        {

            var userCreated = accountService.RegisterAccount(userCreationDto);

            if (!userCreated.IsSuccess)
            {
                return BadRequest(userCreated.ErrorMessage);
            }

            return userCreated.Data;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials credentials)
        {
            var authenticationResponse = accountService.Login(credentials);

            if (authenticationResponse == null)
            {
                return BadRequest("Incorrect credentials");
            }

            return authenticationResponse;
        }

        [HttpGet("refreshToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<AuthenticationResponse> RefreshToken()
        {
            return accountService.Refreshtoken();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserResponseDto>> UpdateAccount(int id, UserUpdateDto userUpdate)
        {
            var userResponse = accountService.UpdateAccount(id, userUpdate);

            if (!userResponse.IsSuccess)
            {
                return BadRequest(userResponse.ErrorMessage);
            }

            return userResponse.Data;
        }

        [HttpPost("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserResponseDto>> ChangePassword(int id, UserPasswordDto passwordDto)
        {
            var userResponse = accountService.ChangePassword(id, passwordDto);

            if (!userResponse.IsSuccess)
            {
                return BadRequest(userResponse.ErrorMessage);
            }

            return userResponse.Data;
        }

        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserResponseDto>> GetAccount(int id)
        {
            var userResponse = accountService.GetAccount(id);

            if (!userResponse.IsSuccess)
            {
                return NotFound(userResponse.ErrorMessage);
            }

            return userResponse.Data;
        }
    }
}
