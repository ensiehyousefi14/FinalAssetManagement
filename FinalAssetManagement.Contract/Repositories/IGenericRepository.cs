using System.Linq.Expressions;

namespace FinalAssetManagement.Contract.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<TEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<TEntity>> GetPagedAsync(
                                                    int page,
                                                    int pageSize,
                                                    Expression<Func<TEntity, bool>>? predicate = null,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                    params Expression<Func<TEntity, object>>[] includes
                                                );



    }
}
