
namespace Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);   
    }
}