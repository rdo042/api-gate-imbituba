using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class TasksRepository(AppDbContext context) : ITasksRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Tasks>> GetAllPorParametroAsync(string nome)
        {
            var query = _context.Tasks.AsNoTracking();

            if (nome is not null)
            {
                var nomeNormalizado = nome.ToLower();
                query = query.Where(x =>
                    EF.Functions.Like(x.Nome, $"%{nomeNormalizado}%")
                );
            }

            var entidades = await query.Take(10).ToListAsync();

            var lista = entidades.Select(item => Tasks.Load(item.Id, item.Nome, item.Url, item.Status));

            return lista;
        }

        public async Task<Tasks?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var query = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (query is null)
                return null;

            return Tasks.Load(query.Id, query.Nome, query.Url, query.Status);
        }
    }
}
