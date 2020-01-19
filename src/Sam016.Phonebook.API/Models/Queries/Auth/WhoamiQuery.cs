using System.Collections.Generic;

namespace Sam016.Phonebook.API.Models.Queries.Auth
{
    public class WhoamiQuery : BaseQuery, MediatR.IRequest<Sam016.Phonebook.Domain.Models.AuthProfile>
    {
    }
}
