using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sam016.Phonebook.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly MediatR.IMediator MediatR;
        protected readonly AutoMapper.IMapper Mapper;

        public BaseController(MediatR.IMediator mediatR, AutoMapper.IMapper mapper)
        {
            MediatR = mediatR;
            Mapper = mapper;
        }

        protected Task<TResponse> SendAsync<TResponse>(MediatR.IRequest<TResponse> request)
        {
            return MediatR.Send(request);
        }

        protected TQuery MapToQuery<TQuery>(object request = null)
        {
            return Mapper.Map<TQuery>(request);
        }

        protected TCommand MapToCommand<TCommand>(object request = null)
        {
            return Mapper.Map<TCommand>(request);
        }

        protected TDto MapToDto<TDto>(object model)
        {
            return Mapper.Map<TDto>(model);
        }
    }
}
