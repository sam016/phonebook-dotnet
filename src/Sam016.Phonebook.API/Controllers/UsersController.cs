using System.Threading.Tasks;

namespace Sam016.Phonebook.API.Controllers
{
    public class UserController : BaseController
    {
        public UserController(MediatR.IMediator mediatR) : base(mediatR)
        {
        }
    }
}
