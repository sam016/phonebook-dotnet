using System.Collections.Generic;

namespace Sam016.Phonebook.API.Models.Queries.User
{
    public class GetAllUsersQuery : BaseQuery, MediatR.IRequest<IEnumerable<Sam016.Phonebook.Domain.Models.User>>
    {
    }
}
