using Web_API_Rate_Limiting.Repository;

namespace Web_API_Rate_Limiting.Utilitis.UOW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChanges();
    }
}
