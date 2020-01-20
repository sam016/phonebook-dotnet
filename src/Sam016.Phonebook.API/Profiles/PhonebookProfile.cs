using Sam016.Phonebook.API.Models.Queries.Phonebook;
using Sam016.Phonebook.API.Models.Commands.Phonebook;
using Sam016.Phonebook.API.Models.Requests.Phonebook;
using Sam016.Phonebook.Domain.Dtos;
using PhonebookModel = Sam016.Phonebook.Domain.Models.Phonebook;

namespace Sam016.Phonebook.API.Profiles
{
    public class PhonebookProfile : AutoMapper.Profile
    {
        public PhonebookProfile()
        {
            CreateMap<GetAllPhonebooksRequest, GetAllPhonebooksQuery>();
            CreateMap<uint, GetPhonebookByIdQuery>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<CreatePhonebookRequest, CreatePhonebookCommand>();
            CreateMap<UpdatePhonebookRequest, UpdatePhonebookCommand>();
            CreateMap<uint, DeletePhonebookCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));

            CreateMap<CreatePhonebookCommand, PhonebookModel>();
            CreateMap<UpdatePhonebookCommand, PhonebookModel>();

            CreateMap<PhonebookModel, PhonebookDto>();
        }
    }
}
