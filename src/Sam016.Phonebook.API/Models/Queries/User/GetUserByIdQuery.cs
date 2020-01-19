namespace Sam016.Phonebook.API.Models.Queries.User
{
    public class GetUserByIdQuery : BaseQuery, MediatR.IRequest<Sam016.Phonebook.Domain.Models.User>
    {
        public int Id { get; set; }
    }
}
