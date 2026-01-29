using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TaskFlowMapper : IMapper<TaskFlow, TaskFlowModel>
    {
        public TaskFlow ToDomain(TaskFlowModel model)
        {
            return TaskFlow.Load(
                model.Id,
                model.Nome
            );
        }

        public TaskFlowModel ToModel(TaskFlow entity)
        {
            var model = new TaskFlowModel
            {
                Id = entity.Id,
                Nome = entity.Nome
            };

            return model;
        }
    }
}
