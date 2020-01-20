using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sam016.Phonebook.API.Models.Queries.User;
using Sam016.Phonebook.API.Models.Requests.User;
using Sam016.Phonebook.API.Models.Responses.User;
using Sam016.Phonebook.Domain.Dtos;

namespace Sam016.Phonebook.API.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        public UserController(MediatR.IMediator mediatR, AutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(mediatR, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Gets all the Users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the list of all users</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllUsersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllUsersRequest request)
        {
            // creating query from request
            var query = MapToQuery<GetAllUsersQuery>(request);

            // sending request query to handlers
            var result = await SendAsync(query);

            // mapping result to dto
            var resultDto = result.Select(r => MapToDto<UserDto>(r));

            return Ok(new GetAllUsersResponse() { Data = resultDto });
        }

        /// <summary>
        /// Gets the User by ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">User</response>
        /// <response code="404">If the user is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, BindRequired] uint id)
        {
            // creating query from request
            var query = MapToQuery<GetUserByIdQuery>(id);

            // sending request query to handlers
            var result = await SendAsync(query);

            // result is null, if user does not exist
            if (result == null)
            {
                return NotFound();
            }

            // mapping result to dto
            var resultDto = MapToDto<UserDto>(result);

            return Ok(new GetUserByIdResponse() { Data = resultDto });
        }
    }
}
