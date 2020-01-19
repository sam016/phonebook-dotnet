namespace Sam016.Phonebook.API.Models.Commands.PhoneEntry
{
    public class UpdatePhoneEntryCommand : BaseCommand, MediatR.IRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
