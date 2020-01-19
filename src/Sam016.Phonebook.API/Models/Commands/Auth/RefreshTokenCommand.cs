namespace Sam016.Phonebook.API.Models.Commands.Auth
{
    public class RefreshTokenCommand : BaseCommand, MediatR.IRequest<Sam016.Phonebook.Domain.Models.AuthToken>
    {
    }
}
