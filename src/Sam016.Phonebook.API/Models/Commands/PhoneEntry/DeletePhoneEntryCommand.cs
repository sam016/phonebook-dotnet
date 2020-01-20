namespace Sam016.Phonebook.API.Models.Commands.PhoneEntry
{
    public class DeletePhoneEntryCommand : BaseUserCommand, MediatR.IRequest<Domain.Models.PhoneEntry>
    {
        public uint Id { get; set; }
        public uint PhonebookId { get; set; }
    }
}
