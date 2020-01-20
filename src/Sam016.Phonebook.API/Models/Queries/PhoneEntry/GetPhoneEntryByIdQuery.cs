namespace Sam016.Phonebook.API.Models.Queries.PhoneEntry
{
    public class GetPhoneEntryByIdQuery : BaseUserQuery, MediatR.IRequest<Sam016.Phonebook.Domain.Models.PhoneEntry>
    {
        public uint Id { get; set; }
        public uint PhonebookId { get; set; }
    }
}
