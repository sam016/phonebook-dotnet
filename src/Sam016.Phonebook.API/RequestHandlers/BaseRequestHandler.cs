using AutoMapper;

namespace Sam016.Phonebook.API.RequestHandlers
{
    public abstract class BaseRequestHandler
    {
        private readonly IMapper Mapper;

        public BaseRequestHandler(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected Tto MapTo<Tto>(object from)
        {
            return Mapper.Map<Tto>(from);
        }
    }
}
