using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoPhonebookModel = Sam016.Phonebook.Domain.Models.Phonebook;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IPhonebookRepository : IBaseRepository<RepoPhonebookModel>
    {
    }

    public class PhonebookRepository : BaseRepository<RepoPhonebookModel>, IPhonebookRepository
    {
        public PhonebookRepository(DbContext context) : base(context)
        {
        }

        public override void CopyData(RepoPhonebookModel fromEntity, RepoPhonebookModel toEntity)
        {
            toEntity.Name = fromEntity.Name;
            toEntity.UserId = fromEntity.UserId;
        }
    }
}
