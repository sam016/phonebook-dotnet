using System.Collections.Generic;

namespace Sam016.Phonebook.API.Models.Queries.PhoneEntry
{
    public class GetAllPhoneEntriesQuery : BaseUserQuery, MediatR.IRequest<IEnumerable<Sam016.Phonebook.Domain.Models.PhoneEntry>>
    {
        public uint PhonebookId { get; set; }
    }
}
