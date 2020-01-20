namespace Sam016.Phonebook.API.Models.Commands.Phonebook
{
    public class CreatePhonebookCommand : BaseUserCommand, MediatR.IRequest<Sam016.Phonebook.Domain.Models.Phonebook>
    {
        public string Name { get; set; }
    }
}
