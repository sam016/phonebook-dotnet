using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sam016.Phonebook.API.Models.Commands.Phonebook;
using Sam016.Phonebook.API.Models.Queries.Phonebook;
using Sam016.Phonebook.API.Models.Requests.Phonebook;
using Sam016.Phonebook.API.Models.Responses.Phonebook;
using Sam016.Phonebook.Domain.Dtos;
using Sam016.Phonebook.Domain.Models;

namespace Sam016.Phonebook.API.Controllers
{
    [Authorize]
    [Route("api/phonebooks")]
    public class PhonebookController : BaseController
    {
        public PhonebookController(MediatR.IMediator mediatR, AutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(mediatR, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Creates a new Phonebook
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created phonebook</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePhonebookResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePhonebookRequest request)
        {
            // creating command from request
            var command = MapToCommand<CreatePhonebookCommand>(request);

            // sending request command to handlers
            var result = await SendAsync<Domain.Models.Phonebook>(command);

            // mapping result to dto
            var resultDto = MapToDto<PhonebookDto>(result);

            return Created(string.Empty, new CreatePhonebookResponse() { Data = resultDto });
        }

        /// <summary>
        /// Gets all the Phonebooks
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Returns the list of all phonebooks</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllPhonebooksResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromQuery] GetAllPhonebooksRequest request)
        {
            // creating query from request
            var query = MapToQuery<GetAllPhonebooksQuery>(request);

            // sending request query to handlers
            var result = await SendAsync(query);

            // mapping result to dto
            var resultDto = result.Select(r => MapToDto<PhonebookDto>(r));

            return Ok(new GetAllPhonebooksResponse() { Data = resultDto });
        }

        /// <summary>
        /// Gets the Phonebook by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Phonebook</response>
        /// <response code="404">If the phonebook is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPhonebookByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute, BindRequired] uint id)
        {
            // creating query from request
            var query = MapToQuery<GetPhonebookByIdQuery>(id);

            // sending request query to handlers
            var result = await SendAsync(query);

            // result is null, if phonebook does not exist
            if (result == null)
            {
                return NotFound();
            }

            // mapping result to dto
            var resultDto = MapToDto<PhonebookDto>(result);

            return Ok(new GetPhonebookByIdResponse() { Data = resultDto });
        }

        /// <summary>
        /// Updates a Phonebook
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="204">when phonebook is successfully updated</response>
        /// <response code="404">when phonebook is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute]uint id, [FromBody] UpdatePhonebookRequest request)
        {
            // creating command from request
            var command = MapToCommand<UpdatePhonebookCommand>(request);
            command.Id = id;

            // sending request command to handlers
            var result = await SendAsync(command);

            // phonebook is not found
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a Phonebook
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="204">when phonebook is successfully deleted</response>
        /// <response code="404">when phonebook is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute]uint id)
        {
            // creating command from request
            var command = MapToCommand<DeletePhonebookCommand>(id);
            command.Id = id;

            // sending request command to handlers
            var result = await SendAsync(command);

            // phonebook is not found
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
