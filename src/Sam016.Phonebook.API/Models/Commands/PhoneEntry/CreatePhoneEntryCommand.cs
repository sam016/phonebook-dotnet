namespace Sam016.Phonebook.API.Models.Commands.PhoneEntry
{
    public class CreatePhoneEntryCommand : BaseCommand, MediatR.IRequest<Sam016.Phonebook.Domain.Models.PhoneEntry>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrganizationName { get; set; }
        public string Address { get; set; }
        public int PhonebookId { get; set; }
        public int UserId { get; set; }
    }
}
