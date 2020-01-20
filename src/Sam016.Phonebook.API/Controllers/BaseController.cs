using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sam016.Phonebook.API.Models.Commands;
using Sam016.Phonebook.API.Models.Queries;

namespace Sam016.Phonebook.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly MediatR.IMediator MediatR;
        protected readonly AutoMapper.IMapper Mapper;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public BaseController(MediatR.IMediator mediatR, AutoMapper.IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            MediatR = mediatR;
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
        }

        protected Task<TResponse> SendAsync<TResponse>(MediatR.IRequest<TResponse> request)
        {
            return MediatR.Send(request);
        }

        protected TQuery MapToQuery<TQuery>(object request = null)
        {
            var query = Mapper.Map<TQuery>(request);

            if (query is BaseUserQuery)
            {
                (query as BaseUserQuery).UserId = uint.Parse(HttpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
            }

            return query;
        }

        protected TCommand MapToCommand<TCommand>(object request = null)
        {
            var command = Mapper.Map<TCommand>(request);

            if (command is BaseUserCommand)
            {
                (command as BaseUserCommand).UserId = uint.Parse(HttpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);
            }

            return command;
        }

        protected TDto MapToDto<TDto>(object model)
        {
            return Mapper.Map<TDto>(model);
        }
    }
}
