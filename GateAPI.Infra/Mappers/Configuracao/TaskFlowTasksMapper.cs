using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TaskFlowTasksMapper : IMapper<TaskFlowTasks, TaskFlowTasksModel>
    {
        public TaskFlowTasks ToDomain(TaskFlowTasksModel model)
        {
            var task =  Tasks.Load(
                model.Tasks.Id,
                model.Tasks.Nome,
                model.Tasks.Url,
                model.Tasks.Status
            );

            var taskFlow = TaskFlow.Load(
                model.TaskFlow.Id,
                model.TaskFlow.Nome
            );

            return TaskFlowTasks.Load(
                model.Id,
                taskFlow,
                task,
                model.Ordem,
                model.Status
            );
        }

        public TaskFlowTasksModel ToModel(TaskFlowTasks entity)
        {
            var model = new TaskFlowTasksModel
            {
                Id = entity.Id,
                TaskFlowId = entity.TaskFlow.Id,
                TasksId = entity.Tasks.Id,
                Ordem = entity.Ordem,
                Status = entity.Status,
            };

            return model;
        }
    }
}
