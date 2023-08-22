using EatonTest.Domain.Interfaces.Models;
using EatonTest.Infrastructure;
using EatonTest.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EatonTest.Infrastructure.Implementations
{
    public class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        protected DbSet<T> entities;

        public CrudRepository(ApiContext context)
        {
            Context = context;
            entities = context.Set<T>();
        }

        protected ApiContext Context { get; }

        public virtual IEnumerable<T> GetAll()
        {
            return entities;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public T GetById(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public virtual async Task<List<T>> GetByIdsAsync(List<Guid> ids)
        {
            return await entities.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Creation entity can't be null");
            }

            entities.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Creation entity can't be null");
            }

            entities.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> CreateBatchAsync(IEnumerable<T> entitiesToCreate)
        {
            entities.AddRange(entitiesToCreate);
            await Context.SaveChangesAsync();

            return entities;
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Updation entity can't be null");
            }

            Context.Update(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Updation entity can't be null");
            }

            Context.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }

        public void Delete(Guid id)
        {
            var entity = entities.Find(id);

            if (entity == null) {
                throw new ArgumentNullException($"There is no such entity with id {id}");
            }

            Context.Remove(entity);
            Context.SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await entities.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentNullException($"There is no such entity with id {id}");
            }

            Context.Remove(entity);

            await Context.SaveChangesAsync();
        }
    }
}
