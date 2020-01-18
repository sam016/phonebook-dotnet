using System.Threading.Tasks;
using System.Collections.Generic;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IBaseRepository<TModel> where TModel : Sam016.Phonebook.Domain.Models.BaseModel
    {
        Task CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync();
        Task DeleteAsync(TModel model);
        Task DeleteAsync(int id);
    }

    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : Sam016.Phonebook.Domain.Models.BaseModel
    {
        public Task CreateAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TModel> GetByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(TModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
