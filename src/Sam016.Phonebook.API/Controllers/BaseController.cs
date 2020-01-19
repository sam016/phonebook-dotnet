using System.Threading.Tasks;

namespace Sam016.Phonebook.API.Controllers
{
    public class BaseController
    {
        protected readonly MediatR.IMediator MediatR;

        public BaseController(MediatR.IMediator mediatR)
        {
            MediatR = mediatR;
        }

        protected Task<TResponse> SendAsync<TResponse>(MediatR.IRequest<TResponse> request)
        {
            return MediatR.Send(request);
        }
    }
}
