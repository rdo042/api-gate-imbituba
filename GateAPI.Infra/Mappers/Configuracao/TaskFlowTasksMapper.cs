using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TaskFlowTasksMapper : IMapper<TaskFlowTasks, TaskFlowTasksModel>
    {
        public TaskFlowTasks ToDomain(TaskFlowTasksModel model)
        {
            throw new NotImplementedException();
            //var listaTasks = model.TaskFlowTasks
            //    .Select(item => Tasks.Load(item.Tasks.Id, item.Tasks.Nome, item.Tasks.Url, item.Tasks.Status));

            //return TaskFlow.Load(
            //    model.Id,
            //    model.Nome,
            //    listaTasks
            //);
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
