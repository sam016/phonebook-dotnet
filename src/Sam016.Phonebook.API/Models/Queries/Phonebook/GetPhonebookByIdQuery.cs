namespace Sam016.Phonebook.API.Models.Queries.Phonebook
{
    public class GetPhonebookByIdQuery : BaseUserQuery, MediatR.IRequest<Sam016.Phonebook.Domain.Models.Phonebook>
    {
        public uint Id { get; set; }
    }
}
