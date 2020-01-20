namespace Sam016.Phonebook.API.Models.Commands.PhoneEntry
{
    public class CreatePhoneEntryCommand : BaseUserCommand, MediatR.IRequest<Sam016.Phonebook.Domain.Models.PhoneEntry>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public uint PhonebookId { get; set; }
    }
}
