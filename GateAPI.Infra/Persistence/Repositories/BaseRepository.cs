using GateAPI.Domain.Repositories;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories
{
    public abstract class BaseRepository<TDomain, TModel>(
    AppDbContext context,
    IMapper<TDomain, TModel> mapper) : IBaseRepository<TDomain>
    where TDomain : class
    where TModel : class
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<TModel> _dbSet = context.Set<TModel>();
        protected readonly IMapper<TDomain, TModel> _mapper = mapper;

        protected virtual IQueryable<TModel> ApplyIncludes(IQueryable<TModel> query) => query;

        public virtual async Task<TDomain?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = ApplyIncludes(_dbSet.AsQueryable());

            var model = await query.FirstOrDefaultAsync(m => EF.Property<Guid>(m, "Id") == id, cancellationToken);

            return model == null ? null : _mapper.ToDomain(model);
        }

        public virtual async Task<TDomain> AddAsync(TDomain entidade, CancellationToken cancellationToken = default)
        {
            var model = _mapper.ToModel(entidade);
            await _dbSet.AddAsync(model, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entidade;
        }

        public Task<IEnumerable<TDomain>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<TDomain>, int)> GetAllPaginatedAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual async Task UpdateAsync(TDomain entidade, CancellationToken cancellationToken = default)
        {
            var model = _mapper.ToModel(entidade);
            _dbSet.Update(model);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var model = await _dbSet.FirstOrDefaultAsync(m => EF.Property<Guid>(m, "Id") == id, cancellationToken);
            if (model == null) return;

            _dbSet.Remove(model);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
