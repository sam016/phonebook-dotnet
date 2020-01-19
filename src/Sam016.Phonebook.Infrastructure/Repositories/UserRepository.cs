using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoUserModel = Sam016.Phonebook.Domain.Models.User;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IUserRepository : IBaseRepository<RepoUserModel>
    {
    }

    public class UserRepository : BaseRepository<RepoUserModel>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override void CopyData(RepoUserModel fromEntity, RepoUserModel toEntity)
        {
            toEntity.FirstName = fromEntity.FirstName;
            toEntity.LastName = fromEntity.LastName;
            toEntity.Email = fromEntity.Email;
            toEntity.Password = fromEntity.Password;
        }
    }
}
