using GateAPI.Domain.Entities.Configuracao;
using GateAPI.Infra.Models.Configuracao;

namespace GateAPI.Infra.Mappers.Configuracao
{
    public class TasksMapper : IMapper<Tasks, TasksModel>
    {
        public Tasks ToDomain(TasksModel model)
        {
            return Tasks.Load(
                model.Id,
                model.Nome,
                model.Url,
                model.Status
            );
        }

        public TasksModel ToModel(Tasks entity)
        {
            var model = new TasksModel
            {
                Id = entity.Id,
                Nome = entity.Nome,
                Url = entity.Url,
                Status = entity.Status
            };

            return model;
        }
    }
}
