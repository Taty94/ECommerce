using Course.ECommerce.Domain.Base;
using Course.ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.ECommerce.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ECommerceDbContext context;

        public GenericRepository(ECommerceDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();//el set le dice a que tabla o entidad va a consultar
        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedDate = DateTime.Now;
                //context.Set<T>().Remove(entity);
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.ModifiedDate = DateTime.Now;
                //context.Set<T>().Remove(entity);
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public IQueryable<T> GetQueryable()
        {
            return context.Set<T>().AsQueryable();
        }

    }
}
