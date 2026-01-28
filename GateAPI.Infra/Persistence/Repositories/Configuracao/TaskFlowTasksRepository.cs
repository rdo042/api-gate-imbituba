using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class TaskFlowTasksRepository(AppDbContext context, IMapper<TaskFlowTasks, TaskFlowTasksModel> mapper) : ITaskFlowTasksRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IMapper<TaskFlowTasks, TaskFlowTasksModel> _mapper = mapper;

        public async Task<TaskFlowTasks> AddAsync(TaskFlowTasks entidade)
        {
            var model = _mapper.ToModel(entidade);

            _context.TaskFlowTasks.Add(model);
            await _context.SaveChangesAsync();

            return entidade;
        }

        public async Task<IEnumerable<TaskFlowTasks>> GetByFlowIdAsync(Guid flowId)
        {
            var query = await _context.TaskFlowTasks
                .Where(x => x.TaskFlowId == flowId)
                .Include(x=> x.TaskFlow)
                .Include(x=> x.Tasks)
                .AsNoTracking()
                .ToListAsync();

            return query == null ? [] : query.Select(_mapper.ToDomain);
        }

        public async Task<TaskFlowTasks?> GetByOrderAsync(Guid flowId, int ordem)
        {
            var model = await _context.TaskFlowTasks
                .Include(x => x.TaskFlow)
                .Include(x => x.Tasks)
                .SingleOrDefaultAsync(x => x.TaskFlowId == flowId && x.Ordem == ordem);

            return model == null ? null : _mapper.ToDomain(model);
        }

        public async Task<TaskFlowTasks?> GetSpecificAsync(Guid flowId, Guid taskId)
        {
            var model = await _context.TaskFlowTasks
                .Include(x => x.TaskFlow)
                .Include(x => x.Tasks)
                .SingleOrDefaultAsync(x => x.TaskFlowId == flowId && x.TasksId == taskId);

            return model == null ? null : _mapper.ToDomain(model);
        }

        public async Task<int> GetMaxOrdemAsync(Guid flowId)
        {
            var query = await _context.TaskFlowTasks
               .Where(x => x.TaskFlowId == flowId)
               .AsNoTracking()
               .ToListAsync();

            var total = query.Count == 0 ? 0 : query.Max(x => x.Ordem);

            return total;
        }

        public async Task<bool> Remove(Guid id)
        {
            var rowsAffected = await _context.TaskFlowTasks.Where(p => p.Id == id).ExecuteDeleteAsync();

            return rowsAffected>0;
        }

        public async Task<bool> RemoveByFlow(Guid flowId)
        {
            var rowsAffected = await _context.TaskFlowTasks.Where(p => p.TaskFlowId == flowId).ExecuteDeleteAsync();

            return rowsAffected>0;
        }

        public async Task RemoveAndShiftAsync(Guid flowId, Guid taskId)
        {
            var relacaoParaRemover = await _context.TaskFlowTasks
                .FirstOrDefaultAsync(x => x.TaskFlowId == flowId && x.TasksId == taskId);

            if (relacaoParaRemover == null) return;

            int ordemRemovida = relacaoParaRemover.Ordem;

            _context.TaskFlowTasks.Remove(relacaoParaRemover);
            //var rowsAffected = await _context.TaskFlowTasks.Where(p => p.Id == relacaoParaRemover.Id).ExecuteDeleteAsync();

            //if (rowsAffected == 0) throw new ArgumentException("Não encontrado relação para remover");

            var tarefasParaReordenar = await _context.TaskFlowTasks
                .Where(x => x.TaskFlowId == flowId && x.Ordem > ordemRemovida)
                .ToListAsync();

            foreach (var relacao in tarefasParaReordenar)
                relacao.Ordem--;

            await _context.SaveChangesAsync();
        }
    }
}
