using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sam016.Phonebook.API.Models.Commands.Auth;
using Sam016.Phonebook.API.Models.Queries.Auth;
using Sam016.Phonebook.API.Models.Requests.Auth;
using Sam016.Phonebook.API.Models.Responses.Auth;
using Sam016.Phonebook.Domain.Dtos;
using Sam016.Phonebook.Domain.Models;

namespace Sam016.Phonebook.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        public AuthController(MediatR.IMediator mediatR, AutoMapper.IMapper mapper) : base(mediatR, mapper)
        {
        }

        /// <summary>
        /// Logs in the user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the auth user model</response>
        /// <response code="401">Invalid email/password</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            // creating command from request
            var command = MapToCommand<LoginCommand>(request);

            // sending request command to handlers
            var result = await SendAsync<AuthToken>(command);

            if (result == null)
            {
                return Unauthorized();
            }

            // mapping result to dto
            var resultDto = MapToDto<AuthTokenDto>(result);

            return Ok(new LoginResponse() { Data = resultDto });
        }

        /// <summary>
        /// Logs out the user
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Success if user id logged out</response>
        /// <response code="401">Invalid authorization</response>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LogoutAsync()
        {
            // creating command from request
            var command = MapToCommand<LogoutCommand>(new LogoutRequest());

            // sending request command to handlers
            await SendAsync<MediatR.Unit>(command);

            return Ok();
        }

        /// <summary>
        /// Refreshes the token
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Renewed jwt auth token</response>
        /// <response code="401">Invalid authorization</response>
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RefreshTokenAsync()
        {
            // creating command from request
            var command = MapToCommand<RefreshTokenCommand>(new RefreshTokenRequest());

            // sending request command to handlers
            var result = await SendAsync<AuthToken>(command);

            // mapping result to dto
            var resultDto = MapToDto<AuthTokenDto>(result);

            return Ok(new RefreshTokenResponse() { Data = resultDto });
        }

        /// <summary>
        /// Who Am I - returns the logged in user info
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Logged in user profile</response>
        /// <response code="401">Invalid authorization</response>
        [HttpPost("whoami")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> WhoamiAsync()
        {
            // creating query from request
            var query = MapToQuery<WhoamiQuery>(new WhoamiRequest());

            // sending request query to handlers
            var result = await SendAsync<AuthProfile>(query);

            // mapping result to dto
            var resultDto = MapToDto<AuthProfileDto>(result);

            return Ok(new WhoamiResponse() { Data = resultDto });
        }
    }
}
