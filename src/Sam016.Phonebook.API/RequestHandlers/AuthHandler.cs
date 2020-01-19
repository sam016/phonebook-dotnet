using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sam016.Phonebook.API.Models.Commands.Auth;
using Sam016.Phonebook.API.Models.Queries.Auth;
using Sam016.Phonebook.Domain.Models;
using Sam016.Phonebook.Infrastructure.Repositories;

namespace Sam016.Phonebook.API.RequestHandlers
{
    public class AuthHandler :
        IRequestHandler<LoginCommand, AuthToken>,
        IRequestHandler<LogoutCommand, Unit>,
        IRequestHandler<RefreshTokenCommand, AuthToken>,
        IRequestHandler<WhoamiQuery, AuthProfile>
    {
        private readonly IUserRepository UserRepository;
        private readonly IConfiguration Configuration;
        private readonly IHttpContextAccessor HttpContextAccessor;

        public AuthHandler(IUserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            UserRepository = userRepository;
            Configuration = configuration;
            HttpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = (await UserRepository.GetAllAsync(x => x.Email == request.Email && x.Password == request.Password)).FirstOrDefault();

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Auth:Secret"));
            var tokenExpiresAt = DateTime.UtcNow.AddSeconds(Configuration.GetValue<int>("Auth:ExpirationInSecs"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                    new Claim(ClaimTypes.GivenName, user.FirstName.ToString()),
                    new Claim(ClaimTypes.Surname, user.LastName.ToString()),
                }),
                Expires = tokenExpiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return new AuthToken()
            {
                Token = jwtToken,
                ExpiresAt = tokenExpiresAt,
            };
        }

        public Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthToken> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthProfile> Handle(WhoamiQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new AuthProfile()
            {
                Id = int.Parse(HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value),
                FirstName = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value,
                LastName = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname).Value,
                Email = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value,
            });
        }
    }
}
