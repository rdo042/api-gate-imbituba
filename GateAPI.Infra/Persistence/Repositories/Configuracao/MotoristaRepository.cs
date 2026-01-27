using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class MotoristaRepository : BaseRepository<Motorista, MotoristaModel>, IMotoristaRepository
    {
        public MotoristaRepository(AppDbContext context, IMapper<Motorista, MotoristaModel> mapper) : base(context, mapper)
        { }

        public async Task<Motorista?> GetByDocumentoAsync(string documento, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable().AsNoTracking();

            var model = await query.FirstOrDefaultAsync(m => m.CPF == documento || m.CNH == documento, cancellationToken);

            return model == null ? null : _mapper.ToDomain(model);

        }
    }
}