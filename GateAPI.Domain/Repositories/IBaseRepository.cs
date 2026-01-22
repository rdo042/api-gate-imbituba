namespace GateAPI.Domain.Repositories
{
    public interface IBaseRepository<TDomain> where TDomain : class
    {
        Task<TDomain?> GetByIdAsync(Guid id);
        Task<IEnumerable<TDomain>> GetAllAsync();
        //Task<(IEnumerable<TDomain>, int)> GetAllPaginatedAsync();
        Task<TDomain> AddAsync(TDomain entidade);
        Task UpdateAsync(TDomain entidade);
        Task DeleteAsync(Guid id);
    }
}
