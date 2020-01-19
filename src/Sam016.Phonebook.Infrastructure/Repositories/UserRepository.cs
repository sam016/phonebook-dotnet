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
    }
}
