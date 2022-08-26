
using Core.Specification;

namespace Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<List<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);   
        Task<T> GetEntityWithSpecification(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}