using PhonebookModel = Sam016.Phonebook.Domain.Models.Phonebook;

namespace Sam016.Phonebook.API.Models.Commands.Phonebook
{
    public class UpdatePhonebookCommand : BaseUserCommand, MediatR.IRequest<PhonebookModel>
{
    public uint Id { get; set; }
    public string Name { get; set; }
}
}
