using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoPhoneEntryModel = Sam016.Phonebook.Domain.Models.PhoneEntry;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IPhoneEntryRepository : IBaseRepository<RepoPhoneEntryModel>
    {
    }

    public class PhoneEntryRepository : BaseRepository<RepoPhoneEntryModel>, IPhoneEntryRepository
    {
        public PhoneEntryRepository(DbContext context) : base(context)
        {
        }

        public override void CopyData(RepoPhoneEntryModel fromEntity, RepoPhoneEntryModel toEntity)
        {
            toEntity.FirstName = fromEntity.FirstName;
            toEntity.LastName = fromEntity.LastName;
            toEntity.Country = fromEntity.Country;
            toEntity.Phone = fromEntity.Phone;
            toEntity.OrganizationName = fromEntity.OrganizationName;
            toEntity.Address = fromEntity.Address;
            toEntity.PhonebookId = fromEntity.PhonebookId;
        }
    }
}
