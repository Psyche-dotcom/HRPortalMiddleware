


using HRPortal.Core.Context;
using HRPortal.Core.Repository.Interface;

namespace HRPortal.Core.Repository.Implementation
{
    public class HRPortalRepository<TEntity> : IHRPortalRepository<TEntity> where TEntity : class
    {
        private readonly HRPortalContext _context;

        public HRPortalRepository(HRPortalContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>();
        }

        public Task<int> SaveChanges()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
