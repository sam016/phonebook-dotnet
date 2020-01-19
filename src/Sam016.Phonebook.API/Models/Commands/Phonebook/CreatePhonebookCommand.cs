namespace Sam016.Phonebook.API.Models.Commands.Phonebook
{
    public class CreatePhonebookCommand : BaseCommand, MediatR.IRequest<Sam016.Phonebook.Domain.Models.Phonebook>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
