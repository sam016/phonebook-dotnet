namespace Sam016.Phonebook.API.Models.Commands.PhoneEntry
{
    public class UpdatePhoneEntryCommand : BaseUserCommand, MediatR.IRequest<Domain.Models.PhoneEntry>
    {
        public uint Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public uint PhonebookId { get; set; }
    }
}
