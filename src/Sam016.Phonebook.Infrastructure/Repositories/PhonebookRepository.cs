using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using PhonebookModel = Sam016.Phonebook.Domain.Models.Phonebook;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IPhonebookRepository : IBaseRepository<PhonebookModel>
    {
        Task<bool> ExistsByNameAsync(uint userId, string name);
        Task<PhonebookModel> FindAsync(uint id, uint userId);
        Task<PhonebookModel> FindByNameAsync(uint userId, string name);
        Task<IEnumerable<PhonebookModel>> GetAllAsync(uint userId);
    }

    public class PhonebookRepository : BaseRepository<PhonebookModel>, IPhonebookRepository
    {
        public PhonebookRepository(DbContext context) : base(context)
        {
        }

        public override void CopyData(PhonebookModel fromEntity, PhonebookModel toEntity)
        {
            toEntity.Name = fromEntity.Name;
            toEntity.UserId = fromEntity.UserId;
        }

        public async Task<bool> ExistsByNameAsync(uint userId, string name)
        {
            // TODO: optimize me
            return (await this.GetAllAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                                                && r.UserId == userId)).Count() > 0;
        }

        public async Task<PhonebookModel> FindAsync(uint id, uint userId)
        {
            return (await this.GetAllAsync(r => r.Id == id && r.UserId == userId)).FirstOrDefault();
        }

        public async Task<PhonebookModel> FindByNameAsync(uint userId, string name)
        {
            return (await this.GetAllAsync(r => r.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)
                                                && r.UserId == userId)).FirstOrDefault();
        }

        public async Task<IEnumerable<PhonebookModel>> GetAllAsync(uint userId)
        {
            return await this.GetAllAsync(r => r.UserId == userId);
        }
    }
}
