using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sam016.Phonebook.API.Models.Commands.Auth;
using Sam016.Phonebook.API.Models.Queries.Auth;
using Sam016.Phonebook.API.Models.Requests.Auth;
using Sam016.Phonebook.Domain.Dtos;
using Sam016.Phonebook.Domain.Models;

namespace Sam016.Phonebook.API.Profiles
{
    public class AuthProfile : AutoMapper.Profile
    {
        public AuthProfile()
        {
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<object, WhoamiQuery>();
            CreateMap<AuthToken, AuthTokenDto>();
        }
    }
}
