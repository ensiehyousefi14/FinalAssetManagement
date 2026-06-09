using FinalAssetManagement.Contract.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalAssetManagement.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        // For read-only queries we use AsNoTracking() to disable EF Core change tracking.
        // This improves performance and reduces memory usage because the entity is not tracked
        // by the DbContext when we don't intend to update it.
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // For read-only queries we use AsNoTracking() to disable EF Core change tracking.
        // This improves performance and reduces memory usage because the entity is not tracked
        // by the DbContext when we don't intend to update it.
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            if (predicate is null)
                return await _dbSet.CountAsync();

            return await _dbSet.CountAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetPagedAsync(
                                                                int page,
                                                                int pageSize,
                                                                Expression<Func<TEntity, bool>>? predicate = null,
                                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
                                                                params Expression<Func<TEntity, object>>[] includes
                                                             )
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            IQueryable<TEntity> query = _dbSet;

            // Includes
            if (includes is not null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            // Filter
            if (predicate is not null)
                query = query.Where(predicate);

            // No tracking for read
            query = query.AsNoTracking();

            // Ordering (strongly recommended)
            if (orderBy is not null)
                query = orderBy(query);

            // Pagination
            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
