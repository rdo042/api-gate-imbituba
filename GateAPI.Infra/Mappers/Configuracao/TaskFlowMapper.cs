using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TaskFlowMapper : IMapper<TaskFlow, TaskFlowModel>
    {
        public TaskFlow ToDomain(TaskFlowModel model)
        {
            var listaTasks = model.TaskFlowTasks
                .Select(item => Tasks.Load(item.Tasks.Id, item.Tasks.Nome, item.Tasks.Url, item.Tasks.Status));

            return TaskFlow.Load(
                model.Id,
                model.Nome,
                listaTasks
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
