using System.Collections;
using Web_API_Rate_Limiting.Context;
using Web_API_Rate_Limiting.Repository;

namespace Web_API_Rate_Limiting.Utilitis.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityDbContext _newsPaperContext;
        private Hashtable Repositories;
        public UnitOfWork(UniversityDbContext newsPaperContext)
        {
            _newsPaperContext = newsPaperContext;
            Repositories = new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!Repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<T>(_newsPaperContext);
                Repositories.Add(type, repo);
            }
            return Repositories[type] as IGenericRepository<T>;
        }

        public async Task<int> SaveChanges()
             => await _newsPaperContext.SaveChangesAsync();
        public async ValueTask DisposeAsync()
             => await _newsPaperContext.DisposeAsync();
    }
}
