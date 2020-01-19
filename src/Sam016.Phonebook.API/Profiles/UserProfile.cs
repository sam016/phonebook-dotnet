using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sam016.Phonebook.API.Models.Queries.User;
using Sam016.Phonebook.API.Models.Requests.User;

namespace Sam016.Phonebook.API.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<GetAllUsersRequest, GetAllUsersQuery>();
            CreateMap<int, GetUserByIdQuery>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
        }
    }
}
