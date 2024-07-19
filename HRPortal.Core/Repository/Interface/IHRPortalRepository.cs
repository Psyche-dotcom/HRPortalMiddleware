

namespace HRPortal.Core.Repository.Interface
{
    public interface IHRPortalRepository<TEntity>
    {
        Task<TEntity?> GetByIdAsync(string id);
        IQueryable<TEntity> GetQueryable();
        Task<TEntity> Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> SaveChanges();
    }
}
