using Microsoft.EntityFrameworkCore;
using Web_API_Rate_Limiting.Context;

namespace Web_API_Rate_Limiting.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal UniversityDbContext _context;
        internal DbSet<T> _dbset;
        public GenericRepository(UniversityDbContext context)
        {
            this._context = context;
            this._dbset = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string includeProperties = "")
        {
            var query = _dbset.AsQueryable();
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object Id, string includeProperties = "")
        {
            IQueryable<T> query = _dbset.AsQueryable();

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, "Id").Equals(Id));
        }
        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task DeleteAsync(object Id)
        {
            var entity = await _dbset.FindAsync(Id);
            if (entity != null)
                _dbset.Remove(entity);
        }

        public async Task UpdateAsync(object Id, T entity)
        {
            var entityToUpdate = await _dbset.FindAsync(Id);
            if (entityToUpdate != null)
                _dbset.Update(entity);
        }
    }
}
