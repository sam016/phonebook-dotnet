using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sam016.Phonebook.API.Models.Queries.User;
using Sam016.Phonebook.Domain.Models;

namespace Sam016.Phonebook.API.RequestHandlers
{
    public class UsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        public Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
