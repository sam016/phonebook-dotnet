namespace Sam016.Phonebook.API.Models.Commands.Auth
{
    public class LoginCommand : BaseCommand, MediatR.IRequest<Sam016.Phonebook.Domain.Models.AuthToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
