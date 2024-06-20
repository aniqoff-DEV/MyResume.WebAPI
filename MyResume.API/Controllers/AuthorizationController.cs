using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyResume.API.Contracts.Requests;
using MyResume.API.Contracts.Responses;
using MyResume.Domain.Interfaces.Services;

namespace MyResume.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] SignInRequest request)
        {
            var token = await _authorizationService.SignInUser(request.Role, request.Email, request.Password);

            if (token.IsFailure)
                return NotFound(token.Error);
            
            return Ok(new TokenResponse(token.Value));
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Registration([FromBody] SignUpRequest request)
        {
            var token = await _authorizationService.SignUpUser(request.Role, request.Name, request.Email, request.Password, request.CityId);

            if (token.IsFailure)
                return BadRequest(token.Error);

            return Ok(token.Value);
        }
    }
}
