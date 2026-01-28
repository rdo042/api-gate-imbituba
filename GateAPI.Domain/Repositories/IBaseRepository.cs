using GateAPI.Domain.Entities;
using System.Linq.Expressions;

namespace GateAPI.Domain.Repositories
{
    public interface IBaseRepository<TDomain>
    where TDomain : class
    {
        Task<TDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDomain>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TDomain> AddAsync(TDomain entidade, CancellationToken cancellationToken = default);
        Task UpdateAsync(TDomain entidade, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
