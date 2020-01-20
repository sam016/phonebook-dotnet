using System.Collections.Generic;

namespace Sam016.Phonebook.API.Models.Queries.Phonebook
{
    public class GetAllPhonebooksQuery : BaseUserQuery, MediatR.IRequest<IEnumerable<Sam016.Phonebook.Domain.Models.Phonebook>>
    {
    }
}
