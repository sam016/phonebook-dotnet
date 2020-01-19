namespace Sam016.Phonebook.API.Models.Queries.PhoneEntry
{
    public class GetPhoneEntryByIdQuery : BaseQuery, MediatR.IRequest<Sam016.Phonebook.Domain.Models.PhoneEntry>
    {
        public int Id { get; set; }
        public int PhonebookId { get; set; }
        public int UserId { get; set; }
    }
}
