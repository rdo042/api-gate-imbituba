using System.Threading;
using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Enums;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class LocalAvariaRepository : BaseRepository<Domain.Entities.Configuracao.LocalAvaria, LocalAvariaModel>, ILocalAvariaRepository
    {
        private readonly StatusEnum _validStatusEnum = StatusEnum.ATIVO;
        public LocalAvariaRepository(AppDbContext context, IMapper<Domain.Entities.Configuracao.LocalAvaria, LocalAvariaModel> mapper) : base(context, mapper)
        {
        }

        public async Task<LocalAvaria?> GetByLocalAsync(string local)
        {
            var model = await _context.LocalAvaria
                .Where(l => l.Local == local && l.DeletedAt == null && l.DeletedBy == null)
                .SingleOrDefaultAsync();

            return model == null ? null : _mapper.ToDomain(model);
        }
        new public virtual async Task<IEnumerable<LocalAvaria>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.LocalAvaria
                .Where(l => l.DeletedAt == null && l.DeletedBy == null);

            var lista = await query.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

            return lista.Count != 0 ? lista.Select(_mapper.ToDomain) : [];
        }

        public virtual async Task<IEnumerable<LocalAvaria>> GetAllAppAsync(CancellationToken cancellationToken = default)
        {
            var query = _context.LocalAvaria
                .Where(l => l.Status == _validStatusEnum && l.DeletedAt == null && l.DeletedBy == null);

            var lista = await query.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);

            return lista.Count != 0 ? lista.Select(_mapper.ToDomain) : [];
        }
    }
}
