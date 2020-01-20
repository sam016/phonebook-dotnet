using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ModelPhoneEntry = Sam016.Phonebook.Domain.Models.PhoneEntry;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IPhoneEntryRepository : IBaseRepository<ModelPhoneEntry>
    {
        Task<bool> ExistsByCountryPhoneAsync(uint userId, uint phonebookId, string countryCode, string phone);
        Task<ModelPhoneEntry> FindByCountryPhoneAsync(uint userId, uint phonebookId, string countryCode, string phone);
        Task<IEnumerable<ModelPhoneEntry>> GetAllAsync(uint userId, uint phonebookId);
        Task<ModelPhoneEntry> FindAsync(uint id, uint userId, uint phonebookId);
        Task<ModelPhoneEntry> DeleteAsync(uint id, uint userId, uint phonebookId);
    }

    public class PhoneEntryRepository : BaseRepository<ModelPhoneEntry>, IPhoneEntryRepository
    {
        public PhoneEntryRepository(DbContext context) : base(context)
        {
        }

        public override void CopyData(ModelPhoneEntry fromEntity, ModelPhoneEntry toEntity)
        {
            toEntity.FirstName = fromEntity.FirstName;
            toEntity.LastName = fromEntity.LastName;
            toEntity.CountryCode = fromEntity.CountryCode;
            toEntity.Phone = fromEntity.Phone;
            toEntity.OrganizationName = fromEntity.OrganizationName;
            toEntity.Address = fromEntity.Address;
            toEntity.PhonebookId = fromEntity.PhonebookId;
        }

        public async Task<ModelPhoneEntry> DeleteAsync(uint id, uint userId, uint phonebookId)
        {
            var model = await FindAsync(id, userId, phonebookId);

            if (model == null)
            {
                return null;
            }

            await DeleteAsync(model);

            return model;
        }

        public async Task<bool> ExistsByCountryPhoneAsync(uint userId, uint phonebookId, string countryCode, string phone)
        {
            // TODO: optimize me
            return (await this.GetAllAsync(r => r.CountryCode == countryCode
                                                && r.Phone == phone
                                                && r.PhonebookId == phonebookId
                                                && r.Phonebook.UserId == userId)).Count() > 0;
        }

        public async Task<ModelPhoneEntry> FindAsync(uint id, uint userId, uint phonebookId)
        {
            return (await this.GetAllAsync(r => r.Id == id
                                                && r.PhonebookId == phonebookId
                                                && r.Phonebook.UserId == userId)).FirstOrDefault();
        }

        public async Task<ModelPhoneEntry> FindByCountryPhoneAsync(uint userId, uint phonebookId, string countryCode, string phone)
        {
            return (await this.GetAllAsync(r => r.CountryCode == countryCode
                                                && r.Phone == phone
                                                && r.PhonebookId == phonebookId
                                                && r.Phonebook.UserId == userId)).FirstOrDefault();
        }

        public async Task<IEnumerable<ModelPhoneEntry>> GetAllAsync(uint userId, uint phonebookId)
        {
            return (await this.GetAllAsync(r=> r.PhonebookId == phonebookId
                                                && r.Phonebook.UserId == userId));
        }
    }
}
