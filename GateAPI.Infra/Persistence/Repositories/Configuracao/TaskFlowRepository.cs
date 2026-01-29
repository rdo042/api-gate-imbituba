using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Domain.Repositories.Configuracao;
using GateAPI.Infra.Mappers;
using GateAPI.Infra.Models.Configuracao;
using GateAPI.Infra.Persistence.Context;

namespace GateAPI.Infra.Persistence.Repositories.Configuracao
{
    public class TaskFlowRepository(AppDbContext context, IMapper<TaskFlow, TaskFlowModel> mapper) 
        : BaseRepository<TaskFlow, TaskFlowModel>(context, mapper), 
        ITaskFlowRepository
    {
    }
}
