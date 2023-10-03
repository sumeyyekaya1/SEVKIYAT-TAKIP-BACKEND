using GlobalTradeSevkiyatTakip.Application.Interfaces.IRepositories;
using GlobalTradeSevkiyatTakip.Domain.BaseEntities;
using GlobalTradeSevkiyatTakip.Persistance.EFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalTradeSevkiyatTakip.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected readonly EFDatabaseContext context;
        public Repository(EFDatabaseContext context)
        {
            this.context = context;

        }
        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
        public virtual IQueryable<T> GetAllAsync()
        {
            return context.Set<T>().AsQueryable().Where(x => x.IsDeleted == false);
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            T entity = await context.Set<T>().AsNoTracking().Where(x => x.IsDeleted == false).FirstOrDefaultAsync(x => x.ID == id);

            return entity;
        }

        public async Task<T> RemoveAsync(T entity)
        {
            return await Task.Run(() =>
            {
                T en = context.Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);
                if (en != null)
                    context.Entry(en).State = EntityState.Detached;

                context.Entry<T>(entity).State = EntityState.Deleted;
                return entity;
            });
        }

        public async Task<T> RemoveAsync(int id)
        {
            return await RemoveAsync(await GetByIdAsync(id));
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await Task.Run(async () =>
            {
                T en = context.Set<T>().Local.FirstOrDefault(x => x.ID == entity.ID);
                if (en != null)
                    context.Entry(en).State = EntityState.Detached;

                context.Entry<T>(entity).State = EntityState.Modified;

                return entity;
            });
        }
        public async Task<T> RawQuery(string querySql, params object[] parameters)
        {
            try
            {
                if (parameters != null)
                {
                    T entity = (await context.Set<T>().FromSqlRaw<T>(querySql, parameters: parameters).ToListAsync()).FirstOrDefault();
                    return entity;
                }

                return context.Set<T>().FromSqlRaw<T>(querySql).AsEnumerable().Single();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
