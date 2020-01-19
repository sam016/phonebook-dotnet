using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Sam016.Phonebook.Infrastructure.Repositories
{
    public interface IBaseRepository<TModel> where TModel : Sam016.Phonebook.Domain.Models.BaseModel, new()
    {
        Task<TModel> CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<IEnumerable<TModel>> GetAllAsync(System.Linq.Expressions.Expression<Func<TModel, bool>> expression);
        Task<TModel> GetByIdAsync(int id);
        Task DeleteAsync(TModel model);
        Task DeleteAsync(int id);
        void CopyData(TModel fromEntity, TModel toEntity);
    }

    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : Sam016.Phonebook.Domain.Models.BaseModel, new()
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<TModel> DbModelContext;

        public BaseRepository(DbContext context)
        {
            this.DbContext = context ?? throw new ArgumentNullException(nameof(context)); ;
            this.DbModelContext = context.Set<TModel>();
        }

        public abstract void CopyData(TModel oldEntity, TModel newEntity);

        public async Task<TModel> CreateAsync(TModel model)
        {
            try
            {
                var result = await DbModelContext.AddAsync(model);
                await DbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task DeleteAsync(TModel model)
        {
            var result = DbModelContext.Remove(model);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var local = DbContext.Set<TModel>()
                        .Local
                        .FirstOrDefault(entry => entry.Id.Equals(id));

            // check if local is not null
            if (local != null)
            {
                // deleted in state
                DbContext.Entry(local).State = EntityState.Deleted;
            }
            else
            {
                DbModelContext.Remove(new TModel { Id = id });
            }

            await DbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAllAsync()
        {
            var result = await DbModelContext.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<TModel>> GetAllAsync(System.Linq.Expressions.Expression<Func<TModel, bool>> expression)
        {
            var result = await DbModelContext.Where(expression).ToListAsync();
            return result;
        }

        public async Task<TModel> GetByIdAsync(int id)
        {
            var result = await DbModelContext.Where(d => d.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateAsync(TModel model)
        {
            var existing = await DbContext.Set<TModel>()
                        .FirstOrDefaultAsync(entry => entry.Id.Equals(model.Id));

            if (existing == null)
            {
                throw new Exception("Entity not found");
            }

            CopyData(model, existing);

            DbContext.Update(existing);

            // save
            await DbContext.SaveChangesAsync();
        }
    }
}
