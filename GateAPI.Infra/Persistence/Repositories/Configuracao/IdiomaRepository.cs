using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class IdiomaRepository : BaseRepository<Idioma, IdiomaModel>, IIdiomaRepository
    {
        public IdiomaRepository(AppDbContext context, IMapper<Idioma, IdiomaModel> mapper) : base(context, mapper)
        {
        }

        public async Task<Idioma?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            var model = await _dbSet
                .Where(i => i.Codigo == codigo)
                .SingleOrDefaultAsync(cancellationToken);

            return model == null ? null : _mapper.ToDomain(model);
        }

        public async Task<Idioma?> GetPadraoAsync(CancellationToken cancellationToken = default)
        {
            var model = await _dbSet
                .Where(i => i.EhPadrao && i.Status == StatusEnum.ATIVO)
                .SingleOrDefaultAsync(cancellationToken);

            return model == null ? null : _mapper.ToDomain(model);
        }

        public async Task<IEnumerable<Idioma>> GetByCanalAsync(int canal, CancellationToken cancellationToken = default)
        {
            var modelos = await _dbSet
                .Where(i => i.Canal == canal && i.Status == StatusEnum.ATIVO)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return modelos.Select(_mapper.ToDomain);
        }

        public async Task<IEnumerable<Idioma>> GetAllAtivosAsync(CancellationToken cancellationToken = default)
        {
            var modelos = await _dbSet
                .Where(i => i.Status == StatusEnum.ATIVO)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return modelos.Select(_mapper.ToDomain);
        }
    }
}
