using Sam016.Phonebook.API.Models.Queries.PhoneEntry;
using Sam016.Phonebook.API.Models.Commands.PhoneEntry;
using Sam016.Phonebook.API.Models.Requests.PhoneEntry;
using Sam016.Phonebook.Domain.Dtos;
using PhoneEntryModel = Sam016.Phonebook.Domain.Models.PhoneEntry;

namespace Sam016.Phonebook.API.Profiles
{
    public class PhoneEntryProfile : AutoMapper.Profile
    {
        public PhoneEntryProfile()
        {
            CreateMap<GetAllPhoneEntriesRequest, GetAllPhoneEntriesQuery>();
            CreateMap<uint, GetPhoneEntryByIdQuery>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<CreatePhoneEntryRequest, CreatePhoneEntryCommand>();
            CreateMap<UpdatePhoneEntryRequest, UpdatePhoneEntryCommand>();
            CreateMap<uint, DeletePhoneEntryCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<CreatePhoneEntryCommand, PhoneEntryModel>();
            CreateMap<UpdatePhoneEntryCommand, PhoneEntryModel>();

            CreateMap<PhoneEntryModel, PhoneEntryDto>();
        }
    }
}
