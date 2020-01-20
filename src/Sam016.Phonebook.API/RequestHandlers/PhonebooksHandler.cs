using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sam016.Phonebook.API.Models.Queries.Phonebook;
using Sam016.Phonebook.API.Models.Commands.Phonebook;
using Sam016.Phonebook.Domain.Models;
using Sam016.Phonebook.Infrastructure.Repositories;
using AutoMapper;
using PhonebookModel = Sam016.Phonebook.Domain.Models.Phonebook;

namespace Sam016.Phonebook.API.RequestHandlers
{
    public class PhonebooksHandler : BaseRequestHandler,
        IRequestHandler<GetAllPhonebooksQuery, IEnumerable<PhonebookModel>>,
        IRequestHandler<GetPhonebookByIdQuery, PhonebookModel>,
        IRequestHandler<CreatePhonebookCommand, PhonebookModel>,
        IRequestHandler<UpdatePhonebookCommand, PhonebookModel>,
        IRequestHandler<DeletePhonebookCommand, PhonebookModel>
    {
        private readonly IPhonebookRepository PhonebookRepository;

        public PhonebooksHandler(IMapper mapper, IPhonebookRepository phoneEntryRepository)
            : base(mapper)
        {
            PhonebookRepository = phoneEntryRepository;
        }

        public async Task<PhonebookModel> Handle(CreatePhonebookCommand request, CancellationToken cancellationToken)
        {
            var exists = await PhonebookRepository.ExistsByNameAsync(request.UserId, request.Name);

            if (exists)
            {
                throw new System.Exception("Phone entry already exists");
            }

            var phoneEntry = MapTo<PhonebookModel>(request);

            var createdPhonebook = await PhonebookRepository.CreateAsync(phoneEntry);

            return createdPhonebook;
        }

        public async Task<PhonebookModel> Handle(GetPhonebookByIdQuery request, CancellationToken cancellationToken)
        {
            var phoneEntry = await PhonebookRepository.FindAsync(request.Id, request.UserId);

            return phoneEntry;
        }

        public async Task<IEnumerable<PhonebookModel>> Handle(GetAllPhonebooksQuery request, CancellationToken cancellationToken)
        {
            var phoneEntries = await PhonebookRepository.GetAllAsync(request.UserId);

            return phoneEntries;
        }

        public async Task<PhonebookModel> Handle(UpdatePhonebookCommand request, CancellationToken cancellationToken)
        {
            var existingModel = await PhonebookRepository.FindAsync(request.Id, request.UserId);

            if (existingModel == null)
            {
                // Phone entry does not exists
                return null;
            }

            var duplicatePhonebook = await PhonebookRepository.FindByNameAsync(request.UserId, request.Name);

            if (duplicatePhonebook != null && duplicatePhonebook.Id != request.Id)
            {
                throw new System.Exception("Phone entry already exists");
            }

            var updatedPhonebook = MapTo<PhonebookModel>(request);

            await PhonebookRepository.UpdateAsync(updatedPhonebook);

            return updatedPhonebook;
        }

        public async Task<PhonebookModel> Handle(DeletePhonebookCommand request, CancellationToken cancellationToken)
        {
            var existingPhonebook = await PhonebookRepository.FindAsync(request.Id, request.UserId);

            if (existingPhonebook == null)
            {
                // Phone entry does not exists
                return null;
            }

            await PhonebookRepository.DeleteAsync(request.Id);

            return existingPhonebook;
        }
    }
}
