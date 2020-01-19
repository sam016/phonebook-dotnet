using System.Collections.Generic;

namespace Sam016.Phonebook.API.Models.Queries.PhoneEntry
{
    public class GetAllPhoneEntriesQuery : BaseQuery, MediatR.IRequest<IEnumerable<Sam016.Phonebook.Domain.Models.PhoneEntry>>
    {
        public int PhonebookId { get; set; }
        public int UserId { get; set; }
    }
}
