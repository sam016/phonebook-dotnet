namespace Sam016.Phonebook.API.Models.Queries.Phonebook
{
    public class GetPhonebookByIdQuery : BaseQuery, MediatR.IRequest<Sam016.Phonebook.Domain.Models.Phonebook>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
