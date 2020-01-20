using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sam016.Phonebook.API.Models.Queries.PhoneEntry;
using Sam016.Phonebook.API.Models.Commands.PhoneEntry;
using Sam016.Phonebook.Domain.Models;
using Sam016.Phonebook.Infrastructure.Repositories;
using AutoMapper;

namespace Sam016.Phonebook.API.RequestHandlers
{
    public class PhoneEntriesHandler : BaseRequestHandler,
        IRequestHandler<GetAllPhoneEntriesQuery, IEnumerable<PhoneEntry>>,
        IRequestHandler<GetPhoneEntryByIdQuery, PhoneEntry>,
        IRequestHandler<CreatePhoneEntryCommand, PhoneEntry>,
        IRequestHandler<UpdatePhoneEntryCommand, PhoneEntry>,
        IRequestHandler<DeletePhoneEntryCommand, PhoneEntry>
    {
        private readonly IPhoneEntryRepository PhoneEntryRepository;

        public PhoneEntriesHandler(IMapper mapper, IPhoneEntryRepository phoneEntryRepository)
            : base(mapper)
        {
            PhoneEntryRepository = phoneEntryRepository;
        }

        public async Task<PhoneEntry> Handle(CreatePhoneEntryCommand request, CancellationToken cancellationToken)
        {
            var exists = await PhoneEntryRepository.ExistsByCountryPhoneAsync(request.UserId, request.PhonebookId, request.CountryCode, request.Phone);

            if (exists)
            {
                throw new System.Exception("Phone entry already exists");
            }

            var phoneEntry = MapTo<PhoneEntry>(request);

            var createdPhoneEntry = await PhoneEntryRepository.CreateAsync(phoneEntry);

            return createdPhoneEntry;
        }

        public async Task<PhoneEntry> Handle(GetPhoneEntryByIdQuery request, CancellationToken cancellationToken)
        {
            var phoneEntry = await PhoneEntryRepository.FindAsync(request.Id, request.UserId, request.PhonebookId);

            return phoneEntry;
        }

        public async Task<IEnumerable<PhoneEntry>> Handle(GetAllPhoneEntriesQuery request, CancellationToken cancellationToken)
        {
            var phoneEntries = await PhoneEntryRepository.GetAllAsync(request.UserId, request.PhonebookId);

            return phoneEntries;
        }

        public async Task<PhoneEntry> Handle(UpdatePhoneEntryCommand request, CancellationToken cancellationToken)
        {
            var existing = await PhoneEntryRepository.FindAsync(request.Id, request.UserId, request.PhonebookId);

            if (existing == null)
            {
                // Phone entry does not exists
                return null;
            }

            var duplicatePhoneEntry = await PhoneEntryRepository.FindByCountryPhoneAsync(request.UserId, request.PhonebookId, request.CountryCode, request.Phone);

            if (duplicatePhoneEntry != null && duplicatePhoneEntry.Id != request.Id)
            {
                throw new System.Exception("Phone entry already exists");
            }

            var updatedPhoneEntry = MapTo<PhoneEntry>(request);

            await PhoneEntryRepository.UpdateAsync(updatedPhoneEntry);

            return updatedPhoneEntry;
        }

        public async Task<PhoneEntry> Handle(DeletePhoneEntryCommand request, CancellationToken cancellationToken)
        {
            var existingPhoneEntry = await PhoneEntryRepository.FindAsync(request.Id, request.UserId, request.PhonebookId);

            if (existingPhoneEntry == null)
            {
                // Phone entry does not exists
                return null;
            }

            await PhoneEntryRepository.DeleteAsync(request.Id);

            return existingPhoneEntry;
        }
    }
}
