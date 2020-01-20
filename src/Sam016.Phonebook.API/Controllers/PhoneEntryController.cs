using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sam016.Phonebook.API.Models.Commands.PhoneEntry;
using Sam016.Phonebook.API.Models.Queries.PhoneEntry;
using Sam016.Phonebook.API.Models.Requests.PhoneEntry;
using Sam016.Phonebook.API.Models.Responses.PhoneEntry;
using Sam016.Phonebook.Domain.Dtos;
using Sam016.Phonebook.Domain.Models;

namespace Sam016.Phonebook.API.Controllers
{
    [Authorize]
    [Route("api/phonebooks/{phonebookId}/phone-entries")]
    public class PhoneEntryController : BaseController
    {
        public PhoneEntryController(MediatR.IMediator mediatR, AutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(mediatR, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Creates a new PhoneEntry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="phonebookId"></param>
        /// <returns></returns>
        /// <response code="201">Returns the newly created phone entry</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatePhoneEntryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromRoute]uint phonebookId, [FromBody] CreatePhoneEntryRequest request)
        {
            // creating command from request
            var command = MapToCommand<CreatePhoneEntryCommand>(request);
            command.PhonebookId = phonebookId;

            // sending request command to handlers
            var result = await SendAsync<PhoneEntry>(command);

            // mapping result to dto
            var resultDto = MapToDto<PhoneEntryDto>(result);

            return Created(string.Empty, new CreatePhoneEntryResponse() { Data = resultDto });
        }

        /// <summary>
        /// Gets all the PhoneEntries
        /// </summary>
        /// <param name="request"></param>
        /// <param name="phonebookId"></param>
        /// <returns></returns>
        /// <response code="200">Returns the list of all phone-entries</response>
        [HttpGet]
        [ProducesResponseType(typeof(GetAllPhoneEntriesResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync([FromRoute]uint phonebookId, [FromQuery] GetAllPhoneEntriesRequest request)
        {
            // creating query from request
            var query = MapToQuery<GetAllPhoneEntriesQuery>(request);
            query.PhonebookId = phonebookId;

            // sending request query to handlers
            var result = await SendAsync(query);

            // mapping result to dto
            var resultDto = result.Select(r => MapToDto<PhoneEntryDto>(r));

            return Ok(new GetAllPhoneEntriesResponse() { Data = resultDto });
        }

        /// <summary>
        /// Gets the PhoneEntry by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="phonebookId"></param>
        /// <returns></returns>
        /// <response code="200">PhoneEntry</response>
        /// <response code="404">If the phone-entry is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetPhoneEntryByIdResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute]uint phonebookId, [FromRoute, BindRequired] uint id)
        {
            // creating query from request
            var query = MapToQuery<GetPhoneEntryByIdQuery>(id);
            query.PhonebookId = phonebookId;

            // sending request query to handlers
            var result = await SendAsync(query);

            // result is null, if phone-entry does not exist
            if (result == null)
            {
                return NotFound();
            }

            // mapping result to dto
            var resultDto = MapToDto<PhoneEntryDto>(result);

            return Ok(new GetPhoneEntryByIdResponse() { Data = resultDto });
        }

        /// <summary>
        /// Updates a Phone Entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="phonebookId"></param>
        /// <returns></returns>
        /// <response code="204">when phone entry is successfully updated</response>
        /// <response code="404">when phone entry is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute]uint phonebookId, [FromRoute]uint id, [FromBody] UpdatePhoneEntryRequest request)
        {
            // creating command from request
            var command = MapToCommand<UpdatePhoneEntryCommand>(request);
            command.PhonebookId = phonebookId;
            command.Id = id;

            // sending request command to handlers
            var result = await SendAsync(command);

            // phone entry is not found
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a Phone Entry
        /// </summary>
        /// <param name="request"></param>
        /// <param name="phonebookId"></param>
        /// <returns></returns>
        /// <response code="204">when phone entry is successfully deleted</response>
        /// <response code="404">when phone entry is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute]uint phonebookId, [FromRoute]uint id)
        {
            // creating command from request
            var command = MapToCommand<DeletePhoneEntryCommand>(id);
            command.PhonebookId = phonebookId;
            command.Id = id;

            // sending request command to handlers
            var result = await SendAsync(command);

            // phone entry is not found
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
