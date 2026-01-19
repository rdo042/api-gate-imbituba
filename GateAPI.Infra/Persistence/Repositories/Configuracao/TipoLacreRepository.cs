using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class TipoLacreRepository(AppDbContext context) : ITipoLacreRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public Task<TipoLacre> AddAsync(TipoLacre entidade)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TipoLacre>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TipoLacre?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TipoLacre entidade)
        {
            throw new NotImplementedException();
        }

        //public async Task<TipoLacre?> GetByIdAsync(Guid id)
        //{
        //    var model = await _context.TipoLacre.FindAsync(id);

        //    return model == null ? null : TipoLacreMapper.ToDomain(model);
        //}

        //public async Task<IEnumerable<TipoLacre>> GetAllAsync()
        //{
        //    var query = await _context.TipoLacre.AsNoTracking().ToListAsync();

        //    return [.. query.Select(TipoLacreMapper.ToDomain)];
        //}

        //public async Task<TipoLacre> AddAsync(TipoLacre entidade)
        //{
        //    var model = TipoLacreMapper.ToModel(entidade);

        //    _context.TipoLacre.Add(model);
        //    await _context.SaveChangesAsync();

        //    return entidade;
        //}

        //public async Task UpdateAsync(TipoLacre entidade)
        //{
        //    var model = TipoLacreMapper.ToModel(entidade);

        //    _context.TipoLacre.Update(model);

        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(Guid id)
        //{
        //    var model = await _context.TipoLacre.FindAsync(id);
        //    if (model is null) return;

        //    _context.TipoLacre.Remove(model);
        //    await _context.SaveChangesAsync();
        //}
    }
}
