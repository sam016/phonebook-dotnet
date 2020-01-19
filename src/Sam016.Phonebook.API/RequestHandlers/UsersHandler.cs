using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sam016.Phonebook.API.Models.Queries.User;
using Sam016.Phonebook.Domain.Models;
using Sam016.Phonebook.Infrastructure.Repositories;

namespace Sam016.Phonebook.API.RequestHandlers
{
    public class UsersHandler :
        IRequestHandler<GetAllUsersQuery, IEnumerable<User>>,
        IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository UserRepository;

        public UsersHandler(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await UserRepository.GetAllAsync();
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await UserRepository.GetByIdAsync(request.Id);
        }
    }
}
