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
    }
}
