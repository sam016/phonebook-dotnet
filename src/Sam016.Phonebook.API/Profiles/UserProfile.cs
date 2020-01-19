using Sam016.Phonebook.API.Models.Queries.User;
using Sam016.Phonebook.API.Models.Requests.User;
using Sam016.Phonebook.Domain.Dtos;
using Sam016.Phonebook.Domain.Models;

namespace Sam016.Phonebook.API.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<GetAllUsersRequest, GetAllUsersQuery>();
            CreateMap<int, GetUserByIdQuery>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<User, UserDto>();
        }
    }
}
